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
            if (!Page.IsPostBack)
                MultiView1.SetActiveView(View1);
            int GuideID = 0;
            try
            {
                GuideID = Convert.ToInt32(Session["GuideID"]);
            }
            catch { }
            GuidesDM dm = new GuidesDM();
            if (GuideID > 0)
            {
                GuidesObject guide = dm.FetchGuide(GuideID);
                IrregularButton.Visible = guide.IrregularSchedule;
                 
            }
            
        }
        protected void ClosePage(object sender, EventArgs e)
        {
            Response.Redirect("~/SubstituteCalendar.aspx");
        }
        protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
        protected void ToView2(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }
        protected void ToView3(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View3);
        }
      
        protected void ToView4(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View4);
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
            //int RoleID = 0;
            //try
            //{
            //    RoleID = Convert.ToInt32(OfferRoleSelect.SelectedValue);
            //}
            //catch { }
            //// Session Role is either 0 or Info Desk depending on whether the person is Info only
            //if (RoleID == 0)
            //    RoleID = Convert.ToInt32(Session["RoleID"]);
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
       
        // Drop-ins
        protected void DoSubmit(object sender, EventArgs e)
        {

            GuideDropinsDM dm = new GuideDropinsDM();
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            int ShiftID = 0;
            try
            {
                ShiftID = Convert.ToInt32(ShiftSelect.SelectedValue);
            }
            catch
            { }

            if ((ShiftID == 0) || (GuideID == 0))
                return;
            foreach (RepeaterItem itm in Repeater2.Items)
            {
                CheckBox cb = (CheckBox)itm.FindControl("DateCheckBox");
                HiddenField hf = (HiddenField)(cb.Parent.FindControl("DropinIDHidden"));
                Label DateLabel = (Label)cb.Parent.FindControl("DateLabel");
               
                DateTime dt = DateTime.Parse(DateLabel.Text);
                int DropinID = Convert.ToInt32(hf.Value);
                if (cb.Checked)
                {
                    if (DropinID < 0)
                    {
                        dm.SaveOnShift(GuideID, ShiftID, dt, Convert.ToInt32(RoleSelect.SelectedValue));

                    }
                }
                else if (DropinID > 0)
                {
                    dm.Delete(DropinID);

                }
            }
            InfoMessage.Set("Changes Accepted");
            Repeater1.DataBind();
        }

    }
}
