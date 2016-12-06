
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class GuideRoleDM : DBAccess<GuideRoleObject>
	{
        public ObjectList<GuideRoleObject> FetchForGuide(int GuideID)
        {
            ObjectList<GuideRoleObject> Results = new ObjectList<GuideRoleObject>();
            string qry = ReadAllCommand() + " WHERE GuideID = @GuideID  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuideRoleObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        public bool HasRole(int GuideID, string Role)
        {
            bool ret = false;
            string qry = @"select cast (1 as bit) from GuideRole gr join Roles r on gr.RoleID = r.RoleID where GuideID = GuideID and r.RoleName = @Role";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("Role", Role));
                try
                {
                    ret = Convert.ToBoolean(myc.ExecuteScalar());
                }
                catch { }
            }
            return ret;
        }
        public void Save(GuideRoleObject obj)
		{
			 string qry = @"INSERT INTO GuideRole (
				[GuideID]
				,[RoleID]
				,[Condition]
				)
			VALUES(
				@GuideID
				,@RoleID
				,@Condition
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID));
				myc.Parameters.Add(new SqlParameter("Condition",obj.Condition));
				myc.ExecuteNonQuery();
			}
		}

		public void DeleteRole(int GuideID, int RoleID)
        {
            string qry = "Delete from GuideRole where GuideID = @GuideID and RoleID = @RoleID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID)); 
                myc.ExecuteNonQuery();
            }
        }

		protected override GuideRoleObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			GuideRoleObject obj = new GuideRoleObject();
			obj.GuideID = GetNullableInt32(reader, "GuideID",0);
			obj.RoleID = GetNullableInt32(reader, "RoleID",0);
			obj.Condition = GetNullableInt32(reader, "Condition",0);
            obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[GuideID]
				,gr.[RoleID]
				,[Condition]
                ,r.RoleName
				FROM GuideRole gr join Roles r on gr.RoleID = r.RoleID";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('GuideRole')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
