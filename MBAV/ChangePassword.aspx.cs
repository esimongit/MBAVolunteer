using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBAV.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void ClosePage(object sender, EventArgs e)
        {
            Response.Redirect("~/SubstituteCalendar.aspx");

        }
    }
}
