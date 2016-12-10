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
    public partial class SubRequest : System.Web.UI.Page
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
            ShiftsDM tdm = new ShiftsDM();
            GuideSubstituteDM sdm = new GuideSubstituteDM();
           // GuideSubstituteObject sub = sdm.FetchForGuide(GuideID, DateTime.Parse(Request.QueryString["dt"]));
            GuidesObject guide = dm.FetchGuide( GuideID);
            DateTime dt = DateTime.Today;
            try
            {
                dt = DateTime.Parse(Request.QueryString["dt"]);
            }
            catch
            {
                ErrorMessage.Set("This page is only reached from the calendar");
                return;
            }
            DateLabel.Text = DateTime.Parse(Request.QueryString["dt"]).ToLongDateString();
            NameLabel.Text = guide.GuideName;           
            RoleLabel.Text = guide.RoleName;

            DropinCell.Visible = tdm.ShiftsForDateAndGuide(dt, GuideID).Count > 0;
            SubstitutesBusiness sb = new SubstitutesBusiness();
            string SubShifts = sb.SubShiftsForGuideAndDate(GuideID, dt);
            CurrentSubCell.Visible = false;
            if (SubShifts != String.Empty)
            {
                CurrentSubCell.Visible = true;
                SequenceLabel.Text = SubShifts;
            }

        }

        protected void ClearCheckBoxes()
        {
           
            NoSubCheckBox.Checked = false;
            NewDropCheckBox.Checked = false;
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
            DateTime dt = DateTime.Parse(Request.QueryString["dt"]);
            int NotifyInterestedSubs = 0;
            bool NeedSub = false;
            bool DoSub = false;
            InfoLabel.Visible = false;
            bool RequestProcessed = false;
            int GuideID = 0;
            int ShiftID = 0;
            try
            {
                GuideID = Convert.ToInt32(Session["GuideID"]);
            }
            catch { }
            string msg = String.Empty;
            if (GuideID == 0)
                Response.Redirect("Login.aspx");
            // Asking for a Substitute.
            GuidesObject guide = gdm.FetchGuide(GuideID);
            foreach (RepeaterItem itm in ShiftNeedRepeater.Items)
            {               
                CheckBox NeedSubCheckBox = (CheckBox)itm.FindControl("NeedSubCheckBox");
                TextBox SubTextBox = (TextBox)itm.FindControl("SubTextBox");
                if (NeedSubCheckBox.Checked)
                {
                    NeedSub = true;
                    ShiftID = Convert.ToInt32(((HiddenField)itm.FindControl("ShiftIDHidden")).Value);
                    ShiftsObject shift = sdm.FetchShift(ShiftID);
                   
                    try
                    {                      
                        sb.SubRequest(GuideID, dt, ShiftID, SubTextBox.Text);
                        NotifyList.AddRange(gdm.FetchCaptains(ShiftID));
                        NotifyList.Add(guide);
                        RequestProcessed = true;
                        msg += String.Format("{0} ({1}) will be absent from Shift {2} on {3}.", guide.GuideName, guide.VolID, shift.Sequence, dt.ToLongDateString());
                        GuideSubstituteObject sub = dm.FetchForGuide(GuideID, ShiftID, dt);
                        if (sub.SubstituteID > 0)
                        {
                            NotifyList.Add(gdm.FetchGuide(sub.SubstituteID));
                            msg += String.Format(" {0} ({1}) will be substituting for this Guide.", sub.SubName, sub.Sub);
                        }
                        NotifyInterestedSubs = (sub.SubstituteID == 0 && dt < DateTime.Today.AddDays(2)) ? ShiftID : 0;
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Set(ex.Message);
                    }
                }
            }
            // Add a Sub ID for an existing absence.
            foreach (RepeaterItem itm in NeedSubRepeater.Items)
            {
                TextBox SubTextBox = (TextBox)itm.FindControl("SubTextBox");
                if (SubTextBox.Text != String.Empty)
                {
                    ShiftID = Convert.ToInt32(((HiddenField)itm.FindControl("ShiftIDHidden")).Value);
                    ShiftsObject shift = sdm.FetchShift(ShiftID);
                    try
                    {                      
                        sb.SubRequest(GuideID, dt, ShiftID, SubTextBox.Text);
                        RequestProcessed = true;
                          GuideSubstituteObject sub = dm.FetchForGuide(GuideID,ShiftID, dt);
                        if (sub.SubstituteID > 0)
                        {
                            NotifyList.AddRange(gdm.FetchCaptains(ShiftID));
                            NotifyList.Add(guide);
                            NotifyList.Add(gdm.FetchGuide(sub.SubstituteID));
                            msg += String.Format(" {0} ({1}) will be substituting for {2} on {3} Shift {4}.", sub.SubName, sub.Sub,guide.GuideName, dt.ToLongDateString(), shift.Sequence );
                        }                
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Set(ex.Message);
                    }
                }
            }


            // Remove Request for Sub
            foreach (RepeaterItem itm in AbsentShiftsRepeater.Items)
            {
                CheckBox NoNeedCheckBox = (CheckBox)itm.FindControl("NoNeedCheckBox");
                if (!NoNeedCheckBox.Checked)
                    continue;
                ShiftID = Convert.ToInt32(((HiddenField)itm.FindControl("ShiftIDHidden")).Value);
                ShiftsObject shift = sdm.FetchShift(ShiftID);
                NotifyList.AddRange(gdm.FetchCaptains(ShiftID));
                GuideSubstituteObject sub = dm.FetchForGuide(GuideID, ShiftID, dt);
                if (sub.SubstituteID > 0)
                    NotifyList.Add(gdm.FetchGuide(sub.SubstituteID));
                RequestProcessed = true;
                dm.DeleteForGuideAndDate(GuideID, dt);
                msg += String.Format("{0} was planning to be absent on Shift {1} on {2}, will be present and no longer needs a substitute.",
                    guide.GuideName, guide.Sequence, dt.ToLongDateString());
            }
            // Remove Sub offer
            if (!RequestProcessed && NoSubCheckBox.Checked)
            {

                try
                {
                    ObjectList<GuideSubstituteObject> subs = dm.FetchForSub(GuideID, dt);
                    ObjectList<GuideDropinsObject> dropins = ddm.FetchForGuide(GuideID, dt);

                    // You can be bailing from multiple shifts at the same time.
                    if (subs.Count > 0)
                    {

                        GuideSubstituteObject ssub = subs[0];
                        string seqs = String.Empty;
                        string sep = String.Empty;
                        foreach (GuideSubstituteObject sub in subs)
                        {
                            NotifyList.AddRange(gdm.FetchCaptains(sub.ShiftID));
                            GuidesObject guide1 = gdm.FetchGuide(sub.GuideID);
                            if (!NotifyList.Contains(guide1))
                                NotifyList.Add(guide1);

                            seqs += sep + sub.Sequence.ToString();
                            sep = ", ";
                        }
                        RequestProcessed = true;
                        dm.DeleteForSubAndDate(GuideID, dt);
                        msg = String.Format("{0} ({1}) who previously planned to substitute on Shift {2} on {3} can no longer do so.",
                        ssub.SubName, ssub.Sub, seqs, ssub.SubDate.ToLongDateString());
                    }
                    if (dropins.Count > 0)
                    {
                        GuideDropinsObject ddropin = dropins[0];

                        string seqs = String.Empty;
                        string sep = String.Empty;
                        foreach (GuideDropinsObject dropin in dropins)
                        {
                            NotifyList.AddRange(gdm.FetchCaptains(dropin.ShiftID));
                            seqs += sep + dropin.Sequence.ToString();
                            sep = ", ";
                        }
                        RequestProcessed = true;
                        ddm.DeleteForGuideAndDate(GuideID, dt);
                        msg = String.Format("{0} ({1}) who previously planned to substitute on Shift {2} on {3} can no longer do so.",
                      ddropin.GuideName, ddropin.VolID, seqs, ddropin.DropinDate.ToLongDateString());
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage.Set(ex.Message);
                }

            }
            // Offer to sub for another shift.
            if (!RequestProcessed)
            {
               
                GridView gv = (e.CommandArgument.ToString() == "1") ? GridView1 : GridView2;
                int Index = (e.CommandArgument.ToString() == "1") ? 4 : 3;
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox cb = null;
                        try
                        {
                            cb = (CheckBox)row.Cells[Index].Controls[3];
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage.Set(ex.Message);
                        }
                        if (cb != null && cb.Checked)
                        {
                            int indx = row.RowIndex;
                            int GuideSubstituteID = Convert.ToInt32(GridView1.DataKeys[indx].Value);
                            GuideSubstituteObject sub = dm.FetchRecord("GuideSubstituteID", GuideSubstituteID);
                            ShiftID = sub.ShiftID;
                            sub.SubstituteID = GuideID;
                            try
                            {
                                if (sb.AddSub(sub))
                                {
                                    RequestProcessed = true;
                                    DoSub = true;
                                    sub = dm.FetchRecord("GuideSubstituteID", GuideSubstituteID);
                                    NotifyList.AddRange(gdm.FetchCaptains(sub.ShiftID));
                                    GuidesObject guide1 = gdm.FetchGuide(sub.GuideID);
                                    if (!NotifyList.Contains(guide1))
                                        NotifyList.Add(guide1);
                                    msg = String.Format("{0} ({1}) will substitute for {2} ({3}) for shift {4} on {5}.",
                                        sub.SubName, sub.Sub, sub.GuideName, sub.VolID, sub.Sequence, sub.SubDate.ToLongDateString());
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage.Set(ex.Message);
                            }
                            break;
                        }
                    }
                }
            }
             
            if (!RequestProcessed && NewDropCheckBox.Checked)
            {
                if (ShiftSelect.SelectedIndex < 0)
                {
                    ErrorMessage.Set("Please select a shift for drop-in.");
                    return;
                }
                bool DoDropin = true;
                foreach (GuideSubstituteObject sub in dm.FetchForSub(GuideID, dt))
                {
                    if (sub.ShiftID == Convert.ToInt32(ShiftSelect.SelectedValue))
                    {
                        // Already subbing on this shift.. No Dropin
                        ErrorMessage.Set("You may not drop in on  a shift for which you are substituting.");
                        DoDropin = false;
                    }
                }
                if (DoDropin)
                {
                    GuideDropinsObject dropin = new GuideDropinsObject();
                    dropin.DropinDate = dt;
                    ShiftID = Convert.ToInt32(ShiftSelect.SelectedValue);
                    try
                    {
                        dropin.ShiftID = ShiftID;
                        dropin.GuideID = GuideID;
                        ddm.Save(dropin);
                        DoSub = true;
                        RequestProcessed = true;
                        dropin = ddm.FetchForGuide(GuideID, dt, dropin.ShiftID);
                        NotifyList.AddRange(gdm.FetchCaptains(dropin.ShiftID));
                        msg = String.Format("{0} ({1}) is planning to be a Drop-in guide for shift {2} on {3}.",
                            dropin.GuideName, dropin.VolID, dropin.Sequence, dropin.DropinDate.ToLongDateString());
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Set(ex.Message);
                    }
                }
            }
            if (!NewDropCheckBox.Checked && ShiftSelect.SelectedValue != String.Empty)
            {
                ErrorMessage.Set("You have selected a Shift for Drop-in but not checked the Drop-in CheckBox.");
                    return;
            }
            if (RequestProcessed)
            {
                UpdateCells();
                ClearCheckBoxes();

                if (e.CommandArgument.ToString() == "1")
                    GridView1.DataBind();
                else
                {
                    GridView2.DataBind();
                    GridView3.DataBind();
                }

                MultiView1.SetActiveView(View2);
                if (NeedSub)
                {
                    InfoLabel.Visible = true;
                    InfoLabel.Text = @" Please remember that you are <b>always responsible</b> for finding a substitute.
            This on-line request system is just for your convenience. <b> If you do not get a substitute,
              or five responses to your request within a week  of the date you will be absent,
                    it is recommended that you get on the phone and call volunteers to find a substitute directly.</b> ";
                }
                if (DoSub)
                {
                    InfoLabel.Visible = true;

                    ConfirmLink.Visible = true;
                    GuideSubstituteObject sub = sb.SelectSubstituteShift(GuideID, dt, ShiftID);
                    ConfirmLink.CalendarType = sub.CalendarType;
                    ConfirmLink.HostName = GetHostName();
                    ConfirmLink.Description = String.Format("Shift {0}", sub.Sequence);
                    ConfirmLink.Date = sub.SubDate;
                    ConfirmLink.StartTime = sub.ShiftStart;
                    ConfirmLink.EndTime = sub.ShiftEnd;
                    InfoLabel.Text = String.Format("You have offered to substitute on {0:d} for shift {1}.", dt, sub.Sequence);
                }

                
                sb.Notify(NotifyList, msg);
                if (NotifyInterestedSubs > 0)
                    sb.NotifyOffers(GuideID, NotifyInterestedSubs, dt);
                msg = "The following message has been sent: <br /> " + msg;
                MsgLabel.Text = msg;
                RecipientsRepeater.DataSource = NotifyList;
                RecipientsRepeater.DataBind();
            }
            else
            {
               ErrorMessage.Set( "No changes were requested.");
            }
 
        }
    }
}