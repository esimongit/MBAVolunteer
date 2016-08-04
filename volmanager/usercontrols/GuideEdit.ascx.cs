using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Bus;
using NQN.Core;

namespace VolManager.UserControls
{
    public partial class GuideEdit : System.Web.UI.UserControl
    {
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
                InfoMessage.Set("Changes accepted.");
            }
        }
        protected void AddLogin(object sender, EventArgs e)
        {
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            GuidesDM dm = new GuidesDM();
            GuidesObject obj = dm.FetchGuide(GuideID);
            MembershipBusiness mb = new MembershipBusiness();
            mb.InsertVols(obj.VolID, obj.Email);
            FormView1.DataBind();
        }

        protected void ResetPW(object sender, EventArgs e)
        {
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            GuidesDM dm = new GuidesDM();
            GuidesObject obj = dm.FetchGuide(GuideID);
            string pw = String.Empty;
            try
            {
                pw = MembershipBusiness.VolChangePassword(obj.VolID);
            } catch (Exception ex)
            {
                ErrorMessage.Set(ex.Message);
                return;
            }
            InfoMessage.Set(String.Format("New Password: {0}", pw));
            FormView1.DataBind();
        }
        protected void DeleteShift(object sender, GridViewDeleteEventArgs e)
        {
            GuidesDM dm = new GuidesDM();
            dm.DeleteGuideShift(Convert.ToInt32(Session["GuideID"]), Convert.ToInt32(e.Keys[0]));
            GridView ShiftView = (GridView)FormView1.FindControl("ShiftView");
            ShiftView.DataBind();
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
            {
                if (!dm.AddSub(GuideID, ShiftID))
                    cb.Checked = false;
            }
            else
                dm.Delete(GuideID, ShiftID);
        }
    }
}