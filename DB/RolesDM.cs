
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class RolesDM : DBAccess<RolesObject>
	{
        public RolesObject FetchRole(string RoleName)
        {
            RolesObject obj = null;
            string qry = ReadAllCommand() + " WHERE RoleName = @RoleName ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("RoleName", RoleName));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public RolesObject FetchCaptain()
        {
            RolesObject obj = null;
            string qry = ReadAllCommand() + " WHERE IsCaptain = 1 ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }

		public void Update(RolesObject obj)
		{
			 string qry = @"UPDATE  Roles SET 
				RoleName=@RoleName
				,IsCaptain=@IsCaptain
                ,IsInfo = @IsInfo
                ,MaskContactInfo=@MaskContactInfo
				 WHERE RoleID = @RoleID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID));
				myc.Parameters.Add(new SqlParameter("RoleName",obj.RoleName));
				myc.Parameters.Add(new SqlParameter("IsCaptain",obj.IsCaptain));
                myc.Parameters.Add(new SqlParameter("IsInfo", obj.IsInfo));
                myc.Parameters.Add(new SqlParameter("MaskContactInfo", obj.MaskContactInfo));
                myc.ExecuteNonQuery();
			}
		}

		public void Save(RolesObject obj)
		{
			 string qry = @"INSERT INTO Roles (
				[RoleName]
				,[IsCaptain]
                ,[IsInfo]
                ,[MaskContactInfo]
				)
			VALUES(
				@RoleName
				,@IsCaptain
                ,@IsInfo
				,@MaskContactInfo)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("RoleName",obj.RoleName));
				myc.Parameters.Add(new SqlParameter("IsCaptain",obj.IsCaptain));
                myc.Parameters.Add(new SqlParameter("IsInfo", obj.IsInfo));
                myc.Parameters.Add(new SqlParameter("MaskContactInfo", obj.MaskContactInfo));
                myc.ExecuteNonQuery();
			}
		}

        public void Delete(RolesObject obj)
        {
            Delete(obj.RoleID);
        }
		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM Roles WHERE [RoleID] = @RoleID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("RoleID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override RolesObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			RolesObject obj = new RolesObject();
			obj.RoleID = GetNullableInt32(reader, "RoleID",0);
			obj.RoleName = GetNullableString(reader, "RoleName",String.Empty);
			obj.IsCaptain = GetNullableBoolean(reader, "IsCaptain",false);
            obj.IsInfo = GetNullableBoolean(reader, "IsInfo", false);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            obj.Number = GetNullableInt32(reader, "Number", 0);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[RoleID]
				,[RoleName]
				,[IsCaptain]
                ,[IsInfo]
                ,[MaskContactInfo]
                ,Number = (select count(*) from Guides where RoleID = r.RoleID and isnull(Inactive,0) = 0)
				FROM Roles r";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('Roles')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
