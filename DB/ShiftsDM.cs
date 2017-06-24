
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
    public class ShiftsDM : DBAccess<ShiftsObject>
    {
        public static bool IsAWeek(DateTime dt)
        {
            TimeSpan ts = dt - new DateTime(2016, 1, 2);
            return ((ts.Days / 7) % 2 == 0);

        }

        public DateTime NextDate(string AB, int ShiftID)
        {
            DateTime dt;
            string qry = String.Format("select dbo.Next{0}(@ShiftID)", AB);
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));

                try
                {
                    dt = Convert.ToDateTime(myc.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("There are no future {0} Weeks for this Shift.", AB));
                }

                return dt;
            }
        }
        public bool IsShiftOnDate(int ShiftID, DateTime dt)
        {
            string qry = ReadAllCommand() + "  where (dow = datepart(dw, @dt) or ShiftDate = @dt) and ShiftID = @ShiftID ";
            ShiftsObject shift = null;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    shift = LoadFrom(reader);
                }
            }
            if (shift == null)
                return false;
            return ((shift.AWeek && ShiftsDM.IsAWeek(dt)) || !ShiftsDM.IsAWeek(dt) && shift.BWeek);
        }
        public  ObjectList<ShiftsObject> FetchRecurring()
        {
            return Fetch(" where Recurring = 1 or nullif(Sequence,0) is null Order by DOW, Sequence ");
        }
        public ObjectList<ShiftsObject> FetchByCategory(bool Recurring)
        {
            if (Recurring)
                return Fetch(" where Recurring = 1 or nullif(Sequence,0) is null order by dow, BWeek, Sequence ");
            else
                return Fetch(" where Recurring = 0 and Sequence > 0 order by ShiftDate, Sequence ");
        }
        public ObjectList<ShiftsObject> FetchWithSubOffers(int GuideID)
        {
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = @" SELECT
				s.[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName] 
				,[GuideID]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftDate] = null
                ,[Recurring]
                ,s.[ShiftTimeID]
                ,[Quota]
                ,[ShiftQuota] = 0
                ,ShiftStart=cast (t.ShiftStart as DateTime)
                ,ShiftEnd=cast (t.ShiftEnd as DateTime)
				FROM Shifts s join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID left join SubOffers o on s.ShiftID = o.ShiftID and GuideID = @GuideID 
                where DOW is not null
                    order by dow, BWeek, Sequence";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Selected = GetNullableInt32(reader, "GuideID", 0) > 0;
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<ShiftsObject> ShiftsForDate(DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = @"SELECT
                [ShiftID]
                ,[ShiftName]
                ,[DOW]
                ,[AWeek]
                ,[BWeek]
                ,[Sequence]
                ,[ShortName]
                ,[Recurring]
                ,[ShiftDate]
                , s.ShiftTimeID
                ,ShiftStart = cast(t.ShiftStart as DateTime)
                ,ShiftEnd = cast(t.ShiftEnd as DateTime)
                ,[Captains] = ''
                ,[Info] = ''
                ,[Quota]
                ,ShiftQuota = dbo.ShiftQuota(s.ShiftID)
             ,Attendance = dbo.ShiftAttendance(@dt, s.ShiftID)
		     FROM Shifts s left join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID
		    WHERE (s.recurring = 1 and dow = datepart(dw, @dt)  and ((s.AWeek = 1 and dbo.AB(@dt)  = 'AWeek') or (s.BWeek = 1 and   dbo.AB(@dt) = 'BWeek')))
		    or (s.recurring = 0 and s.ShiftDate = @dt)
                order by Sequence  ";
           
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Attendance = GetNullableInt32(reader, "Attendance", 0);
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            for (int i = 0; i < Results.Count; i++)
            {
                Results[i].Captains = CaptainsForDate(Results[i].ShiftID, dt);
                Results[i].InfoDesk = InfoForDate(Results[i].ShiftID, dt);
            }
            return Results;
        }
        public string CaptainsForDate(int ShiftID, DateTime dt)
        {
            string qry = @"select Captains=dbo.FlattenDateCaptains(@ShiftID, @dt)";
            string res = String.Empty;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));

                try
                {
                    res = myc.ExecuteScalar().ToString();
                }
                catch { }
            }

            return res;
        }
        public string InfoForDate(int ShiftID, DateTime dt)
        {
            string qry = @"select Info=dbo.FlattenDateInfo(@ShiftID, @dt)";
            string res = String.Empty;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));

                try
                {
                    res = myc.ExecuteScalar().ToString();
                }
                catch { }
            }

            return res;
        }

        public ShiftsObject FetchShift(int ShiftID)
        {
            ShiftsObject obj = null;

            string qry = ReadAllCommand() + @" WHERE ShiftID = @ShiftID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);

                }
            }
            return obj;
        }

        public ObjectList<ShiftsObject> ShiftsForGuide(int GuideID)
        {
           
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();


            string qry = ReadAllCommand() + @" WHERE 
                  ShiftID in (select ShiftID from GuideShift where GuideID = @GuideID) order by DOW, Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        // Find all shifts where you will be present and haven't yet requested  a substitute
        public ObjectList<ShiftsObject> RegularShiftsForGuideAndDate(DateTime dt, int GuideID)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = ReadAllCommand() + @" WHERE  
                  ShiftID   in (select ShiftID from GuideShift where GuideID = @GuideID) 
                   and ShiftID not in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt) ";
            qry += " union " + ReadAllCommand() + @" WHERE  ShiftID in (select ShiftID from GuideDropins where GuideID = @GuideID
                    and DropinDate = @dt and OnShift = 1) 
                 and ShiftID not in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt) ";
            qry += "	  order by Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        if (IsShiftOnDate(obj.ShiftID, dt))
                            Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        // Find all shifts where you will be absent and need a sub
        public ObjectList<ShiftsObject> NeedSubShiftsForGuideAndDate(DateTime dt, int GuideID)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = ReadAllCommand() + @" WHERE  
                  ShiftID   in (select ShiftID from GuideShift where GuideID = @GuideID) 
                   and ShiftID  in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt and SubstituteID is null) ";
            qry += " union " + ReadAllCommand() + @" WHERE  ShiftID in (select ShiftID from GuideDropins where GuideID = @GuideID
                    and DropinDate = @dt and OnShift = 1)
                  and ShiftID  in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt and SubstituteID is null) ";
            qry += "	  order by Sequence ";
       
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        if (IsShiftOnDate(obj.ShiftID, dt))
                            Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        // Find all shifts where you will be absent for a date
        public ObjectList<ShiftsObject> AbsentShiftsForGuideAndDate(DateTime dt, int GuideID)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = ReadAllCommand() + @" WHERE  
                  ShiftID   in (select ShiftID from GuideShift where GuideID = @GuideID) 
                   and ShiftID  in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt ) ";
            qry += " union " + ReadAllCommand() + @" WHERE  ShiftID in (select ShiftID from GuideDropins where GuideID = @GuideID
                    and DropinDate = @dt and OnShift = 1)
                  and ShiftID  in (select ShiftID from GuideSubstitute where GuideID = @GuideID and SubDate = @dt) ";
            qry += "	  order by Sequence ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        if (IsShiftOnDate(obj.ShiftID, dt))
                            Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        // Used to display available shifts for a drop-in request. Exclude shifts with open sub requests
        public ObjectList<ShiftsObject> ShiftsForDateAndGuide(DateTime dt, int GuideID)
        {

            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            if (dt == DateTime.MinValue)
                return Results;

                string qry = ReadAllCommand() + @" WHERE dow = datepart(dw, @dt)  and ((dbo.AB(@dt) = 'AWeek' and AWeek = 1)
                    or (dbo.AB(@dt) = 'BWeek' and BWeek = 1))
                and ShiftID not in (select ShiftID from GuideShift where GuideID = @GuideID) 
                 and ShiftID not in (select ShiftID from GuideSubstitute where SubDate = @dt
					 and SubStituteID is null) order by Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        
        public ObjectList<ShiftsObject> SpecialShiftsForGuide(int GuideID)
        {
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();
            string qry = @"SELECT
                s.[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Recurring]
                ,[ShiftDate]
                ,s.ShiftTimeID
                ,Quota
                ,ShiftQuota = dbo.ShiftQuota(s.ShiftID)
                ,ShiftStart=cast(t.ShiftStart as DateTime)
                ,ShiftEnd=cast(t.ShiftEnd as DateTime)
                ,Attendance = (select count(*) from GuideDropins  where ShiftID = s.ShiftID)
                ,[Captains] = dbo.FlattenCaptains(s.ShiftID)
                ,[Info] = dbo.FlattenInfo(s.ShiftID)
				,[Selected]  = (select cast(max(1) as bit) from GuideDropins where ShiftID = s.ShiftID and GuideID = @GuideID)
                FROM Shifts s join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID
                WHERE Recurring = 0 and ShiftDate >= cast( getdate() as date)    order by ShiftDate, sequence";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Selected = GetNullableBoolean(reader, "Selected", false);
                        obj.Attendance = GetNullableInt32(reader, "Attendance", 0);
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }



        public ObjectList<ShiftsObject> ShiftsForMonth(int Yr, int Mo)
        {
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();

            string qry = @"SELECT
				[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftTimeID] 
                ,[Recurring]
                ,[ShiftDate] = isnull(ShiftDate, dt)
                ,[Attendance]
                ,[ShiftStart] = null
                ,[ShiftEnd] = null
                ,[Quota]
                ,[ShiftQuota]
			   FROM dbo.ShiftsForMonth(@Yr, @Mo)  order by dt, Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Yr", Yr));
                myc.Parameters.Add(new SqlParameter("Mo", Mo));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Attendance = GetNullableInt32(reader, "Attendance", 0);
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        // Current List of Info Center guides for the month
        public ObjectList<ShiftsObject> InfoForMonth(int Yr, int Mo)
        {
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();

            string qry = @"SELECT
				dt, s.[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftTimeID] 
                ,[Recurring]
                ,[ShiftDate] = isnull(ShiftDate, dt)
                ,[Attendance]
                ,[ShiftStart] = null
                ,[ShiftEnd] = null
                ,[Quota]
                ,[ShiftQuota]
                ,[FirstName]
                ,[LastName]
                ,g.[GuideID]
                ,[VolID]
			   FROM dbo.ShiftsForMonth(@Yr, @Mo)   s JOIN  GuideShift gs on s.ShiftID = gs.ShiftID 
				 join Guides g on gs.GuideID = g.GuideID and g.RoleID = 6  
				 where not exists (select 1 from GuideSubstitute where GuideID = g.GuideID and SubDate = dt)
			union 
                SELECT
                dt,s.[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftTimeID] 
                ,[Recurring]
                ,[ShiftDate] = isnull(ShiftDate, dt)
                ,[Attendance]
                ,[ShiftStart] = null
                ,[ShiftEnd] = null
                ,[Quota]
                ,[ShiftQuota]
                ,[FirstName]
                ,[LastName]
                ,g.[GuideID]
                ,[VolID]
            FROM dbo.ShiftsForMonth(@Yr, @Mo) s JOIN  GuideSubstitute gs on s.ShiftID = gs.ShiftID 
				and s.dt = gs.SubDate
				 join Guides g on gs.SubstituteID = g.GuideID and g.RoleID = 6 
            union
            SELECT
                dt,s.[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftTimeID] 
                ,[Recurring]
                ,[ShiftDate] = isnull(ShiftDate, dt)
                ,[Attendance]
                ,[ShiftStart] = null
                ,[ShiftEnd] = null
                ,[Quota]
                ,[ShiftQuota]
                ,[FirstName]
                ,[LastName]
                ,g.[GuideID]
                ,[VolID]
            FROM dbo.ShiftsForMonth(@Yr, @Mo) s JOIN  GuideDropins gs on s.ShiftID = gs.ShiftID 
				and s.dt = gs.DropinDate
				 join Guides g on gs.GuideID = g.GuideID and gs.RoleID = 6  			 
                order by dt, Sequence ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Yr", Yr));
                myc.Parameters.Add(new SqlParameter("Mo", Mo));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Attendance = GetNullableInt32(reader, "Attendance", 0);
                        obj.InfoFirst = GetNullableString(reader, "FirstName", String.Empty);
                        obj.InfoLast = GetNullableString(reader, "LastName", String.Empty);
                        obj.InfoVolID = GetNullableString(reader, "VolID", String.Empty);
                        obj.InfoGuideID = GetNullableInt32(reader, "GuideID", 0);
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public void Update(ShiftsObject obj)
        {
            string qry = @"UPDATE  Shifts SET 
				ShiftName=@ShiftName
				,DOW=nullif(@DOW, 0)
				,AWeek=@AWeek
				,BWeek=@BWeek
				,Sequence=@Sequence
				,ShortName=@ShortName
                ,ShiftTimeID = @ShiftTimeID 
                ,ShiftDate = nullif(@ShiftDate, @DefaultDate)
                ,Quota = nullif(@Quota, 0)
				 WHERE ShiftID = @ShiftID"
                  ;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", obj.ShiftID));
                myc.Parameters.Add(new SqlParameter("ShiftName", obj.ShiftName));
                myc.Parameters.Add(new SqlParameter("DOW", obj.DOW));
                myc.Parameters.Add(new SqlParameter("AWeek", obj.AWeek));
                myc.Parameters.Add(new SqlParameter("BWeek", obj.BWeek));
                myc.Parameters.Add(new SqlParameter("Sequence", obj.Sequence));
                myc.Parameters.Add(new SqlParameter("ShortName", obj.ShortName));
                myc.Parameters.Add(new SqlParameter("ShiftTimeID", obj.ShiftTimeID)); 
                myc.Parameters.Add(new SqlParameter("ShiftDate", obj.ShiftDate));
                myc.Parameters.Add(new SqlParameter("DefaultDate", obj.SQLMinDate));
                myc.Parameters.Add(new SqlParameter("Quota", obj.Quota));
                myc.ExecuteNonQuery();
            }
        }

        public void Save(ShiftsObject obj)
        {
            string qry = @"INSERT INTO Shifts (
				[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[ShiftTimeID]
                ,[Recurring]
                ,[ShiftDate]
                ,[Quota]
				)
			VALUES(
				@ShiftName
				,nullif(@DOW,0)
				,@AWeek
				,@BWeek
				,@Sequence
				,@ShortName
                ,@ShiftTimeID
                ,@Recurring
                ,nullif(@ShiftDate, @DefaultDate)
                ,nullif(@Quota, 0)
				)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftName", obj.ShiftName));
                myc.Parameters.Add(new SqlParameter("DOW", obj.DOW));
                myc.Parameters.Add(new SqlParameter("AWeek", obj.AWeek));
                myc.Parameters.Add(new SqlParameter("BWeek", obj.BWeek));
                myc.Parameters.Add(new SqlParameter("Sequence", obj.Sequence));
                myc.Parameters.Add(new SqlParameter("ShortName", obj.ShortName));
                myc.Parameters.Add(new SqlParameter("ShiftTimeID", obj.ShiftTimeID));
                myc.Parameters.Add(new SqlParameter("Recurring", obj.Recurring));
                myc.Parameters.Add(new SqlParameter("ShiftDate", obj.ShiftDate));
                myc.Parameters.Add(new SqlParameter("DefaultDate", obj.SQLMinDate));
                myc.Parameters.Add(new SqlParameter("Quota", obj.Quota));
                myc.ExecuteNonQuery();
            }
        }

        public void Delete(int ShiftID)
        {
            string qry = @"DELETE FROM Shifts WHERE [ShiftID] = @ShiftID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.ExecuteNonQuery();
            }
        }

        protected override ShiftsObject LoadFrom(SqlDataReader reader)
        {
          
            if (reader == null) return null;
            if (!reader.Read()) return null;
            ShiftsObject obj = new ShiftsObject();
            obj.ShiftID = GetNullableInt32(reader, "ShiftID", 0);
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.DOW = GetNullableInt32(reader, "DOW", 0);
            obj.AWeek = GetNullableBoolean(reader, "AWeek", false);
            obj.BWeek = GetNullableBoolean(reader, "BWeek", false);
            obj.Recurring = GetNullableBoolean(reader, "Recurring", false);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.ShortName = GetNullableString(reader, "ShortName", String.Empty);
            obj.Captains = GetNullableString(reader, "Captains", String.Empty);
            obj.InfoDesk = GetNullableString(reader, "Info", String.Empty);
            obj.ShiftTimeID = GetNullableInt32(reader, "ShiftTimeID", 0);
            obj.ShiftDate = GetNullableDateTime(reader, "ShiftDate", obj.SQLMinDate);
            obj.ShiftStart = GetNullableDateTime(reader, "ShiftStart", obj.SQLMinDate);
            obj.ShiftEnd = GetNullableDateTime(reader, "ShiftEnd", obj.SQLMinDate);
            obj.ShiftQuota = GetNullableInt32(reader, "ShiftQuota", 0);
            obj.Quota = GetNullableInt32(reader, "Quota", 0);
            return obj;
        }

        protected override string ReadAllCommand()
        {
            return @"
			SELECT
				[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Recurring]
                ,[ShiftDate]
                ,s.ShiftTimeID
                ,[Quota]
                ,ShiftQuota = dbo.ShiftQuota(s.ShiftID)
                ,ShiftStart=cast (t.ShiftStart as DateTime)
                ,ShiftEnd=cast (t.ShiftEnd as DateTime)
                ,[Captains] = dbo.FlattenCaptains(ShiftID)
                ,[Info] = dbo.FlattenInfo(ShiftID)
				FROM Shifts s left join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID ";
        }
        public int GetLast()
        {
            string qry = "SELECT IDENT_CURRENT('Shifts')";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                return Convert.ToInt32(myc.ExecuteScalar());
            }
        }

        public ObjectList<ShiftSummaryObject> ShiftCountReport(DateTime StartDate, DateTime EndDate)
        {
            ObjectList<ShiftSummaryObject> Results = new ObjectList<ShiftSummaryObject>();
            string qry = @"SELECT
				[ShiftID]
				,[ShiftName] 
                ,[ShortName]
                ,[Recurring]
                ,[ShiftDate]
                ,ShiftStart 
                ,ShiftEnd  
                ,[Sequence]  
                ,[Captains]  
                ,[Info]  
                ,[BaseCnt]
                ,[ShiftQuota]
                ,[SubRequests]
                ,[Substitutes]
                ,[Dropins]
                from dbo.ShiftSummary (@StartDate, @EndDate)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("StartDate", StartDate));
                myc.Parameters.Add(new SqlParameter("EndDate", EndDate));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShiftSummaryObject obj = new ShiftSummaryObject();
                        obj.ShiftID = GetNullableInt32(reader, "ShiftID", 0);
                        obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
                        obj.ShortName = GetNullableString(reader, "ShortName", String.Empty);
                        obj.Recurring = GetNullableBoolean(reader, "Recurring", false);
                        obj.ShiftDate = GetNullableDateTime(reader, "ShiftDate", obj.SQLMinDate);
                        obj.ShiftStart = GetNullableDateTime(reader, "ShiftStart", obj.SQLMinDate);
                        obj.ShiftEnd = GetNullableDateTime(reader, "ShiftEnd", obj.SQLMinDate);
                        obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
                        obj.Captains = GetNullableString(reader, "Captains", String.Empty);
                        obj.Info = GetNullableString(reader, "Info", String.Empty);
                        obj.BaseCnt = GetNullableInt32(reader, "BaseCnt", 0);
                        obj.ShiftQuota = GetNullableInt32(reader, "ShiftQuota", 0);
                        obj.SubRequests = GetNullableInt32(reader, "SubRequests", 0);
                        obj.Substitutes = GetNullableInt32(reader, "Substitutes", 0);
                        obj.Dropins = GetNullableInt32(reader, "Dropins", 0);
                        Results.Add(obj);

                    }
                }
            }
            return Results;

        }
    }
}
