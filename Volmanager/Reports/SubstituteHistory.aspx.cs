using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

namespace VolManager.Reports
{
    public partial class SubstituteHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateRangeSelect.StartDate = "1/1/2016";
                DateRangeSelect.EndDate = DateTime.Today.ToShortDateString();

            }
        }
        protected void DoReport(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("StartDate", DateRangeSelect.bDate);
            parameters[1] = new ReportParameter("EndDate", DateRangeSelect.eDate);
            ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ShowPrintButton = true;
            ReportViewer1.Visible = true;
        }
    }
}