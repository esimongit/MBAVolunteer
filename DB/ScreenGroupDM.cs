
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace NQN.DB
{	

    public class ScreenGroupDM : DBAccess<ScreenGroupObject>
	{

        public ObjectList<ScreenGroupObject> FetchForScreen(int ScreenID)
        {
            ObjectList<ScreenGroupObject> Results = new ObjectList<ScreenGroupObject>();
            string qry = @"SELECT
				ScreenID=@ScreenID
                ,r.RoleID
				,r.[RoleName] 
                ,ScreenName = cast (sg.ScreenID as varchar) 
				FROM     aspnet_Roles r left join ScreenGroup sg on sg.RoleID= r.RoleId
                 and sg.ScreenID = @ScreenID  where r.RoleName != 'admin' ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ScreenID", ScreenID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ScreenGroupObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public StaffRolesObject FetchRole(string RoleName)
        {
            StaffRolesObject obj = new StaffRolesObject();
            string qry = "select RoleId, RoleName from aspnet_Roles where RoleName = @RoleName ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("RoleName", RoleName));

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        obj.RoleID = GetNullableGuid(reader, "RoleID", Guid.Empty);
                        obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);

                    }
                }
            }
            return obj;
        }

        public List<StaffRolesObject> FetchRoles()
        {
            List<StaffRolesObject> Results = new List<StaffRolesObject>();
            string qry = "select RoleId, RoleName from aspnet_Roles where RoleName != 'admin' ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StaffRolesObject obj = new StaffRolesObject();
                        obj.RoleID = GetNullableGuid(reader, "RoleID", Guid.Empty);
                        obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);
                        Results.Add(obj);
                    }
                }
            }
            return Results;
        }
        
        public void Save(ScreenGroupObject obj)
		{
            string qry = @"INSERT INTO ScreenGroup (
				[ScreenID] ,[RoleID]  )
			SELECT
				@ScreenID, @RoleID where not exists (select 1 from ScreenGroup where ScreenID =@ScreenID and RoleID = @RoleID) ";
		 
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ScreenID",obj.ScreenID));
                myc.Parameters.Add(new SqlParameter("RoleID", obj.RoleID));
				myc.ExecuteNonQuery();
			}
		}

        public void Delete(ScreenGroupObject obj)
        {
            string qry = @"DELETE From ScreenGroup where ScreenID = @ScreenID 
                and roleID = @RoleID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("@RoleID", obj.RoleID)); 
                myc.Parameters.Add(new SqlParameter("@ScreenID", obj.ScreenID));
                myc.ExecuteNonQuery();
            }
        }

        // LoadFrom loads the Screen and Group object in ScreenGroup.
		protected override ScreenGroupObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			ScreenGroupObject obj = new ScreenGroupObject();
			obj.ScreenID = GetNullableInt32(reader, "ScreenID",0);
            obj.RoleID = GetNullableGuid(reader, "RoleID", Guid.Empty);
            obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);
            obj.ScreenHandle = GetNullableString(reader, "ScreenName", String.Empty);
            
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				sg.[ScreenID]
                ,sg.RoleID
				,r.[RoleName]
                ,s.[ScreenName]
				FROM ScreenGroup sg join aspnet_Roles r on sg.RoleID= r.RoleId
                join Screens s on sg.ScreenID = s.ScreenID ";
		}
		

	}
}
