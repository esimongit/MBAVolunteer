﻿using System;
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
    public partial class ShiftCalendar : System.Web.UI.Page
    {
        protected ObjectList<ShiftsObject> CurrentShifts = null;

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
            
            foreach (ShiftsObject shift in sList)
            {
                txt = new Literal();
                txt.Text = "<br />";
                e.Cell.Controls.Add(txt);
                HyperLink lc = new HyperLink();
                lc.Text = shift.ShiftName;
                lc.NavigateUrl = shift.Url(dt);
                e.Cell.Controls.Add(lc);
            }
           
        }

        protected void GetCurrentShifts()
        {
            if (Session["VolCalDate"] == null)
                return;
            ShiftsDM dm = new ShiftsDM();
            DateTime curdate = DateTime.Parse(Session["VolCalDate"].ToString());
            CurrentShifts = dm.ShiftsForMonth(curdate.Year, curdate.Month);
             
        }
        protected void MonthChanged(Object sender, MonthChangedEventArgs e)
        {
            CurrentShifts = null;
            Session["VolCalDate"] = e.NewDate;
        }
          
    }
}
