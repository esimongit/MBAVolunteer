
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class GuideSubstituteDM : DBAccess<GuideSubstituteObject>
	{
        public ObjectList<GuideSubstituteObject> FetchForShift(int ShiftID, DateTime dt)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE s.ShiftID = @ShiftID and SubDate = @dt order by g.VolID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
       
        public  GuideSubstituteObject FetchForGuide(int GuideID, int ShiftID,  DateTime dt )
        {
            GuideSubstituteObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.GuideID = @GuideID and s.ShiftID = @ShiftID and SubDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                     obj = LoadFrom(reader); 
                }
            }
            return obj;
        }

        public ObjectList<GuideSubstituteObject> FetchForGuide(int GuideID, int ShiftID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE g.GuideID = @GuideID and s.ShiftID = @ShiftID and SubDate > getdate() ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> FetchAllForGuide(int GuideID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE s.GuideID = @GuideID and SubDate >= getdate()";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> FetchForSub(int SubstituteID, DateTime dt )
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE s.SubstituteID = @SubstituteID and SubDate = @dt ";
 
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("SubstituteID", SubstituteID));
                myc.Parameters.Add(new SqlParameter("dt", dt)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public  GuideSubstituteObject FetchBySub(int SubstituteID, DateTime dt, int ShiftID)
        {
            GuideSubstituteObject obj = null;
            string qry = ReadAllCommand() + " WHERE s.SubstituteID = @SubstituteID and SubDate = @dt   and s.ShiftID = @ShiftID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("SubstituteID", SubstituteID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                     obj = LoadFrom(reader);                   
                }
            }
            return obj;
        }

        public ObjectList<GuideSubstituteObject> FetchAllForSub(int SubstituteID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE s.SubstituteID = @SubstituteID and SubDate >= cast(getdate() as date)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("SubstituteID", SubstituteID)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> FetchForDate( DateTime dt, int RoleID)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE  SubDate = @dt ";
            if (RoleID > 0)
                qry += " and g.RoleID = @RoleID ";
            qry += " order by h.Sequence, g.VolID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public int Attendance(DateTime dt, int ShiftID)
        {
            int ret = 0;
            string qry = "select Attendance = dbo.ShiftAttendance(@dt, @ShiftID)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                try
                {
                    ret = Convert.ToInt32(myc.ExecuteScalar());
                }
                catch { }
            }
            return ret;
        }

        public ObjectList<GuideSubstituteObject> FetchForMonth(int Year, int Month, int RoleID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + @" where month(SubDate) = @Month and Year(SubDate) = @Year ";
            if (RoleID > 0)
                qry += " and g.RoleID = @RoleID ";
            qry += " order by SubDate";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Year", Year));
                myc.Parameters.Add(new SqlParameter("Month", Month));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            int WeekendCritical = Convert.ToInt32(StaticFieldsObject.StaticValue("WeekendCritical"));
            int WeekdayCritical = Convert.ToInt32(StaticFieldsObject.StaticValue("WeekdayCritical"));
            for (int i = 0; i < Results.Count; i++)
            {
                Results[i].Attendance = Attendance(Results[i].SubDate, Results[i].ShiftID);
                Results[i].Critical = false;
                if (Results[i].SubDate.DayOfWeek == DayOfWeek.Saturday || Results[i].SubDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (Results[i].Attendance < WeekendCritical)
                        Results[i].Critical = true;
                }
                else
                {
                    if (Results[i].Attendance < WeekdayCritical)
                        Results[i].Critical = true;
                }
            }
            return Results;
        }

        // For CalendarList on mbav
        public ObjectList<GuideSubstituteObject> FetchAllRequests(int SubstituteID, int RoleID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + @" where SubDate > convert(date,getdate()) 
			
				and not Exists (select 1 from GuideSubstitute s2 join (Guides g3 join GuideShift gs on g3.GuideID = gs.GuideID) on s2.GuideID = g3.GuideID
					 where s2.SubstituteID = @GuideID and s2.SubDate = s.SubDate and gs.ShiftID = s.ShiftID)
                and not Exists (select 1 from GuideDropins where GuideID = @GuideID and DropinDate = s.SubDate and ShiftID = s.ShiftID) ";
				
            if (RoleID > 0)
                qry += "	and g.RoleID = @RoleID ";
            qry += "order by SubDate, Sequence, g.FirstName "; 
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", SubstituteID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        // Fetch all open requests for shifts in which our Sub has expressed an interest (SubOffers)
        // but this Sub has not yet offered to sub for anybody on that Shift and date
        public ObjectList<GuideSubstituteObject> FetchRequests(int SubstituteID)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + @" where SubDate > convert(date,getdate()) and SubstituteID is null 
				and s.ShiftID in (select ShiftID from SubOffers o where o.GuideID = @GuideID)
				and not Exists (select 1 from GuideSubstitute s2 join (Guides g3 join GuideShift gs on g3.GuideID = gs.GuideID) on s2.GuideID = g3.GuideID
					 where s2.SubstituteID = @GuideID and s2.SubDate = s.SubDate and gs.ShiftID = s.ShiftID)
                and not Exists (select 1 from GuideDropins where GuideID = @GuideID and DropinDate = s.SubDate and ShiftID = s.ShiftID)
				order by SubDate, Sequence, g.FirstName";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", SubstituteID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> SubReport()
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + @" where SubDate >= cast(getdate() as date) order by SubDate, Sequence, g.FirstName";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public int OpenRequests()
        {
            int ret = 0;
            string qry = @"select count(*) from GuideSubstitute where SubDate >= convert(date,getdate()) and SubstituteID is null";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                try
                {
                    ret = Convert.ToInt32(myc.ExecuteScalar());
                }
                catch { }

            }
            return ret;
        }
        public int RequestsWithSubs()
        {
            int ret = 0;
            string qry = @" select count(*) from GuideSubstitute where SubDate >= convert(date,getdate()) and SubstituteID is not null";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                try
                {
                    ret = Convert.ToInt32(myc.ExecuteScalar());
                }
                catch { }

            }
            return ret;
        }
        public ObjectList<GuideSubstituteObject> RequestHistory(int GuideID)
        {
            string qry = ReadAllCommand() + " where s.GuideID = @GuideID order by Subdate";
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.HasSub = (obj.SubstituteID != 0);
                        obj.LeadTime = obj.SubDate.Subtract(obj.DateEntered).Days;
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> SubHistory(int GuideID)
        {
            string qry = ReadAllCommand() + " where s.SubstituteID = @GuideID order by Subdate";
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideSubstituteObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public void Update(GuideSubstituteObject obj)
		{
            
            GuidesDM dm = new GuidesDM();
            GuidesObject Guide = dm.FetchGuide(obj.SubstituteID);
            if (Guide != null)
            {
                if (Guide.ShiftID == obj.ShiftID)
                {
                    throw new Exception("You may not substitute on your own shift");
                }
            }
			 string qry = @"UPDATE  GuideSubstitute SET 
				GuideID=@GuideID
				,SubDate=@SubDate
                ,ShiftID = @ShiftID
				,SubstituteID=nullif(@SubstituteID,0)
				,DateEntered=@DateEntered
				 WHERE GuideSubstituteID = @GuideSubstituteID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideSubstituteID",obj.GuideSubstituteID));
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("SubDate",obj.SubDate));
				myc.Parameters.Add(new SqlParameter("SubstituteID",obj.SubstituteID));
                myc.Parameters.Add(new SqlParameter("ShiftID", obj.ShiftID));
                myc.Parameters.Add(new SqlParameter("DateEntered",obj.DateEntered));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(GuideSubstituteObject obj)
		{
            if (obj.SubstituteID == obj.GuideID)
                throw new Exception("You may not substitute for yourself");
			 string qry = @"INSERT INTO GuideSubstitute (
				[GuideID]
				,[SubDate]
                ,[ShiftID]
				,[SubstituteID]
				,[DateEntered]
				)
			VALUES(
				@GuideID
				,@SubDate
                ,@ShiftID
				,nullif(@SubstituteID,0)
				,@DateEntered
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("SubDate",obj.SubDate));
                myc.Parameters.Add(new SqlParameter("ShiftID", obj.ShiftID));
                myc.Parameters.Add(new SqlParameter("SubstituteID",obj.SubstituteID));
				myc.Parameters.Add(new SqlParameter("DateEntered",obj.DateEntered));
				myc.ExecuteNonQuery();
			}
		}

        public void Delete(int GuideSubstituteID)
		{
			string qry = @"DELETE FROM GuideSubstitute WHERE [GuideSubstituteID] = @GuideSubstituteID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideSubstituteID", GuideSubstituteID));
				 myc.ExecuteNonQuery();
			}
		}
        public void DeleteForSubAndDate(int SubstituteID, DateTime dt)
        {
            string qry = @"UPDATE GuideSubstitute Set SubstituteID = null WHERE SubstituteID = @SubstituteID and SubDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("SubstituteID", SubstituteID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.ExecuteNonQuery();
            }
        }
        public void DeleteForGuideAndDate(int GuideID, DateTime dt)
        {
            string qry = @"DELETE GuideSubstitute   WHERE GuideID = @GuideID and SubDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.ExecuteNonQuery();
            }
        }
        public void DeleteAllForGuide(int GuideID)
        {
            string qry = @"DELETE GuideSubstitute   WHERE GuideID = @GuideID  and SubDate > getdate();
                Update GuideSubstitute set SubstituteID = null where SubstituteID = @GuideID and SubDate > getdate() ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID)); 
                myc.ExecuteNonQuery();
            }
        }
		protected override GuideSubstituteObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			GuideSubstituteObject obj = new GuideSubstituteObject();
			obj.GuideSubstituteID = GetNullableInt32(reader, "GuideSubstituteID",0);
			obj.GuideID = GetNullableInt32(reader, "GuideID",0);
			obj.SubDate = GetNullableDateTime(reader, "SubDate",new DateTime());
			obj.SubstituteID = GetNullableInt32(reader, "SubstituteID",0);
			obj.DateEntered = GetNullableDateTime(reader, "DateEntered",new DateTime());
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.ShortName = GetNullableString(reader, "ShortName", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence",0);
            obj.FirstName = GetNullableString(reader, "FirstName", String.Empty);
            obj.LastName = GetNullableString(reader, "LastName", String.Empty);
            obj.VolID = GetNullableString(reader, "VolID", String.Empty);
            obj.Role = GetNullableString(reader, "Role", String.Empty);
            obj.IsInfo = GetNullableBoolean(reader, "IsInfo", false);
            obj.ShiftID = GetNullableInt32(reader, "ShiftID", 0);
             obj.Sub = GetNullableString(reader, "Sub", String.Empty);
             obj.SubFirst = GetNullableString(reader, "SubFirst", String.Empty);
             obj.SubLast = GetNullableString(reader, "SubLast", String.Empty);
                 obj.Email = GetNullableString(reader, "Email", String.Empty);
             obj.Phone = GetNullableString(reader, "Phone", String.Empty);
             obj.SubEmail = GetNullableString(reader, "SubEmail", String.Empty);
             obj.SubPhone = GetNullableString(reader, "SubPhone", String.Empty);
             obj.SubRole = GetNullableString(reader, "SubRole", String.Empty);
            obj.NoSub = (obj.SubstituteID == 0);
            obj.HasSub = (obj.SubstituteID > 0);
            obj.CanSub = obj.NoSub;
            obj.HasName = true;
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[GuideSubstituteID]
				,s.[GuideID]
				,[SubDate]
				,[SubstituteID]
				,[DateEntered]
                ,s.ShiftID
				,h.ShiftName
                ,h.ShortName
				,h.Sequence
				,FirstName =   g.FirstName  
				,g.LastName
                ,g.Email
                ,Phone = case g.CellPreferred WHEN 1 THEN g.Cell ELSE g.Phone END
				,g.VolID
				,Role = r.RoleName
                ,r.IsInfo 
                ,MaskContactinfo = r.MaskContactInfo
				,Sub = g2.VolID
				,SubFirst =  g2.FirstName 
				,SubLast = g2.LastName
                ,SubEmail = g2.Email
                ,SubPhone = g2.Phone
                ,SubRole = r2.RoleName
				FROM GuideSubstitute s join Guides g on s.GuideID = g.GuideID
				join Shifts h on h.ShiftID = s.ShiftID
				join Roles r on r.RoleID = g.RoleID
				left join (Guides g2 join Roles r2 on g2.RoleID = r2.RoleID) on g2.GuideID = s.SubstituteID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('GuideSubstitute')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}
        public ObjectList<GuideSubstituteObject> SubstituteReport(DateTime StartDate, DateTime EndDate)
        {
            string qry = @"select  guidid,volid, firstname, lastname,
                subs = (select count(*) from GuideSubstitute s where GuideID = g.GuideID and SubDate between @StartDate and @EndDate) ,
                Zerodays = (select count(*)   from GuideSubstitute s where GuideID = g.GuideID and SubDate between @StartDate and @EndDate
                  and datediff(Day, DateEntered,SubDate) = 0),
                MinDays = (select  min(datediff(Day, DateEntered,SubDate)) from GuideSubstitute s where GuideID = g.GuideID and SubDate between @StartDate and @EndDate) ,
                MaxDays = (select  max(datediff(Day, DateEntered,SubDate)) from GuideSubstitute s where GuideID = g.GuideID and SubDate between @StartDate and @EndDate) 
                 from Guides g
                 order by lastname";
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
             
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("StartDate", StartDate));
                myc.Parameters.Add(new SqlParameter("EndDate", EndDate));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GuideSubstituteObject obj = new GuideSubstituteObject();
                        obj.FirstName = GetNullableString(reader, "FirstName", String.Empty);
                        obj.LastName = GetNullableString(reader, "LastName", String.Empty);
                        obj.VolID = GetNullableString(reader, "VolID", String.Empty);
                        obj.GuideID = GetNullableInt32(reader, "GuideID", 0);
                        obj.Subs = GetNullableInt32(reader, "Subs", 0);
                        obj.ZeroDays = GetNullableInt32(reader, "ZeroDays", 0);
                        obj.MinDays = GetNullableInt32(reader, "MinDays", 0);
                        obj.MaxDays = GetNullableInt32(reader, "MaxDays", 0);
                         
                        Results.Add(obj);
                       
                    }
                }
            }
            return Results;
        }

             

	}
}
