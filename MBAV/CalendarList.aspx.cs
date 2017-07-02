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
            RoleLabel.Text = guide.RoleName;

            
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
           // Offer to sub for another shift.
             

                GridView gv = GridView1;
                // Default Gridview1
                int Index = 4;

            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = (CheckBox)row.Cells[Index].FindControl("SubCheckBox");
                    if (cb != null && cb.Checked)
                    {
                        int indx = row.RowIndex;
                        int GuideSubstituteID = Convert.ToInt32(gv.DataKeys[indx].Value);
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
     
             
           
            if (RequestProcessed)
            {
                

                if (e.CommandArgument.ToString() == "1")
                    GridView1.DataBind();
                

                MultiView1.SetActiveView(View2);
            //    if (NeedSub)
            //    {
            //        InfoLabel.Visible = true;
            //        InfoLabel.Text = @" Please remember that you are <b>always responsible</b> for finding a substitute.
            //This on-line request system is just for your convenience. <b> If you do not get a substitute,
            //  or five responses to your request within a week  of the date you will be absent,
            //        it is recommended that you get on the phone and call volunteers to find a substitute directly.</b> ";
            //    }
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

                
               // sb.Notify(NotifyList, msg);
                if (NotifyInterestedSubs > 0)
                { 
                   //sb.NotifyOffers(GuideID, NotifyInterestedSubs, dt);
                }
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