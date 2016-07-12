﻿using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
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
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideSubstituteObject sub = sdm.FetchForGuide(GuideID, DateTime.Parse(Request.QueryString["dt"]));
            GuidesObject guide = dm.FetchRecord("g.GuideID", GuideID);
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
            SeqLabel.Text = guide.Sequence.ToString();
            RoleLabel.Text = guide.RoleName;
            ShiftLabel.Text = guide.Sequence.ToString();
            ShiftLabel2.Text = guide.Sequence.ToString();
            UndoSubCell.Visible = (sub != null);
            NeedSubCell.Visible = guide.HasDate(dt) && (sub == null);
            HaveSubCell.Visible = guide.HasDate(dt) && (sub == null || sub.SubstituteID == 0);

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
            NeedSubCheckBox.Checked = false;
            NoNeedCheckbox.Checked = false;
            SubTextBox.Text = String.Empty;
            NoSubCheckBox.Checked = false;
            NewDropCheckBox.Checked = false;
        }
        protected string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }
        protected void DoSubmit(object sender, CommandEventArgs e)
        {
            GuidesDM gdm = new GuidesDM();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            SubOffersDM odm = new SubOffersDM();
            SubstitutesBusiness sb = new SubstitutesBusiness();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuidesObject> NotifyList = new ObjectList<GuidesObject>();
            DateTime dt = DateTime.Parse(Request.QueryString["dt"]);
            int NotifyInterestedSubs = 0;
            bool RequestProcessed = false;
            int GuideID = 0;
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
            if (NeedSubCheckBox.Checked || SubTextBox.Text != String.Empty)
            {
                try
                {
                    NotifyList.AddRange(gdm.FetchCaptains(guide.ShiftID));
                    NotifyList.Add(guide);
                    sb.SubRequest(GuideID, dt, SubTextBox.Text);
                    RequestProcessed = true;
                    msg = String.Format("{0} (1) will be absent from Shift {2} on {3}.", guide.GuideName, guide.VolID, guide.Sequence, dt.ToLongDateString());
                    GuideSubstituteObject sub = dm.FetchForGuide(GuideID, dt);
                    if (sub.SubstituteID > 0)
                    {
                        NotifyList.Add(gdm.FetchGuide(sub.SubstituteID));
                        msg += String.Format(" {0} ({1}) will be substituting for this Guide.", sub.SubName, sub.Sub);
                    }
                    NotifyInterestedSubs = (sub.SubstituteID == 0) ? guide.ShiftID : 0;
                }
                catch (Exception ex)
                {
                    ErrorMessage.Set(ex.Message);
                }
            }


            // Remove Request for Sub
            if (!RequestProcessed && NoNeedCheckbox.Checked)
            {
                NotifyList.AddRange(gdm.FetchCaptains(guide.ShiftID));
                GuideSubstituteObject sub = dm.FetchForGuide(GuideID, dt);
                if (sub.SubstituteID > 0)
                    NotifyList.Add(gdm.FetchGuide(sub.SubstituteID));
                RequestProcessed = true;
                dm.DeleteForGuideAndDate(GuideID, dt);
                msg = String.Format("{0} was planning to be absent on Shift {1} on {2}, will be present and no longer needs a substitute.",
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
                            NotifyList.Add(gdm.FetchGuide(sub.GuideID));

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
                            cb = (CheckBox)row.Cells[Index].Controls[1];
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
                           
                            sub.SubstituteID = GuideID;
                            try
                            {
                                if (sb.AddSub(sub))
                                {
                                    RequestProcessed = true;
                                    sub = dm.FetchRecord("GuideSubstituteID", GuideSubstituteID);
                                    NotifyList.AddRange(gdm.FetchCaptains(sub.ShiftID));
                                    NotifyList.Add(gdm.FetchGuide(sub.GuideID));
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
                        DoDropin = false;
                    }
                }
                if (DoDropin)
                {
                    GuideDropinsObject dropin = new GuideDropinsObject();
                    dropin.DropinDate = dt;

                    try
                    {
                        dropin.ShiftID = Convert.ToInt32(ShiftSelect.SelectedValue);
                        dropin.GuideID = GuideID;
                        ddm.Save(dropin);
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

                RecipientsRepeater.DataSource = NotifyList;
                RecipientsRepeater.DataBind();
                if (TestBox.Checked)
                {
                    PracticeLabel.Text = "Practice: No messages have been sent ";
                    msg = "The following  message would otherwise have been sent: <br />" + msg;
                }
                else
                {
                    msg = "The following message has been sent: <br /> " + msg;
                    sb.Notify(NotifyList, msg);
                    if (NotifyInterestedSubs > 0)
                        sb.NotifyOffers(GuideID, NotifyInterestedSubs, dt);
                }
                MsgLabel.Text = msg;

            }
        }
    }
}