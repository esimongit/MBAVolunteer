using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace MBAV
{
    public partial class SubList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShiftSelect.DataBind();
                int ShiftID = 0;
                try
                {
                    ShiftID = Convert.ToInt32(Request.QueryString["ShiftID"]);
                }
                catch { }
                if (ShiftID == 0)
                {
                    TitleLabel.Text = "Select a Shift";
                    return;
                }
                ShiftsDM dm = new ShiftsDM();
                ShiftsObject shift = dm.FetchRecord("ShiftID", ShiftID);
                TitleLabel.Text = String.Format("Guide Substitutes Available for {0}", shift.ShortName);
                ShiftSelect.SelectedValue=ShiftID.ToString();
                GridView1.DataBind();
            }
        }
        protected void DoList(object sender, EventArgs e)
        {
            GridView1.DataBind();
            int ShiftID = 0;
            try
            {
                ShiftID = Convert.ToInt32(ShiftSelect.SelectedValue);
            }
            catch { }
            if (ShiftID > 0)
            {
                ShiftsDM dm = new ShiftsDM();
                ShiftsObject shift = dm.FetchRecord("ShiftID", ShiftID);
                TitleLabel.Text = String.Format("Guide Substitutes Available for {0}", shift.ShortName);
            }
        }
    }
}