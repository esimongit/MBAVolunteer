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
        
        public void SetupEdit()
        {
            HyperLink MBAVLink = (HyperLink)FormView1.FindControl("MBAVLink");
            GuidesBusiness gb = new GuidesBusiness();
            GuidesObject guide = gb.SelectGuide(Convert.ToInt32(Session["GuideID"]));
            if (guide == null)
                return;
            MBAVLink.NavigateUrl = String.Format("{0}/Login.aspx?ID={1}", StaticFieldsObject.StaticValue("GuideURL"), guide.UserId);
 
        }
        protected void SetLink(object sender, EventArgs e)
        {
            SetupEdit();
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
                InfoMessage.Set("Changes accepted.");
                
            }
        }
        protected void AddLogin(object sender, EventArgs e)
        {
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            GuidesDM dm = new GuidesDM();
            GuidesObject obj = dm.FetchGuide(GuideID);
            MembershipBusiness mb = new MembershipBusiness();
            try
            {
                mb.InsertVols(obj.VolID, obj.Email);
                FormView1.DataBind(); 
            }
            catch (Exception ex)
            {
                ErrorMessage.Set(ex.Message);
            }
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
            try
            {
                dm.DeleteGuideShift(Convert.ToInt32(Session["GuideID"]), Convert.ToInt32(e.Keys[0]));
                FormView1.DataBind(); 
            }
            catch(Exception ex)
            {
                ErrorMessage.Set(ex.Message);
            } 
            
        }
        protected void DeleteRole(object sender, GridViewDeleteEventArgs e)
        {
            GuidesDM dm = new GuidesDM();
            try
            {
                dm.RemoveRole(Convert.ToInt32(Session["GuideID"]), Convert.ToInt32(e.Keys[0]));
                FormView1.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMessage.Set(ex.Message);
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
            {
                if (!dm.AddSub(GuideID, ShiftID))
                    cb.Checked = false;
            }
            else
                dm.Delete(GuideID, ShiftID);
        }
    }
}