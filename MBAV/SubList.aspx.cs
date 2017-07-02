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
                int RoleID = 0;
                try
                {
                    RoleID = Convert.ToInt32(Session["RoleID"]);
                }
                catch { }
                string RoleName = RoleID == 0 ? String.Empty : "Info Center";
                TitleLabel.Text = String.Format("{0} Substitutes Available for {1}", RoleName, shift.ShortName);
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
                TitleLabel.Text = String.Format("Substitutes Available for {0}", shift.ShortName);
            }
        }
    }
}