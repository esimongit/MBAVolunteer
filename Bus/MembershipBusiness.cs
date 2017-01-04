using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class MembershipBusiness
    {
        string VolAccessApp = String.Empty;
        string VolAccessTable = String.Empty;
        public MembershipBusiness()
        {
            Membership.ApplicationName = "VolManager";
            if (Roles.Enabled)
                Roles.ApplicationName = "VolManager";
            VolAccessApp = ConfigurationManager.AppSettings["VolAccessApp"];
            VolAccessTable = ConfigurationManager.AppSettings["VolAccessTable"];
        }


        public string Insert(string UserName, string Email, string role)
        {
            Membership.ApplicationName = "VolManager";
            Roles.ApplicationName = "VolManager";
            MembershipUserCollection col = Membership.FindUsersByEmail(Email);
            if (col.Count > 0) throw new Exception("User with this Email already exists: "+ Email);
            col = Membership.FindUsersByName(UserName);
            if (col.Count > 0) throw new Exception("User with this Login ID already exists: " + UserName);
            string password = Membership.GeneratePassword(7, 1);
            
            MembershipUser u = Membership.CreateUser(UserName, password, Email);
            if (u != null)
            {
                Roles.AddUserToRole(UserName, role);
                PasswordRecoveryDM dm = new PasswordRecoveryDM();
               
                NQNMembershipObject mobj = new NQNMembershipObject(UserName, Email, password);
                 mobj.VolAccessUrl = "http://" + HttpContext.Current.Request.Url.DnsSafeHost;
                if (HttpContext.Current.Request.Url.Port != 80)
                    mobj.VolAccessUrl += ":" + HttpContext.Current.Request.Url.Port.ToString(); 
                PasswordRecoveryObject pwobj = new PasswordRecoveryObject();
        
                pwobj.username = UserName;
                pwobj.tstamp = DateTime.Now;
                dm.Save(pwobj);
                mobj.PRKey = pwobj.ID;
                EmailBusiness eb = new EmailBusiness();
                try
                {
                    eb.Notify(Email, UserName, mobj.Link, String.Empty, "MBA Substitute Administration");
                }
                catch { }
            }
            return password;
        }
        public string InsertVols(string UserName, string Email)
        {
            if (VolAccessApp == String.Empty)
                return String.Empty;
            if (Email.Trim() == String.Empty)
                throw new Exception("Email is required for a login.");    
           
            Regex r = new Regex(GuidesObject.ValidEmail, RegexOptions.IgnoreCase);
            Match m = r.Match(Email);
            if (!m.Success)
                throw new Exception("Invalid Email Format: " + Email);
           
            Membership.ApplicationName = VolAccessApp;

            MembershipUserCollection col = Membership.FindUsersByName(UserName);
            if (col.Count > 0)
            {
                Membership.ApplicationName = "VolManager";
                return String.Empty;
            }
            
            string password = Membership.GeneratePassword(7, 1);
            GuidesDM gdm = new GuidesDM();
           
          
            string res = String.Empty;
             MembershipUser u = Membership.CreateUser(UserName, password, Email);
            
            if (u != null)
            {
                GuidesObject guide = gdm.FetchGuide(UserName);
                if (guide == null)
                {
                    Membership.ApplicationName = "VolManager";
                    throw new Exception("Guide not found");
                }
                PasswordRecoveryDM dm = new PasswordRecoveryDM();
                NQNMembershipObject mobj = new NQNMembershipObject(UserName, Email, password);
                mobj.FirstName = guide.FirstName;
                mobj.LastName = guide.LastName;

                mobj.VolAccessUrl = StaticFieldsObject.StaticValue("GuideURL");
                PasswordRecoveryObject pwobj = new PasswordRecoveryObject(Membership.ApplicationName);

                pwobj.username = UserName;
                pwobj.tstamp = DateTime.Now;
                dm.Save(pwobj);
                mobj.PRKey = pwobj.ID;
                EmailBusiness eb = new EmailBusiness();
                MailTextDM mtdm = new MailTextDM();
                MailTextObject mtobj = mtdm.FetchForSymbol("VolAccessLogin");
                if (mtobj != null && mtobj.Enabled)
                    eb.SendMail(mtobj.MailFrom, Email, mtobj.Subject, mtobj.CompletedText(mobj), mtobj.IsHtml);
            }

            Membership.ApplicationName = "VolManager";

            return password;
        }

       

       
           
        // Staff changes Guide Password via reset
        public static string VolChangePassword(string UserName)
        {

            string pw = String.Empty;
            Membership.ApplicationName = "MBAV";
            MembershipUserCollection col = Membership.FindUsersByName(UserName);
            if (col.Count > 0)
            {
                MembershipUser u = col[UserName];
                if (u.IsLockedOut) u.UnlockUser();
              
                pw = u.ResetPassword();
                
                PasswordRecoveryDM dm = new PasswordRecoveryDM();
                NQNMembershipObject mobj = new NQNMembershipObject(UserName, u.Email, pw);
                mobj.VolAccessUrl = StaticFieldsObject.StaticValue("GuideURL");
                PasswordRecoveryObject pwobj = dm.FetchUser(UserName, Membership.ApplicationName);
                if (pwobj == null)
                    pwobj = new PasswordRecoveryObject(Membership.ApplicationName);
                // If the timestamp is within the last day, keep it
                if (pwobj.tstamp.Date < DateTime.Today)
                {
                    pwobj.ID = mobj.PRKey;
                    pwobj.tstamp = DateTime.Now;
                    pwobj.username = UserName;
                    dm.Save(pwobj);
                }
                else
                {
                    mobj.PRKey = pwobj.ID;
                }
                EmailBusiness eb = new EmailBusiness();
                MailTextDM mtdm = new MailTextDM();
                MailTextObject mtobj = mtdm.FetchForSymbol("VolAccessReset");
                if (mtobj != null && mtobj.Enabled)
                    eb.SendMail(mtobj.MailFrom, mobj.Email, mtobj.Subject, mtobj.CompletedText(mobj), mtobj.IsHtml);

            }
            else
            {
                Membership.ApplicationName = "VolManager";
                throw new Exception("User Name not Found");
            }
            Membership.ApplicationName = "VolManager";
            return pw;
        }

        // Change password for staff person from Manage Users screen
        public string ChangePassword(string UserName)
        {
            MembershipUserCollection col = Membership.FindUsersByName(UserName);
            if (col.Count > 0)
            {
                MembershipUser u = col[UserName];
                if (u.IsLockedOut) u.UnlockUser();
                string  pw = u.ResetPassword();
                NQNMembershipObject mobj = new NQNMembershipObject(UserName, u.Email, pw);
                int Port = HttpContext.Current.Request.Url.Port;
                if (Port == 80)
                    mobj.VolAccessUrl = "http://" + mobj.VolAccessUrl;
                else if (Port == 443)
                    mobj.VolAccessUrl = "https://" + mobj.VolAccessUrl;
                else
                    mobj.VolAccessUrl = "http://" + mobj.VolAccessUrl + ":" + HttpContext.Current.Request.Url.Port.ToString();
                PasswordRecoveryDM dm = new PasswordRecoveryDM();
                PasswordRecoveryObject pwobj = dm.FetchUser(UserName, Membership.ApplicationName);
                if (pwobj == null)
                    pwobj = new PasswordRecoveryObject();
                // If the timestamp is within the last day, keep it
                if (pwobj.tstamp.Date < DateTime.Today)
                {
                    pwobj.ID = mobj.PRKey;
                    pwobj.tstamp = DateTime.Now;
                    pwobj.username = UserName;
                    dm.Save(pwobj);
                }
                else
                {
                    mobj.PRKey = pwobj.ID;
                }
                EmailBusiness eb = new EmailBusiness();
                MailTextDM mtdm = new MailTextDM();
                string ret = "New Password: " + pw;
                try
                {
                    eb.Notify(u.Email, UserName, mobj.Link,
                         "<p>Your Login has been reset. Please click on the Link below to set a new password.</p><ul>",
                          "MBA Substitute Administration");
                }
                catch(Exception e)
                {
                    ret += " -- " + e.Message;
                }
                return ret;
            }
         
            return String.Empty;
        }

        // Request a new password from login screen.  Staff or guides
        public void ResetPassword(string uname, string ApplicationName)
        {
            string pw = String.Empty;

            Membership.ApplicationName = ApplicationName;
            MembershipUserCollection col = Membership.FindUsersByName(uname);
            if (col.Count == 0)
                throw new Exception("User not found");

            MembershipUser u = col[uname];
            if (u.IsLockedOut) u.UnlockUser();
            pw = u.ResetPassword();
            EmailBusiness eb = new EmailBusiness();
            PasswordRecoveryDM dm = new PasswordRecoveryDM();
            NQNMembershipObject mobj = new NQNMembershipObject(u.Email, pw);
            MailTextDM mtm = new MailTextDM();
            MailTextObject mtobj = mtm.FetchForSymbol("PasswordReset");
            
            mobj.VolAccessUrl =   HttpContext.Current.Request.Url.DnsSafeHost;
            int Port = HttpContext.Current.Request.Url.Port;
            if (Port == 80)
                mobj.VolAccessUrl = "http://" + mobj.VolAccessUrl;
            else if (Port == 443)
                mobj.VolAccessUrl = "https://" + mobj.VolAccessUrl;
            else
                mobj.VolAccessUrl = "http://" + mobj.VolAccessUrl +":"+ HttpContext.Current.Request.Url.Port.ToString();

            PasswordRecoveryObject pwobj = dm.FetchUser(uname, ApplicationName);
            if (pwobj == null)
                pwobj = new PasswordRecoveryObject(ApplicationName);
            // If the timestamp is within the last day, keep it
            if (pwobj.tstamp.Date < DateTime.Today)
            {
                pwobj.ID = mobj.PRKey;
                pwobj.tstamp = DateTime.Now;
                pwobj.username = uname;
                dm.Save(pwobj);
            }
            else
            {
                mobj.PRKey = pwobj.ID;
            }
     
            string Program =  ApplicationName == "MBAV" ? "Monterey Bay Aquarium Volunteer Substitute Calendar" : "MBA Substitute Administration";
            string msg = String.Format(@"<p>We have received a password reset request on {0}.
                Please use the link below to set a new password:</p><ul>", Program) ;
            try
            {
                eb.Notify(u.Email, uname, mobj.Link, msg, Program);
            }
            catch { }
        }
        public MembershipUserCollection SelectAll()
        {
            MembershipUserCollection col = Membership.GetAllUsers();
            foreach (MembershipUser u in col)
            {
                string[] uroles = Roles.GetRolesForUser(u.UserName);
                if (uroles.Length > 0)
                    u.Comment = uroles[0];
            }
            return col;
        }
       
        public DataTable SelectVols(string Pattern)
        {
            if (Pattern == null)
                Pattern = String.Empty;
            SiteMembershipDM dm = new SiteMembershipDM(VolAccessApp, VolAccessTable);
            Membership.ApplicationName = VolAccessApp;
            ObjectList<SiteMembershipUser> dList =   dm.FetchAll(Pattern);
           
            if (dList == null)
                return null;
            return dList.RenderAsTable();
        }
        public int SelectVolCount()
        {
            SiteMembershipDM dm = new SiteMembershipDM(VolAccessApp, VolAccessTable);
            return dm.FetchCount();
        }
        

        

        public MembershipUser SelectVol(string UserName)
        {
            if (VolAccessApp == String.Empty)
                return null;
            Membership.ApplicationName = VolAccessApp;

            MembershipUser u = Membership.GetUser(UserName);
            Membership.ApplicationName = "VolManager";

            return u;
        }

        public void UpdateRole(string Comment, string UserName)
        {
            string[] uroles = Roles.GetRolesForUser(UserName);
            foreach (string urole in uroles)
            {
                Roles.RemoveUserFromRole(UserName, urole);
            }
            Roles.AddUserToRole(UserName, Comment);
        }
        public void Delete(string UserName)
        {
            if (UserName == HttpContext.Current.User.Identity.Name)
                throw new Exception("You may not delete the current login");

            if (UserName != null)
            {
                string[] uroles = Roles.GetRolesForUser(UserName);
                if (uroles.Length > 0)
                    Roles.RemoveUserFromRoles(UserName, uroles);
                Membership.DeleteUser(UserName);
            }
        }
        // Delete all Vols
        public void DeleteVols()
        {
            Membership.ApplicationName = VolAccessApp;
            SiteMembershipDM dm = new SiteMembershipDM(VolAccessApp, VolAccessTable);
            foreach (SiteMembershipUser obj in dm.FetchAll(String.Empty))
            {
                if (obj.UserName != null)
                {
                    Membership.DeleteUser(obj.UserName);
                }
            }
            Membership.ApplicationName = "VolManager";
        }
         
        
        public void DeleteVols(string UserName)
        {
            if (VolAccessApp == String.Empty)
                return;
            Membership.ApplicationName = VolAccessApp;
          
            if (UserName != null)
            {               
                Membership.DeleteUser(UserName);
            }
            Membership.ApplicationName = "VolManager";

        }
        public string GetCurrentUsers(string ApplicationName)
        {
            Membership.ApplicationName = ApplicationName;
            string ulist = String.Empty;
            string sep = String.Empty;
            foreach (MembershipUser u in Membership.GetAllUsers())
            {
                if (u.IsOnline)
                {
                    ulist += sep + u.UserName;
                    sep = ", ";
                }
            }
            return ulist;
        }
       
    }
}
