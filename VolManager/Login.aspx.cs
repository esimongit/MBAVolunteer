using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using NQN.Bus;
using NQN.DB;
using NQN.Core;

namespace VolManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("Role");
            string agency = System.Configuration.ConfigurationManager.AppSettings["Title"];


            Membership.ApplicationName = "VolManager";
            Roles.ApplicationName = "VolManager";

        }
        public void Login_OnClick(object sender, EventArgs args)
        {
            if (Membership.ValidateUser(LoginUser.UserName, LoginUser.Password))
            {
                SaveUser();
                FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, LoginUser.RememberMeSet);
            }
            else
            {
                Literal FailureText = (Literal)LoginUser.FindControl("FailureText");
                FailureText.Text = "Login failed. Please check your user name and password and try again.";
            }
        }
        
        protected void ResetPW(object sender, EventArgs e)
        {
            string name = LoginUser.UserName;
            if (string.IsNullOrEmpty(name))
            {
                AlertMsg.Show("You must enter a known Username to request a password reset.");
            }
            MembershipBusiness mb = new MembershipBusiness();
            try
            {
                mb.ResetPassword(name, "VolManager");
                AlertMsg.Show("Check your Email for instructions on resetting your password.");
            }
            catch
            {
                AlertMsg.Show("A problem occurred in this operation, please contact Volunteer Engagement.");
            }
        }
        protected void SaveUser()
        {
            string name = LoginUser.UserName;
            Session["UserName"] = name;
            string Role = UserSecurity.GetRole(name);
            Session["Role"] = Role;
            Session["MenuID"] = 4;
          

        }

    }
}
