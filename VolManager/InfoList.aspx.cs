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
    public partial class InfoList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateSelect.StartDate = DateTime.Today.ToShortDateString();
                DateSelect.EndDate = DateTime.Today.AddDays(120).ToShortDateString();
                NQNGridView1.DataBind();
            }
        }
        protected void GuideSelected(object sender, EventArgs e)
        {
            DataKey dk = NQNGridView1.DataKeys[NQNGridView1.SelectedIndex];
            int GuideID = Convert.ToInt32(dk[0]);
            Response.Redirect(String.Format("~/GuideList.aspx?GuideID={0}", GuideID));
        }
    }
}