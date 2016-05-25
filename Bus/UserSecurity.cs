using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Text;

namespace NQN.Bus
{
    public class UserSecurity
    {
       
        public static string GetUserName()
        {
            string uname = "script";
            try
            {
                uname = HttpContext.Current.User.Identity.Name;
            }
            catch { }
            return uname;
        }
        public static string GetRole()
        {
            return UserSecurity.GetRole(HttpContext.Current.User.Identity.Name);
        }
        //public static Guid GetRoleID()
        //{
        //    string rolename = UserSecurity.GetRole(HttpContext.Current.User.Identity.Name);
        //    RolesBusiness rb = new RolesBusiness();
        //    return rb.SelectRole().RoleID;
        //}
        public static string GetRole(string UserName)
        {
            string[] uroles = Roles.GetRolesForUser(UserName);
            if (uroles.Length > 0)
                return uroles[0];
            return null;
        }
        public bool HasAccess(string RoleName)
        {
            if (RoleName.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                return true;
            return Roles.IsUserInRole(RoleName);
        }
    }
}
