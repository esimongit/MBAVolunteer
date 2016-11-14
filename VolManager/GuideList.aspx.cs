using System;
using System.Collections.Generic; 
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;
using NQN.Bus;

namespace VolManager
{
    public partial class GuideList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MultiView1.SetActiveView(View1);
                GuidesDM dm = new GuidesDM();
                DataTable taskTable = dm.Search("", 0,0, false);
                Session["TaskTable"] = taskTable;
                ShiftSelect.DataBind();
                RoleSelect.DataBind();
                //Bind the GridView control to the data source.
                GridView1.DataSource = Session["TaskTable"];
                GridView1.DataBind();
                if (Request.QueryString["GuideID"] != null)
                    MultiView1.SetActiveView(View2);
            }
        }
        protected void DoSearch(object sender, EventArgs e)
        {
             
             
                GuidesDM dm = new GuidesDM();
                DataTable taskTable = dm.Search(PatternTextBox.Text, Convert.ToInt32(ShiftSelect.SelectedValue), Convert.ToInt32(RoleSelect.SelectedValue), SearchInactiveCheckBox.Checked);
                Session["TaskTable"] = taskTable;

                //Bind the GridView control to the data source.
                GridView1.DataSource = Session["TaskTable"];
                GridView1.DataBind();
 
        }
        protected void TaskGridView_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;

            if (dt != null)
            {

                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }


        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        protected void OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
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
            InfoMessage.Set("New Guide Added");
            MultiView1.SetActiveView(View1);
        }
        protected void GuideSelected(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
            Session["GuideID"] = GridView1.SelectedValue;
            GuideEdit1.SetupEdit();
        }
         protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
         protected void ToView3(object sender, EventArgs e)
         {
             MultiView1.SetActiveView(View3);
         }
        protected void AddView(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View3);
        }
       
       
    }
}