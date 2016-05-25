using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using NQN.DB;

namespace VolManager
{
    public partial class Recovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PasswordRecoveryObject obj = GetPWRecovery();
                LoginLabel.Text = "Reset Password for " + obj.username;
            }
            catch 
            {
                Response.StatusCode = 404;
                Response.SuppressContent = true;
            }
        }
        protected void Change(object sender, EventArgs e)
        {
            if (pw1.Text != pw2.Text)
            {
                ErrorLabel.Text = "Passwords do not match";
                return;
            }
            Membership.ApplicationName = "VolManager";
           
            PasswordRecoveryObject obj =  null;
            try
            {
                obj = GetPWRecovery();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
                return;
            }
            MembershipUserCollection col = Membership.FindUsersByName(obj.username);
            if (col.Count > 0)
            {
                MembershipUser u = col[obj.username];
                if (u.IsLockedOut) u.UnlockUser();

                string pw = u.ResetPassword();

                try
                {
                    if (u.ChangePassword(pw, pw1.Text))
                    {
                        PasswordRecoveryDM dm = new PasswordRecoveryDM();
                        dm.Delete(obj.ID);
                        Response.Redirect("/");
                    }
                    else
                    {
                        ErrorLabel.Text = "Password Change Failed.  Please contact the site administrator.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = ex.Message;
                    return;
                }
                
            }
        }
        private PasswordRecoveryObject GetPWRecovery()
        {
            PasswordRecoveryObject obj = null;
           
            PasswordRecoveryDM dm = new PasswordRecoveryDM();
            Guid prkey = new Guid(Request.QueryString["key"]);

            obj = dm.FetchRecord(prkey);
            if (obj == null)
                throw new Exception("User Name not found");
            return obj;
        }
    }
}