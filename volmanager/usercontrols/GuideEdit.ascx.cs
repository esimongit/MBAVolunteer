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
            Repeater1.DataBind();
            GridView2.DataBind();
            if (guide.IrregularSchedule)
                GridView3.DataBind();
            Menu1.Items[3].Enabled = guide.IrregularSchedule;
            MultiView1.SetActiveView(View0);
            MultiView2.SetActiveView(View3AB);
        }
        protected void SetLink(object sender, EventArgs e)
        {
            SetupEdit();
        }
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Convert.ToInt32(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
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
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
        protected void ChangeOption(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = (1 - MultiView2.ActiveViewIndex );
            OptionButton.Text = MultiView2.ActiveViewIndex == 0 ? "View/Select a Role for Each Date" : "Set future A and B Roles";
        }
        protected void DoSubmitAB(object sender, EventArgs e)
        {
            int cnt = 0;
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
            {
                ErrorMessage.Set("Select a Shift");
                return;
            }
            int RoleA = Convert.ToInt32(RoleSelectA.SelectedValue);
            int RoleB = Convert.ToInt32(RoleSelectB.SelectedValue);
            SubstitutesBusiness sb = new SubstitutesBusiness();
            cnt = sb.SetFutureABShifts(GuideID, ShiftID, RoleA, RoleB);
            
            InfoMessage.Set(String.Format("{0} changes made.", cnt));
        }

        protected void DoSubmit(object sender, EventArgs e)
        {
            int cnt = 0;
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
            {
                ErrorMessage.Set("Select a Shift");
                return;
            }
            foreach (RepeaterItem itm in Repeater2.Items)
            {
                CheckBox cb = (CheckBox)itm.FindControl("DateCheckBox");
                HiddenField hf = (HiddenField)(cb.Parent.FindControl("DropinIDHidden"));
                Label DateLabel = (Label)cb.Parent.FindControl("DateLabel");
                DropDownList RoleSelect = (DropDownList)cb.Parent.FindControl("RoleSelect");
                DateTime dt = DateTime.Parse(DateLabel.Text);
                int DropinID = Convert.ToInt32(hf.Value);
                if (cb.Checked)
                {
                    if (DropinID < 0)
                    {
                        try
                        {
                            dm.SaveOnShift(GuideID, ShiftID, dt, Convert.ToInt32(RoleSelect.SelectedValue));
                            cnt++;
                        }
                        catch
                        {
                            ErrorMessage.Add(String.Format("Select a Role for {0:d}", dt));
                        }
                    }
                }
                else if (DropinID > 0)
                {
                    dm.Delete(DropinID);
                    cnt++;
                }
            }
            InfoMessage.Set(String.Format("{0} changes made.", cnt));
        }
      
    }
}