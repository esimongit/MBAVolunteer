﻿using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;

namespace VolManager
{
    public partial class ShiftDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int ShiftID = 0;
                try
                {
                    ShiftID = Convert.ToInt32(Request.QueryString["ShiftID"]);
                    DateTime ShiftDate = DateTime.Parse(Request.QueryString["ShiftDate"]);
                    ReportLink.NavigateUrl = String.Format("~/Reports/Roster.aspx?ShiftID={0}&ShiftDate={1:d}", ShiftID, ShiftDate);
                    ShiftsDM dm = new ShiftsDM();
                    ShiftsObject shift = dm.ShiftWithDate(ShiftID, ShiftDate);
                    GridView1.Visible = shift.Recurring;
                    GridView2.Visible = !shift.Recurring;
                }
                catch
                {
                    return;
                }
               
            }
        }
        protected void ShowDropin(object sender, EventArgs e)
        {
            DropinView.Visible = true;
            DropinView.Focus();
        }
        protected void HideDropin(object sender, EventArgs e)
        {
            DropinView.Visible = false;
        }
        protected void OnChanged(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
            }
            else
            {
                GridView1.DataBind();
            }
        }
    }
}