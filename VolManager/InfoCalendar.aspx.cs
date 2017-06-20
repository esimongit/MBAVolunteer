using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NQN.DB;
using NQN.Core;
using NQN.Bus;

namespace VolManager
{
    public partial class InfoCalendar : System.Web.UI.Page
    {
        protected ObjectList<ShiftsObject> CurrentShifts = null;
        int WeekdayCritical = 0;
        int WeekendCritical = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["VolCalDate"] == null)
                    Session["VolCalDate"] = DateTime.Today;
                Calendar1.SelectedDate = (DateTime)Session["VolCalDate"];
                Calendar1.VisibleDate = (DateTime)Session["VolCalDate"];

            }
        }
        protected void DayRenderHandler(object source, DayRenderEventArgs e)
        { 
            DateTime dt = e.Day.Date;
            bool Weekend = dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
            if (dt < DateTime.Today)
                return;
           
            string clr = ShiftsDM.IsAWeek(dt) ? "#aaaaff": "#ffaaaa";
            
            if (e.Day.IsOtherMonth)
                return;
            if (CurrentShifts == null || CurrentShifts.Count == 0)
            {
                GetCurrentShifts();
            }
            if (CurrentShifts == null)
                return;
            List<ShiftsObject> sList = CurrentShifts.FindAll(x => x.ShiftDate == e.Day.Date);
            
            
            string DayNumberText = "<span style='color:#330088'>" + e.Day.DayNumberText;

            System.Drawing.ColorConverter conv = new System.Drawing.ColorConverter();
            e.Cell.Text = "";
            e.Cell.BackColor = (System.Drawing.Color)conv.ConvertFromString(clr);

            Literal txt = new Literal();
            txt.Text = DayNumberText;
            e.Cell.Controls.Add(txt);

            int LastShift = 0;
            foreach (ShiftsObject shift in sList)
            {
                txt = new Literal();
                txt.Text = "<div style='clear:both'></div>";
                e.Cell.Controls.Add(txt);
                int padding = (LastShift != shift.ShiftID) ? 10 : 0;
                 
                HyperLink lc = new HyperLink();
                lc.Text = String.Format("<div style='float:left;padding-top:{0}px; width:40px'>{1}</div><div style='float:left;padding-top:{0}px;padding-left:3px'>{2} {3}</div><div style='clear:both'></div>",
                    padding, shift.ShortName, shift.InfoFirst, shift.InfoLast);
                lc.NavigateUrl = shift.Url(dt);
                e.Cell.Controls.Add(lc);
                
                LastShift = shift.ShiftID;
            }
            //txt = new Literal();
            //txt.Text = "</div>";
            //e.Cell.Controls.Add(txt);
        }

        protected void GetCurrentShifts()
        {
            if (Session["VolCalDate"] == null)
                return;
            ShiftsDM dm = new ShiftsDM();
            DateTime curdate = DateTime.Parse(Session["VolCalDate"].ToString());
            CurrentShifts = dm.InfoForMonth(curdate.Year, curdate.Month);
            WeekdayCritical = Convert.ToInt32(StaticFieldsObject.StaticValue("WeekdayCritical"));
            WeekendCritical = Convert.ToInt32(StaticFieldsObject.StaticValue("WeekendCritical"));
        }
        protected void MonthChanged(Object sender, MonthChangedEventArgs e)
        {
            CurrentShifts = null;
            Session["VolCalDate"] = e.NewDate;
        }
          
    }
}
