
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

        public bool IsShiftOnDate(int ShiftID, DateTime dt)
        {
            string qry = ReadAllCommand() + "  where dow = datepart(dw, @dt) and ShiftID = @ShiftID ";
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

        public override ObjectList<ShiftsObject> FetchAll()
        {
            return Fetch(" order by dow, BWeek, Sequence ");
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
                ,s.[ShiftTimeID]
                ,ShiftStart=cast (t.ShiftStart as DateTime)
                , ShiftEnd=cast (t.ShiftEnd as DateTime)
				FROM Shifts s join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID left join SubOffers o on s.ShiftID = o.ShiftID and GuideID = @GuideID 
                where DOW is not null
                    order by dow, Sequence";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ShiftsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Selected =   GetNullableInt32(reader, "GuideID", 0) > 0 ;
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

          
            string qry = ReadAllCommand() + @" WHERE dow = datepart(dw, @dt)  and ((dbo.AB(@dt) = 'AWeek' and AWeek = 1)
                    or (dbo.AB(@dt) = 'BWeek' and BWeek = 1))
                order by Sequence " ;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
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
        public ObjectList<ShiftsObject> ShiftsForDateAndGuide(DateTime dt, int GuideID)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();

    
            string qry = ReadAllCommand() + @" WHERE dow = datepart(dw, @dt)  and ((dbo.AB(@dt) = 'AWeek' and AWeek = 1)
                    or (dbo.AB(@dt) = 'BWeek' and BWeek = 1))
                and ShiftID not in (select ShiftID from Guides where GuideID = @GuideID) order by Sequence ";
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

        public ObjectList<ShiftsObject> ShiftsForMonth(int Yr, int Mo)
        {            
            ObjectList<ShiftsObject> Results = new ObjectList<ShiftsObject>();

            string qry = @"SELECT
                [dt]
				,[ShiftID]
				,[ShiftName]
				,[DOW]
				,[AWeek]
				,[BWeek]
				,[Sequence]
				,[ShortName]
                ,[Captains] = ''
                ,[Info] = ''
                ,[ShiftTimeID] 
                ,[ShiftStart] = null
                ,[ShiftEnd] = null
			   FROM dbo.ShiftsForMonth(@Yr, @Mo) order by dt, Sequence ";
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
                        obj.ShiftDate = GetNullableDateTime(reader, "dt", new DateTime(2016,1,2)); 
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
				,DOW=@DOW
				,AWeek=@AWeek
				,BWeek=@BWeek
				,Sequence=@Sequence
				,ShortName=@ShortName
                ,ShiftTimeID = @ShiftTimeID
				 WHERE ShiftID = @ShiftID"
                   ;
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.Parameters.Add(new SqlParameter("ShiftName",obj.ShiftName));
				myc.Parameters.Add(new SqlParameter("DOW",obj.DOW));
				myc.Parameters.Add(new SqlParameter("AWeek",obj.AWeek));
				myc.Parameters.Add(new SqlParameter("BWeek",obj.BWeek));
				myc.Parameters.Add(new SqlParameter("Sequence",obj.Sequence));
				myc.Parameters.Add(new SqlParameter("ShortName",obj.ShortName));
                myc.Parameters.Add(new SqlParameter("ShiftTimeID", obj.ShiftTimeID));
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
				)
			VALUES(
				@ShiftName
				,@DOW
				,@AWeek
				,@BWeek
				,@Sequence
				,@ShortName
                ,@ShiftTimeID
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftName",obj.ShiftName));
				myc.Parameters.Add(new SqlParameter("DOW",obj.DOW));
				myc.Parameters.Add(new SqlParameter("AWeek",obj.AWeek));
				myc.Parameters.Add(new SqlParameter("BWeek",obj.BWeek));
				myc.Parameters.Add(new SqlParameter("Sequence",obj.Sequence));
                myc.Parameters.Add(new SqlParameter("ShortName", obj.ShortName));
                myc.Parameters.Add(new SqlParameter("ShiftTimeID", obj.ShiftTimeID));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM Shifts WHERE [ShiftID] = @ShiftID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override ShiftsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			ShiftsObject obj = new ShiftsObject();
			obj.ShiftID = GetNullableInt32(reader, "ShiftID",0);
			obj.ShiftName = GetNullableString(reader, "ShiftName",String.Empty);
			obj.DOW = GetNullableInt32(reader, "DOW",0);
			obj.AWeek = GetNullableBoolean(reader, "AWeek",false);
			obj.BWeek = GetNullableBoolean(reader, "BWeek",false);
			obj.Sequence = GetNullableInt32(reader, "Sequence",0);
			obj.ShortName = GetNullableString(reader, "ShortName",String.Empty);
            obj.Captains = GetNullableString(reader, "Captains", String.Empty);
            obj.InfoDesk = GetNullableString(reader, "Info", String.Empty);
            obj.ShiftTimeID = GetNullableInt32(reader, "ShiftTimeID", 0);
            obj.ShiftStart = GetNullableDateTime(reader, "ShiftStart", DateTime.MinValue);
            obj.ShiftEnd = GetNullableDateTime(reader, "ShiftEnd", DateTime.MinValue);
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
                ,s.ShiftTimeID
                ,ShiftStart=cast (t.ShiftStart as DateTime)
                , ShiftEnd=cast (t.ShiftEnd as DateTime)
                ,[Captains] = dbo.FlattenCaptains(ShiftID)
                ,[Info] = dbo.FlattenInfo(ShiftID)
				FROM Shifts s join ShiftTimes t on s.ShiftTimeID = t.ShiftTimeID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('Shifts')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
