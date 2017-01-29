using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VolManager
{
    public partial class ShiftTimes : System.Web.UI.Page
    {
        protected void OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
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
                AddForm.Visible = false;
            }
        }
        protected void ShowAdd(object sender, EventArgs e)
        {
            AddForm.Visible = true;
        }
        protected void HideAdd(object sender, EventArgs e)
        {
            AddForm.Visible = false;
        }
    }
}