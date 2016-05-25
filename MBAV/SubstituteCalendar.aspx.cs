using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;
using NQN.Bus;

namespace MBAV
{
    public partial class SubstituteCalendar : System.Web.UI.Page
    {
        protected ObjectList<CalendarDateObject> CurrentEvents = null;

        protected void Page_Load(object sender, EventArgs e)
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
            GuidesObject guide = dm.FetchRecord("GuideID", GuideID);
            
            NameLabel.Text = guide.GuideName;
            ShiftsDM sdm = new ShiftsDM();
            ShiftsObject shift = sdm.FetchRecord("ShiftID", guide.ShiftID);
            SubListLink.Text = "Substitutes for " + shift.ShortName;
            SubListLink.NavigateUrl = String.Format("SubList.aspx?ShiftID={0}", guide.ShiftID);
            if (!Page.IsPostBack)
            {
                if (Session["VolCalDate"] != null)
                {
                    Calendar1.SelectedDate = (DateTime)Session["VolCalDate"];
                    Calendar1.VisibleDate = (DateTime)Session["VolCalDate"];
                }
            }
            Calendar2.SelectedDate = DateTime.Today;
            Calendar2.VisibleDate = DateTime.Today;
            Calendar3.SelectedDate = DateTime.Today.AddMonths(1);
            Calendar3.VisibleDate = DateTime.Today.AddMonths(1);
            Calendar4.SelectedDate = DateTime.Today.AddMonths(2);
            Calendar4.VisibleDate = DateTime.Today.AddMonths(2);
            Calendar5.SelectedDate = DateTime.Today.AddMonths(3);
            Calendar5.VisibleDate = DateTime.Today.AddMonths(3); 
        }
        protected void DayRenderHandler(object source, DayRenderEventArgs e)
        { 
            DateTime dt = e.Day.Date;
            if (dt < DateTime.Today)
                return;
           
            string clr = ShiftsDM.IsAWeek(dt) ? "#aaaaff": "#ffaaaa";
            
            if (e.Day.IsOtherMonth)
                return;
            if (CurrentEvents == null || CurrentEvents.Count == 0)
            {
                GetCurrentEvents();
            }
           
            CalendarDateObject obj = CurrentEvents.Find(x => x.Dt == e.Day.Date);
            if (obj == null)
                return;
            string color = "#330088";
            string style = "normal";
            string decoration="inherit";
          
            if (obj.NeedSubstitutes)
            {
                color = "red";
                style = "italic";
            }
             
            if (obj.IsSubstitute)
            {
                decoration = "underline";
            }
            string lt = String.Format("<span style='color:{0};font-style:{1};text-decoration:{2} '>", color, style, decoration);
            string gt = "</span>";
            string DayNumberText =  e.Day.DayNumberText  ;
                DayNumberText = lt + DayNumberText + gt;
            
            System.Drawing.ColorConverter conv = new System.Drawing.ColorConverter();
            e.Cell.Text = "";
            e.Cell.BackColor = (System.Drawing.Color) conv.ConvertFromString(clr);
           
            Literal txt = new Literal();
            txt.Text = String.Format("<a href='SubRequest.aspx?dt={0}'>{1}</a>", e.Day.Date.ToShortDateString(), DayNumberText);
            e.Cell.Controls.Add(txt);
        }

        protected void GetCurrentEvents()
        {
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            SubstitutesBusiness sb = new SubstitutesBusiness();
            DateTime curdate = DateTime.Today;
            CurrentEvents = new ObjectList<CalendarDateObject>();
            for (int i = 0; i < 4; i++)
            {
                CurrentEvents.AddRange(sb.CalendarList(curdate.Year, curdate.Month, GuideID));
                curdate = curdate.AddMonths(1);
            }
        }
        protected void MonthChanged(Object sender, MonthChangedEventArgs e)
        {
            CurrentEvents = null;
            Session["VolCalDate"] = e.NewDate;
            if (e.NewDate > DateTime.Today.AddMonths(3))
                Calendar1.VisibleDate = e.PreviousDate;
            Calendar1.Focus();
        }
         protected void SubPage(object source, EventArgs e)
        { 
             Response.Redirect("~SubRequest.aspx");
         }
    }
}
