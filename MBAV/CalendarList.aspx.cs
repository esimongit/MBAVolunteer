using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Bus;
using NQN.Core;

namespace MBAV
{
    public partial class CalendarList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            UpdateCells();
            if (!Page.IsPostBack)
            {
                MultiView1.SetActiveView(View1);
                GridView1.Focus();
            }
        }
        protected void UpdateCells()
        {
            int GuideID = 0;
            try
            {
                GuideID = Convert.ToInt32(Session["GuideID"]);
            }
            catch { }
            if (GuideID == 0)
                Response.Redirect("Login.aspx");
            GuidesDM dm = new GuidesDM();
           
            GuidesObject guide = dm.FetchGuide( GuideID);
             
            NameLabel.Text = guide.GuideName;
            RoleLabel.Text = "Info Center Sign-up";

            
        }

        protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
        protected string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }
        protected void DoReset(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.PathAndQuery, true);
        }
        protected void DoSubmit(object sender, CommandEventArgs e)
        {
            GuidesDM gdm = new GuidesDM();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            SubOffersDM odm = new SubOffersDM();
            SubstitutesBusiness sb = new SubstitutesBusiness();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ShiftsDM sdm = new ShiftsDM();
            ObjectList<GuidesObject> NotifyList = new ObjectList<GuidesObject>();
            bool RequestProcessed = false;
            bool DoSub = false;
            int GuideID = 0;
            int ShiftID = 0;
            int GuideSubstituteID = 0;
            DateTime SubDate = DateTime.MinValue;
            try
            {
                GuideID = Convert.ToInt32(Session["GuideID"]);
            }
            catch { }
            string msg = String.Empty;
            if (GuideID == 0)
                Response.Redirect("Login.aspx");
            // Remove a commitment
            foreach (GridViewRow row in GridView0.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = (CheckBox)row.Cells[2].FindControl("RemoveCheckBox");
                    if (cb != null && cb.Checked)
                    {
                        int indx = row.RowIndex;
                        GuideSubstituteID = Convert.ToInt32(GridView0.DataKeys[indx].Values[0]);
                        RequestProcessed = true;
                        if (GuideSubstituteID > 0)
                        {
                            GuideSubstituteObject sub = dm.FetchRecord("GuideSubstituteID", GuideSubstituteID);
                            if (sub == null)
                                continue;
                            NotifyList.AddRange(gdm.FetchCaptains(sub.ShiftID));
                            dm.DeleteForSubAndDate(GuideID, sub.SubDate, sub.ShiftID);
                            msg = String.Format("{0} ({1}) will not be able to substitute on    for shift {2} on {3}.",
                                sub.SubName, sub.Sub,  sub.Sequence, sub.SubDate.ToLongDateString());
                            break;
                        }
                        else if (GuideSubstituteID < 0)
                        {
                            GuideDropinsObject drop = ddm.FetchRecord("GuideDropinID", -GuideSubstituteID);
                            if (drop == null)
                                continue;
                            NotifyList.AddRange(gdm.FetchCaptains(drop.ShiftID));
                            ddm.Delete(drop.GuideDropinID);
                            msg = String.Format("{0} ({1}) will not be able to substitute on    for shift {2} on {3}.",
                                drop.GuideName, drop.Role, drop.Sequence, drop.DropinDate.ToLongDateString());
                            break;
                        }
                    }
                }
            }

            // Add a commitment
            if (!RequestProcessed)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox cb = (CheckBox)row.Cells[4].FindControl("SubCheckBox");
                        if (cb != null && cb.Checked)
                        {
                            int indx = row.RowIndex;
                            ShiftID = Convert.ToInt32(GridView1.DataKeys[indx].Values[0]);
                            SubDate = DateTime.Parse(GridView1.DataKeys[indx].Values[1].ToString());
                            int Cnt = Convert.ToInt32(GridView1.DataKeys[indx].Values[2]);
                            ObjectList<GuideSubstituteObject> subs = dm.FetchInfoForShift(ShiftID, SubDate);
                            int satisfied = 0;
                            foreach (GuideSubstituteObject sub in subs)
                            {
                                if (sub.SubstituteID > 0)
                                    continue;
                                // Here is an unrequited subrequest
                                sub.SubstituteID = GuideID;
                                dm.Update(sub);
                                satisfied = sub.GuideSubstituteID;
                                RequestProcessed = true;
                                break;
                            }

                            if (satisfied == 0)
                            {
                                GuideDropinsObject dropin = new GuideDropinsObject();
                                dropin.DropinDate = SubDate;
                                dropin.ShiftID = ShiftID;
                                dropin.GuideID = GuideID;
                                dropin.RoleID = RolesDM.GetInfo();
                                dropin.OnShift = false;
                                ddm.Save(dropin);
                                RequestProcessed = true;
                                satisfied = -ddm.GetLast();
                            }
                            if (satisfied > 0)
                            {

                                GuideSubstituteObject obj = dm.FetchRecord("GuideSubstituteID", satisfied);
                                NotifyList.AddRange(gdm.FetchCaptains(obj.ShiftID));
                                GuidesObject guide1 = gdm.FetchGuide(obj.GuideID);
                                NotifyList.Add(guide1);
                                msg = String.Format("{0} ({1}) will substitute for {2} ({3}) for shift {4} on {5}.",
                                    obj.SubName, obj.Sub, obj.GuideName, obj.VolID, obj.Sequence, obj.SubDate.ToLongDateString());
                                break;
                            }
                            else if (satisfied < 0)
                            {
                                GuideDropinsObject dropin = ddm.FetchRecord("GuideDropinID", -satisfied);
                                NotifyList.AddRange(gdm.FetchCaptains(dropin.ShiftID));
                                msg = String.Format("{0} ({1}) will be filling in for shift {2} on {3}.",
                                    dropin.GuideName, "Info Center", dropin.ShiftName, dropin.DropinDate.ToLongDateString());
                                break;
                            }

                        }
                        DoSub = RequestProcessed;
                    }
                }
            }
            if (RequestProcessed)
            {

                GridView1.DataBind();
                GridView0.DataBind();

                MultiView1.SetActiveView(View2);
                sb.Notify(NotifyList, msg, RolesDM.GetInfo());

                msg = "The following message has been sent: <br /> " + msg;
                MsgLabel.Text = msg;
                RecipientsRepeater.DataSource = NotifyList;
                RecipientsRepeater.DataBind();
                if (DoSub)
                {

                    ConfirmLink.Visible = true;
                    GuideSubstituteObject sub = sb.SelectSubstituteShift(GuideID, SubDate, ShiftID);
                    ConfirmLink.CalendarType = sub.CalendarType;
                    ConfirmLink.HostName = GetHostName();
                    ConfirmLink.Description = String.Format("Shift {0}", sub.Sequence);
                    ConfirmLink.Date = sub.SubDate;
                    ConfirmLink.StartTime = sub.ShiftStart;
                    ConfirmLink.EndTime = sub.ShiftEnd;
                }
            }
            else
            {
                ErrorMessage.Set("No matching substitute requests were found.");
            }

        }
    }
}