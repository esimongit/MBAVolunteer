
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
            string qry = ReadAllCommand() + " WHERE d.ShiftID = @ShiftID and DropinDate = @dt and isnull(OnShift, 0) = 0  order by d.GuideID ";
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
        public ObjectList<GuideDropinsObject> FetchOnShift(int ShiftID, DateTime dt, int RoleID)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + " WHERE d.ShiftID = @ShiftID and DropinDate = @dt and g.IrregularSchedule = 1 and OnShift = 1 ";
           
            qry += " order by d.GuideID ";
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
                        if (RoleID == 0 || RoleID == obj.RoleID)
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
            string qry = ReadAllCommand() + " WHERE  DropinDate = @dt and isnull(OnShift,0) = 0 and isnull(IrregularSchedule,0) = 0 order by s.Sequence, d.GuideID ";
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
        public ObjectList<GuideDropinsObject> FetchFuture()
        {

            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
           
            string qry = ReadAllCommand() + " WHERE  DropinDate >= cast(getdate() as date)  order by DropinDate, s.Sequence, g.FirstName ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
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
            string qry = ReadAllCommand() + " WHERE  DropinDate = @dt and d.GuideID = @GuideID and isnull(OnShift,0) = 0  order by s.Sequence ";
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

        public ObjectList<GuideDropinsObject> FetchOnShiftForGuide(int GuideID, int ShiftID, int RoleID )
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + @" WHERE  DropinDate >= getdate() and d.GuideID = @GuideID 
                        and d.ShiftID = @ShiftID and OnShift = 1 ";
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
                        if (RoleID == 0 || RoleID == obj.RoleID)
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
            string qry = ReadAllCommand() + " WHERE  DropinDate >= getdate() and d.GuideID = @GuideID and isnull(OnShift,0) = 0 ";
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
        // Get All Drop-ins including OnShift (Irregular Guides)
        public ObjectList<GuideDropinsObject> FetchForMonth(int Year, int Month, int GuideID, int RoleID)
        {
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            string qry = ReadAllCommand() + @" where month(DropinDate) = @Month and Year(DropinDate) = @Year and d.GuideID = @GuideID ";
            if (RoleID > 0)
                qry += " and d.RoleID = @RoleID ";
            qry += "    order by DropinDate";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Year", Year));
                myc.Parameters.Add(new SqlParameter("Month", Month));
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
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

        public int FutureDropins()
        {
            int ret = 0;
            string qry = @"select count(*) from GuideDropins where DropinDate >= convert(date,getdate()) ";
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
		public void Update(GuideDropinsObject obj)
		{
			 string qry = @"UPDATE  GuideDropins SET 
				GuideID=@GuideID
				,DropinDate=@DropinDate
				,ShiftID=@ShiftID
                ,OnShift=@OnShift
                ,RoleID = @RoleID
				 WHERE GuideDropinID = @GuideDropinID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideDropinID",obj.GuideDropinID));
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("DropinDate",obj.DropinDate));
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
                myc.Parameters.Add(new SqlParameter("OnShift", obj.OnShift));
                myc.Parameters.Add(new SqlParameter("RoleID", obj.RoleID));
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
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(GuideID);
            GuideDropinsObject obj = new GuideDropinsObject();
            obj.GuideID = GuideID;
            obj.ShiftID = ShiftID;
            obj.DropinDate = dt;
            obj.RoleID = guide.RoleID;
            Save(obj);
        }
        public void SaveOnShift(int GuideID, int ShiftID, DateTime dt, int RoleID)
        {
            GuideDropinsObject obj = new GuideDropinsObject();
            obj.GuideID = GuideID;
            obj.ShiftID = ShiftID;
            obj.OnShift = true;
            obj.DropinDate = dt;
            obj.RoleID = RoleID;
            Save(obj);
        }
        public void Save(GuideDropinsObject obj)
		{
			 string qry = @"INSERT INTO GuideDropins (
				[GuideID]
				,[DropinDate]
				,[ShiftID]
                ,[OnShift]
                ,[RoleID]
				)
			SELECT
				@GuideID
				,@DropinDate
				,@ShiftID
                ,isnull(@OnShift,0)
                ,nullif(@RoleID,0)
				 where not exists (select 1 from GuideDropins where DropinDate = @DropinDate and GuideID = @GuideID and ShiftID = @ShiftID) "; ;
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("DropinDate",obj.DropinDate));
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
                myc.Parameters.Add(new SqlParameter("OnShift", obj.OnShift));
                myc.Parameters.Add(new SqlParameter("RoleID", obj.RoleID));
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
   
        public void DeleteForGuideAndDate(int GuideID, int ShiftID, DateTime dt)
        {
            string qry = @"DELETE FROM GuideDropins WHERE ShiftID = @ShiftID and  GuideID = @GuideID and DropinDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("dt", dt));
                myc.ExecuteNonQuery();
            }
        }
        // removes all dropins for a date on this guide
        public void DeleteForGuideAndDate(int GuideID,   DateTime dt)
        {
            string qry = @"DELETE FROM GuideDropins WHERE  GuideID = @GuideID and DropinDate = @dt ";
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
            obj.RoleID = GetNullableInt32(reader, "RoleID", 0);
            obj.Role = GetNullableString(reader, "Role", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.Email = GetNullableString(reader, "Email", String.Empty);
            obj.Phone = GetNullableString(reader, "Phone", String.Empty);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            obj.OnShift = GetNullableBoolean(reader, "OnShift", false);
            obj.IsInfo = GetNullableBoolean(reader, "IsInfo", false);
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
                ,Phone = case g.CellPreferred WHEN 1 THEN g.Cell ELSE g.Phone END
				,g.VolID
                ,RoleID = case isnull(d.RoleID,0) WHEN 0 THEN g.RoleID ELSE d.RoleID END
				,Role=  case isnull(d.RoleID,0) WHEN 0 THEN (select RoleName from Roles WHERE RoleID = g.RoleID)
                    ELSE (select RoleName from Roles where RoleID = d.RoleID) END
                ,IsInfo= case isnull(d.RoleID,0) WHEN 0 THEN (select IsInfo from Roles WHERE RoleID = g.RoleID)
                    ELSE (select IsInfo from Roles where RoleID = d.RoleID) END
                ,d.OnShift 
                ,MaskContactInfo = (select MaskContactInfo from Roles where RoleID = g.RoleID) | g.MaskPersonalInfo
                ,s.Sequence
                ,s.ShiftName
				FROM GuideDropins d join Guides g on d.GuideID= g.GuideID
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
