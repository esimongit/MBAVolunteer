using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Bus;

namespace MBAV
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
                Session["RoleID"] = (guide.RoleID == RolesDM.GetInfo()) ? guide.RoleID : 0;
                FormsAuthentication.RedirectFromLoginPage(guide.VolID, LoginUser.RememberMeSet);
                FormsAuthentication.SetAuthCookie(guide.VolID, false);
            }
            AnnouncementsDM adm = new AnnouncementsDM();
            FileBusiness fb = new FileBusiness();
            AnnouncementLabel.Text = fb.CompletedText(adm.FetchMain().AnnouncementText);
        }

        protected void OnLoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
        {
            string VolID = LoginUser.UserName;
            Label InstructionText = (Label)LoginUser.FindControl("InstructionText");
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(VolID);
            if (guide == null || guide.Inactive)
            {
                 InstructionText.Text = "Your login has been disabled.";
                e.Cancel = true;
            }
            else
                 InstructionText.Text = "Please enter your Guide ID and password.";



        }
        protected void SaveUser(object sender, EventArgs e)
        {
            string VolID = LoginUser.UserName;
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(VolID);
            
            Session["IsCaptain"] = guide.HasRole("Shift Captain");
            Session["RoleID"] = (guide.RoleID == RolesDM.GetInfo()) ? guide.RoleID : 0;

            Session["GuideID"] = guide.GuideID;
        }
        protected void ResetPW(object sender, EventArgs e)
        {
            string name = LoginUser.UserName;
            if (string.IsNullOrEmpty(name))
            {
                AlertMsg.Show("You must enter a valid Guide ID to request a password reset.");
            }
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
