﻿using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VolManager
{
    public partial class ShiftList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                MultiView1.SetActiveView(View1);
        }
        protected void ToView2(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }
        protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
        protected void ToView4(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View4);
            FormView2.DataSourceID = null;
            FormView2.DataSourceID = "ObjectDataSource2";
        }
        protected void RecurringChanged(object sender, EventArgs e)
        {
            Panel RecurringPanel = (Panel)FormView2.FindControl("RecurringPanel");
            Panel SpecialPanel = (Panel)FormView2.FindControl("SpecialPanel");
            DropDownList DateSelect = (DropDownList)FormView2.FindControl("DateSelect");
            CheckBox RecurringCheckBox = (CheckBox)sender;
            if (RecurringCheckBox.Checked)
            {
                RecurringPanel.Visible = true;
                SpecialPanel.Visible = false;
                DateSelect.Visible = false;
            }
            else
            {
                RecurringPanel.Visible = false;
                SpecialPanel.Visible = true;
                DateSelect.Visible = true;
            }
                
        }
        protected void OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
                return;
            }
            GridView1.DataBind();
            InfoMessage.Set("New Shift Added");
            MultiView1.SetActiveView(View1);
        }
        protected void GuideSelected(object sender, EventArgs e)
        {
            Session["GuideID"] = GridView2.SelectedValue;
            MultiView1.SetActiveView(View3);
        }
    }
}