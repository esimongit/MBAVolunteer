using System;
 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NQN.Bus;
using NQN.Controls;


namespace VolManager
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MultiView1.SetActiveView(View1);
            }

        }
        protected void AddButtonClick(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View3);
        }
        protected void GridView_Selected(object sender, EventArgs e)
        {
            // Change Password
            MembershipBusiness mb = new MembershipBusiness();
            string pw = String.Empty;
            try
            {
                pw = mb.ChangePassword(NQNGridView1.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ErrorMessage.Set(ex.Message);
                return;
            }
            if (pw == String.Empty)
            {
                ErrorMessage.Set("Password change failed");
            } 
            else 
            {
                InfoMessage.Set("Password changed to " + pw);
                
            }
        }
        protected void OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
             if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
                return;
            }
            InfoMessage.Set("User Deleted");
          
            NQNGridView1.DataBind();
            MultiView1.SetActiveView(View1);
        }
        protected void OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
                return;
            }
            InfoMessage.Set("New User Password:  " +e.ReturnValue.ToString());
            TextBox UserNameTB = (TextBox)FormView2.FindControl("UserNameTextBox");
            TextBox EmailTB = (TextBox)FormView2.FindControl("EmailTextBox");
            NQNGridView1.DataBind();
            MultiView1.SetActiveView(View1);
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
    }
}
