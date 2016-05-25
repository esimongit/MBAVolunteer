using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{

    /// <summary>
    /// Summary description for PagePrivilege
    /// </summary>
    public class PagePrivilege
    {
        
 
        RoleList AllRoles;
        RoleList MyRoles;
        public ScreenList AllScreens;
        SortedDictionary<string, RoleList> privs;

        public PagePrivilege()
        {
            Populate();
        }
        public SortedDictionary<string, RoleList> Privs
        {
            get { return privs; }
            set { privs = value; }
        }
       
        public string ConvertToHandle(string screen)
        {
            return (screen.Contains(".")) ? screen.Substring(2, screen.IndexOf('.') - 2) : screen;

        }
      
        private void Populate()
        {
            if (!Roles.Enabled || Roles.ApplicationName == null)
                return;
            privs = new SortedDictionary<string, RoleList>();
            ScreenGroupDM dm = new ScreenGroupDM();

            GetAllScreens();
             GetAllRoles();
             GetMyRoles();
            foreach (ScreenGroupObject rpo in dm.FetchAll())
            {
                if (!privs.ContainsKey(rpo.ScreenHandle))
                {
                    privs.Add(rpo.ScreenHandle, new RoleList());
                    
                }
                privs[rpo.ScreenHandle].Add(rpo.RoleName);
            }
        }
      
      
        protected void GetMyRoles()
        {
            
             if (MyRoles == null)
             {
                 if (HttpContext.Current.Session["Roles"] == null)
                 {
                     MyRoles = new RoleList(Roles.GetRolesForUser());

                     HttpContext.Current.Session["Roles"] = MyRoles;
                 }
                 else
                 {
                     MyRoles = (RoleList) HttpContext.Current.Session["Roles"];
                 }
            }

        }
        public  RoleList GetAllRoles()
        {
            if (AllRoles == null)
            {
                if (HttpContext.Current.Session["AllRoles"] == null)
                {
                    AllRoles = new RoleList(Roles.GetAllRoles());

                    HttpContext.Current.Session["AllRoles"] = AllRoles;
                }
                else
                {
                    AllRoles = (RoleList)HttpContext.Current.Session["AllRoles"];
                }
            }
            return AllRoles;
 
          
        }
        public ScreenList GetAllScreens()
        {
            if (AllScreens == null)
            {
                if (HttpContext.Current.Session["Screens"] == null)
                {
                    ScreensDM dm = new ScreensDM();
                    AllScreens = new ScreenList(dm.FetchForPrivs());
                   
                    HttpContext.Current.Session["Screens"] = AllScreens;
                }
                else
                {
                    AllScreens = (ScreenList) HttpContext.Current.Session["Screens"];
                }
            }
            return AllScreens;

        }
       

        public ObjectList<ScreensObject> FetchForEdit()
        {
            ScreensDM dm = new ScreensDM();
            ScreenGroupDM sdm = new ScreenGroupDM();
            ObjectList<ScreensObject> dList = dm.FetchForPrivs();
            for (int i = 0; i < dList.Count; i++)
            {
                dList[i].Roles = sdm.FetchForScreen(dList[i].ScreenID);
            }
            return dList;
        }
        

        protected void AddGroup(string ScreenHandle, string role, bool HasGroup)
        {
            if (!HasGroup) return;
            if (role == "Admin") return;
            
            ScreenGroupObject rpo = new ScreenGroupObject();
            rpo.ScreenID = AllScreens[ScreenHandle].ScreenID ;
            rpo.RoleName = role;
            ScreenGroupDM dm = new ScreenGroupDM();
            dm.Save(rpo);
        }
        public void ClearPrivs()
        {
            HttpContext.Current.Session.Remove("Privs");
        }
        public bool HasPriv()
        {
            return HasPriv(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath);
        }
        public bool HasPriv(string handle)
        {
            bool UsePrivs = Convert.ToBoolean(ConfigurationManager.AppSettings["UsePrivs"]);
            if (!UsePrivs) return true;
            bool ret = false;
            if (handle == String.Empty)
            {
                handle = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
            }
            handle = StripPath(handle);
            if (MyRoles.Contains( "Admin"))
            {
                ret = true;
            }
             
            else if (!privs.ContainsKey(handle))
            {
                ret = false;
            }
            else
            {
                RoleList rp = privs[handle];
                foreach (string role in MyRoles)
                {
                    if (rp.Contains(role))
                    {
                        ret = true;
                        break;
                    }
                }
            }
           // PageAuditStatic.Register(UserSecurity.GetUserName(), ScreenID, ret);
           
            return ret;
        }
        // This class is a singleton.  Need a factory
        public static PagePrivilege PagePrivilegeFactory()
        {

            if (HttpContext.Current.Session["Privs"] == null)
            {
                PagePrivilege pp = new PagePrivilege();

                HttpContext.Current.Session["Privs"] = pp;
                return pp;

            }
            else
            {
                return (PagePrivilege)HttpContext.Current.Session["Privs"];

            }
        }
       

        protected string StripPath(string handle)
        {
            return handle.Replace("~/", "").Replace(".aspx", "");
        }
    }
    public class RoleList : List<string>
    {
        public RoleList()
        {
        }
        public RoleList(string[] rolearray)
        {
            AddRange(rolearray);
        }
    }

    public class ScreenList : Dictionary<string, ScreensObject>
    {
        public ScreenList()
        {
        }
        public ScreenList(ObjectList<ScreensObject> sl)
        {
            foreach (ScreensObject obj in sl)
            {
                if (!ContainsKey(obj.ScreenName))
                    Add(obj.ScreenName, obj );
            }
        }
    }
    
}
