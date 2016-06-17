using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace MBAV
{
    public partial class DropIns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ClosePage(object sender, EventArgs e)
        {
            Response.Redirect("~/SubstituteCalendar.aspx");

        }
        protected void DoSubmit(object sender, EventArgs e)
        {

            GuideDropinsDM dm = new GuideDropinsDM();
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            int ShiftID = 0;
            try
            {
                ShiftID = Convert.ToInt32(ShiftSelect.SelectedValue);
            }
            catch 
            { }
               
            if ((ShiftID == 0) ||  (GuideID == 0))
                return;
            foreach (RepeaterItem itm in Repeater1.Items)
            {
                CheckBox cb = (CheckBox)itm.FindControl("DateCheckBox");
                HiddenField hf = (HiddenField)(cb.Parent.FindControl("DropinIDHidden"));
                Label DateLabel = (Label)cb.Parent.FindControl("DateLabel");
                DateTime dt = DateTime.Parse(DateLabel.Text);
                int DropinID = Convert.ToInt32(hf.Value);
                if (cb.Checked)
                {
                    if (DropinID < 0)
                    {
                        dm.Save(GuideID, ShiftID, dt);
                        
                    }
                }
                else if (DropinID > 0)
                {
                    dm.Delete(DropinID);
                   
                }
            }
            InfoMessage.Set("Changes Accepted");
            Repeater1.DataBind();
        }
     
    }
}