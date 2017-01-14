using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;
using NQN.DB;

namespace VolManager
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipBusiness mb = new MembershipBusiness();
           
            Label1.Text = String.Format("Current Guide IDs On Line: {0} ",   mb.GetCurrentUsers("MBAV"));
            if (!Page.IsPostBack)
            {
                GuideDropinsDM gdm = new GuideDropinsDM();
                GuideSubstituteDM dm = new GuideSubstituteDM();
                Label2.Text = String.Format("Outstanding Guide Requests: {0}; Requests with Substitutes: {1}; Future Drop-ins: {2}.",
                    dm.OpenRequests(), dm.RequestsWithSubs(), gdm.FutureDropins());
            }
        }
        protected void ShiftSelected(object sender, EventArgs e)
        {
            DataKey dk = GridView2.SelectedDataKey;
            Response.Redirect(String.Format("~/ShiftDetail.aspx?ShiftID={0}&ShiftDate={1}", dk.Values[0].ToString(), ((DateTime)dk.Values[1]).ToShortDateString()));
        }
    }
}
