using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace VolManager
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipBusiness mb = new MembershipBusiness();
           
            Label1.Text = String.Format("<b> Current Volunteers On Line:</b> {0} ",   mb.GetCurrentUsers("MBAV"));
        }
    }
}
