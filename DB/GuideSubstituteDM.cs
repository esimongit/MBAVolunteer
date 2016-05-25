
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
            string qry = ReadAllCommand() + " WHERE g.ShiftID = @ShiftID and SubDate = @dt order by g.VolID";
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
        // YOu can only ask for a sub for your home shift, so no need to specify the shift.
        public  GuideSubstituteObject FetchForGuide(int GuideID, DateTime dt )
        {
            GuideSubstituteObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.GuideID = @GuideID and SubDate = @dt ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("dt", dt)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                     obj = LoadFrom(reader); 
                }
            }
            return obj;
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
            string qry = ReadAllCommand() + " WHERE s.SubstituteID = @SubstituteID and SubDate = @dt   and g.ShiftID = @ShiftID ";
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
            string qry = ReadAllCommand() + " WHERE s.SubstituteID = @SubstituteID and SubDate >= getdate()";
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
        public ObjectList<GuideSubstituteObject> FetchForDate( DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return null;
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            string qry = ReadAllCommand() + " WHERE  SubDate = @dt order by h.Sequence, g.VolID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
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

        public  ObjectList<GuideSubstituteObject> FetchForMonth(int Year, int Month)
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
             string qry = ReadAllCommand() +  @" where month(SubDate) = @Month and Year(SubDate) = @Year order by SubDate";
             using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                  myc.Parameters.Add(new SqlParameter("Year", Year));
                myc.Parameters.Add(new SqlParameter("Month", Month));
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
            string qry = ReadAllCommand() + @" where SubDate > getdate() order by SubDate";
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
				,[SubstituteID]
				,[DateEntered]
				)
			VALUES(
				@GuideID
				,@SubDate
				,nullif(@SubstituteID,0)
				,@DateEntered
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("SubDate",obj.SubDate));
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
            string qry = @"DELETE GuideSubstitute   WHERE GuideID = @GuideID; 
                UpdateGuideSubstitute set SubstituteID = null where SubstituteID = @GuideID ";
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
            obj.Sequence = GetNullableInt32(reader, "Sequence",0);
            obj.FirstName = GetNullableString(reader, "FirstName", String.Empty);
            obj.LastName = GetNullableString(reader, "LastName", String.Empty);
            obj.VolID = GetNullableString(reader, "VolID", String.Empty);
            obj.Role = GetNullableString(reader, "Role", String.Empty);
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
                ,g.ShiftID
				,h.ShiftName
				,h.Sequence
				,FirstName =   g.FirstName  
				,g.LastName
                ,g.Email
                ,g.Phone
				,g.VolID
				,Role = r.RoleName
				,Sub = g2.VolID
				,SubFirst =  g2.FirstName 
				,SubLast = g2.LastName
                ,SubEmail = g2.Email
                ,SubPhone = g2.Phone
                ,SubRole = r2.RoleName
				FROM GuideSubstitute s join Guides g on s.GuideID = g.GuideID
				join Shifts h on h.ShiftID = g.ShiftID
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
       

             

	}
}
