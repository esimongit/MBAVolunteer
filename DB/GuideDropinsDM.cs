
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class GuideDropinsDM : DBAccess<GuideDropinsObject>
	{
        public ObjectList<GuideDropinsObject> FetchForShift(int ShiftID, DateTime dt)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + " WHERE d.ShiftID = @ShiftID and DropinDate = @dt order by d.GuideID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideDropinsObject> FetchForDate(DateTime dt)
        {
           
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            if (dt == DateTime.MinValue)
                return Results;
            string qry = ReadAllCommand() + " WHERE  DropinDate = @dt order by s.Sequence, d.GuideID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideDropinsObject> FetchForGuide(int GuideID, DateTime dt)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + " WHERE  DropinDate = @dt and d.GuideID = @GuideID order by s.Sequence  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public GuideDropinsObject FetchForGuide(int GuideID, DateTime dt, int ShiftID)
        {
            GuideDropinsObject obj = null;
            string qry = ReadAllCommand() + " WHERE  DropinDate = @dt and d.GuideID = @GuideID and d.ShiftID = @ShiftID order by s.Sequence  ";
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

        public ObjectList<GuideDropinsObject> FetchAllForGuide(int GuideID, int ShiftID )
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + @" WHERE  DropinDate >= getdate() and d.GuideID = @GuideID 
                        and d.ShiftID = @ShiftID ";
            qry += " order by DropinDate, s.Sequence  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public ObjectList<GuideDropinsObject> FetchAllForGuide(int GuideID,  bool SpecialsOnly)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + " WHERE  DropinDate >= getdate() and d.GuideID = @GuideID ";
            if (SpecialsOnly)
                qry += " and s.recurring = 0 ";
            qry += " order by DropinDate, s.Sequence  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public ObjectList<GuideDropinsObject> FetchForMonth(int Year, int Month, int GuideID)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + @" where month(DropinDate) = @Month and Year(DropinDate) = @Year and d.GuideID = @GuideID
                order by DropinDate";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Year", Year));
                myc.Parameters.Add(new SqlParameter("Month", Month));
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideDropinsObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

		public void Update(GuideDropinsObject obj)
		{
			 string qry = @"UPDATE  GuideDropins SET 
				GuideID=@GuideID
				,DropinDate=@DropinDate
				,ShiftID=@ShiftID
				 WHERE GuideDropinID = @GuideDropinID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideDropinID",obj.GuideDropinID));
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("DropinDate",obj.DropinDate));
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.ExecuteNonQuery();
			}
		}

        public bool AddSpecial(int GuideID, int ShiftID)
        {
            ShiftsDM dm = new ShiftsDM();
            ShiftsObject obj = dm.FetchShift(ShiftID);
            Save(GuideID, ShiftID, obj.ShiftDate);
            return true;
        }

        public void Save(int GuideID, int ShiftID, DateTime dt)
        {
            GuideDropinsObject obj = new GuideDropinsObject();
            obj.GuideID = GuideID;
            obj.ShiftID = ShiftID;
            obj.DropinDate = dt;
            Save(obj);
        }

		public void Save(GuideDropinsObject obj)
		{
			 string qry = @"INSERT INTO GuideDropins (
				[GuideID]
				,[DropinDate]
				,[ShiftID]
				)
			SELECT
				@GuideID
				,@DropinDate
				,@ShiftID
				 where not exists (select 1 from GuideDropins where DropinDate = @DropinDate and GuideID = @GuideID and ShiftID = @ShiftID) "; ;
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("DropinDate",obj.DropinDate));
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.ExecuteNonQuery();
			}
		}

        public void Delete(int GuideDropinID)
		{
			string qry = @"DELETE FROM GuideDropins WHERE [GuideDropinID] = @GuideDropinID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideDropinID", GuideDropinID));
				 myc.ExecuteNonQuery();
			}
		}

        public void DeleteSpecial(int GuideID, int ShiftID)
        {
            string qry = @"DELETE FROM GuideDropins WHERE GuideID = @GuideID and ShiftID = @ShiftID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.ExecuteNonQuery();
            }
        }
   
        public void DeleteForGuideAndDate(int GuideID, DateTime dt)
        {
            string qry = @"DELETE FROM GuideDropins WHERE GuideID = @GuideID and DropinDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.ExecuteNonQuery();
            }
        }
 
		protected override GuideDropinsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			GuideDropinsObject obj = new GuideDropinsObject();
			obj.GuideDropinID = GetNullableInt32(reader, "GuideDropinID",0);
			obj.GuideID = GetNullableInt32(reader, "GuideID",0);
			obj.DropinDate = GetNullableDateTime(reader, "DropinDate",new DateTime());
			obj.ShiftID = GetNullableInt32(reader, "ShiftID",0);
            obj.FirstName = GetNullableString(reader, "FirstName", String.Empty);
            obj.LastName = GetNullableString(reader, "LastName", String.Empty);
            obj.VolID = GetNullableString(reader, "VolID", String.Empty);
            obj.Role = GetNullableString(reader, "Role", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.Email = GetNullableString(reader, "Email", String.Empty);
            obj.Phone = GetNullableString(reader, "Phone", String.Empty);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
					[GuideDropinID]
				,d.[GuideID]
				,[DropinDate]
				,d.[ShiftID]
				,g.FirstName  
				,g.LastName
                ,g.Email
                ,g.Phone
				,g.VolID
				,Role= r.RoleName
                ,r.MaskContactInfo
                ,s.Sequence
                ,s.ShiftName
				FROM GuideDropins d join Guides g on d.GuideID= g.GuideID
				join Roles r on r.RoleID = g.RoleID
                join Shifts s on d.ShiftID= s.ShiftID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('GuideDropins')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
