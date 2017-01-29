using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class RolesBusiness
    {
        public const string  ADMINROLE = "VolAdmin";

        public RolesBusiness()
        {
            Roles.ApplicationName = "VolManager";
        }
        // Create the role if it is missing. return the number of users in the role
        public int VerifyAdminRole()
        {
            if (!Roles.RoleExists(ADMINROLE))
            {
                Insert(ADMINROLE, true);
            }
            return Roles.GetUsersInRole(ADMINROLE).Length;
        }
        public bool HasAdminRole()
        {
            return GetRole().Equals(ADMINROLE, StringComparison.OrdinalIgnoreCase);
        }
        public  string GetRole()
        {
            return Roles.GetRolesForUser()[0];
        }
       
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RoleName"));

            foreach (string role in Roles.GetAllRoles())
            {
                DataRow row = dt.NewRow();
                row["RoleName"] = role;
                dt.Rows.Add(row);
            }
            return dt;
        }
      
        public void Insert(string RoleName, bool IsAdmin)
        {
            if (Roles.RoleExists(RoleName))
                throw new Exception("Role exists");

            Roles.CreateRole(RoleName);
            
        }
        public void Delete(string RoleName)
        {
            if (RoleName == null) return;
            if (!Roles.RoleExists(RoleName)) return;
            string[] userInRole = Roles.GetUsersInRole(RoleName);
            if (userInRole.Length > 0)
                throw new Exception("Role has members and may not be deleted.");
            if (!Roles.DeleteRole(RoleName))
                throw new Exception("Delete Role failed");
        }
        public DataTable UsersForRole(string RoleName)
        {
            DataTable dt = new DataTable();
            if (RoleName == null) return dt;
             dt.Columns.Add(new DataColumn("UserName"));
            foreach (string uname in Roles.GetUsersInRole(RoleName))
            {
                DataRow row = dt.NewRow();
                row["UserName"] = uname;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
