using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace MBAV
{
    public partial class ShiftRoster : System.Web.UI.Page
    {
        protected void DoReport(object sender, EventArgs e)
        {
            if (ShiftSelect.SelectedIndex <= 0)
            {
                ErrorMessage.Set("Please select a shift");
                return;
            }
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("dt", DateSelect1.Text);

            ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ShowPrintButton = true;
            ReportViewer1.Visible = true;
        }
        protected void RefreshShifts(object sender, EventArgs e)
        {
            ShiftSelect.Items.Clear();
            ShiftSelect.Items.Add(new ListItem("(Select a Shift)", "0"));
            ShiftSelect.DataBind();
            ReportViewer1.Visible = false;
        }
    }
}