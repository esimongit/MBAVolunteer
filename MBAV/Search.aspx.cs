using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBAV
{
    public partial class Search : System.Web.UI.Page
    {
        protected void DoSearch(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        protected void ClosePage(object sender, EventArgs e)
        {
            Response.Redirect("SubstituteCalendar.aspx");
        }
    }
}