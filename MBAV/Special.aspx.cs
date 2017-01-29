using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace MBAV
{
    public partial class Special : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 
            }
        }
        protected void SpecialShiftChanged(object sender, EventArgs e)
        {
            GuideDropinsDM dm = new GuideDropinsDM();
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            if (GuideID == 0)
                return;
            CheckBox cb = (CheckBox)sender;
            HiddenField hf = (HiddenField)(cb.Parent.FindControl("ShiftIDHidden"));
            int ShiftID = Convert.ToInt32(hf.Value);
            if (cb.Checked)
                cb.Checked = dm.AddSpecial(GuideID, ShiftID);
            else
            {
                dm.DeleteSpecial(GuideID, ShiftID);
                cb.Checked = false;
            }
            cb.Focus();
            InfoMessage.Set("Changes Accepted");
            SpecialRepeater.DataBind();
        }
    }
}