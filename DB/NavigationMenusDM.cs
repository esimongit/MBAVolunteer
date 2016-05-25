
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class NavigationMenusDM : DBAccess<NavigationMenusObject>
	{
        public ObjectList<NavigationMenusObject> FetchForRole(string Role)
        {
            ObjectList<NavigationMenusObject> Results = new ObjectList<NavigationMenusObject>();
            string qry = ReadAllCommand() + @" WHERE MenuID in (select MenuID from MenuRole
                where RoleID in (select RoleID from aspnet_Roles where RoleName = @Role)) ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Role", Role));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    NavigationMenusObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        
        public void AddRole(int MenuID, Guid RoleID)
        {
            string qry = @"insert into MenuRole( MenuID, RoleID) 
                select @MenuID, @RoleID  
                where not exists (select 1 from MenuRole where MenuID = @MenuID and RoleID = @RoleID)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MenuID", MenuID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                myc.ExecuteNonQuery();
            }
        }
        
        public void DeleteRole(int MenuID, Guid RoleID)
        {
            string qry = "DELETE from MenuRole where MenuID = @MenuID and RoleID = @RoleID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("MenuID", MenuID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                myc.ExecuteNonQuery();
            }
        }
		public void Update(NavigationMenusObject obj)
		{
			 string qry = @"UPDATE  NavigationMenus SET 
				Menu=@Menu
				 WHERE MenuID = @MenuID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MenuID",obj.MenuID));
				myc.Parameters.Add(new SqlParameter("Menu",obj.Menu));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(NavigationMenusObject obj)
		{
			 string qry = @"INSERT INTO NavigationMenus (
				[Menu]
				)
			VALUES(
				@Menu
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("Menu",obj.Menu));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM NavigationMenus WHERE [MenuID] = @MenuID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("MenuID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override NavigationMenusObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			NavigationMenusObject obj = new NavigationMenusObject();
			obj.MenuID = GetNullableInt32(reader, "MenuID",0);
			obj.Menu = GetNullableString(reader, "Menu",String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[MenuID]
				,[Menu]
				FROM NavigationMenus ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('NavigationMenus')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
