using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace MBAV
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ClosePage(object sender, EventArgs e)
        {
            Response.Redirect("~/SubstituteCalendar.aspx");

        }
        protected void OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
            }
            else
            {
                InfoMessage.Set("Changes Accepted");
            }
        }
        protected void CheckChanged(object sender, EventArgs e)
        {
            SubOffersDM dm = new SubOffersDM();
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            if (GuideID == 0)
                return;
            CheckBox cb = (CheckBox)sender;
            HiddenField hf = (HiddenField)(cb.Parent.FindControl("ShiftIDHidden"));
            int ShiftID = Convert.ToInt32(hf.Value);
            if (cb.Checked)
                cb.Checked = dm.AddSub(GuideID, ShiftID);
            else
            {
                dm.Delete(GuideID, ShiftID);
                cb.Checked = false;
            }
            cb.Focus();
        }
    }
}