using System;
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
            {
                MultiView1.SetActiveView(View1);
                GridView1.Columns[2].Visible = true;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = false;
            }
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
        //Select filter change on Shift List
        protected void RecurSpecial(object sender, EventArgs e)
        {
            if (RecurringSelect.SelectedValue == "True")
            {
                GridView1.Columns[2].Visible = true;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = false;
            }
            else
            {
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = true;
            }
            GridView1.DataBind();
        }
        protected void RecurringChanged(object sender, EventArgs e)
        {
            Panel RecurringPanel = (Panel)FormView2.FindControl("RecurringPanel");
            Panel SpecialPanel = (Panel)FormView2.FindControl("SpecialPanel");
            DateSelector DateSelect = (DateSelector)FormView2.FindControl("DateSelect");
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
        protected void OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                if (ex.Message.Contains("FK"))
                    ErrorMessage.Set("This shift has guides who must first be removed.");
                else
                    ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
                return;
            }
          
            InfoMessage.Set("Shift Deleted"); 
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
            InfoMessage.Set("Shift Added or Changed");
            MultiView1.SetActiveView(View1);
        }
        protected void GuideSelected(object sender, EventArgs e)
        {
            Session["GuideID"] = GridView2.SelectedValue;
            MultiView1.SetActiveView(View3);
        }
    }
}