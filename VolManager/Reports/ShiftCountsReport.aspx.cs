using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VolManager.Reports
{
    public partial class ShiftCountsReport : System.Web.UI.Page
    {
        protected void DoReport(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ShowPrintButton = true;
            ReportViewer1.Visible = true;
        }
    }
}