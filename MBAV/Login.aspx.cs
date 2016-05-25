using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Bus;

namespace MBAV.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Membership.ApplicationName = "MBAV";
            if (Request.QueryString["ID"] != String.Empty && Request.QueryString["ID"] != null)
            {
                Guid UserID = new Guid(Request.QueryString["ID"]);
                SiteMembershipDM dm = new SiteMembershipDM("MBAV");
                SiteMembershipUser u = dm.FetchUser(UserID);
                if (u == null)
                    return;
                GuidesDM gdm = new GuidesDM();
                GuidesObject guide = gdm.FetchGuide(u.UserName);
                Session["GuideID"] = guide.GuideID;
                FormsAuthentication.RedirectFromLoginPage(guide.VolID, LoginUser.RememberMeSet);
                FormsAuthentication.SetAuthCookie(guide.VolID, false);
            }
        }
        protected void SaveUser(object sender, EventArgs e)
        {
            string VolID = LoginUser.UserName;
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(VolID);
            Session["GuideID"] = guide.GuideID;
        }
        protected void ResetPW(object sender, EventArgs e)
        {
            string name = LoginUser.UserName;
            MembershipBusiness mb = new MembershipBusiness();
            try
            {
                mb.ResetPassword(name, "MBAV");
                AlertMsg.Show("Check your Email for instructions on resetting your password.");
            }
            catch
            {
                AlertMsg.Show("A problem occurred in this operation, please contact Volunteer Engagement.");
            }
        }
    }
}
