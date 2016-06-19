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
        protected void GuideSelected(object sender, EventArgs e)
        {
            Session["GuideID"] = GridView2.SelectedValue;
            MultiView1.SetActiveView(View3);
        }
    }
}