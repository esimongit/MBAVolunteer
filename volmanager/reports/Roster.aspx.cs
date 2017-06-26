using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

using NQN.DB;

namespace VolManager.Reports
{
    public partial class Roster : System.Web.UI.Page
    {
        protected void DoReport(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("dt", Request.QueryString["ShiftDate"]);
            ShiftsDM dm = new ShiftsDM();
            string shiftname = String.Empty;
            ShiftsObject shift = dm.FetchShift(Convert.ToInt32(Request.QueryString["ShiftID"]));
            if (shift == null)
                return;
            shiftname = shift.ShiftName;
            if (shift.Recurring)
                ReportViewer1.LocalReport.ReportPath = "Reports/Roster.rdlc";
            else
                ReportViewer1.LocalReport.ReportPath = "Reports/SpecialRoster.rdlc";
            parameters[1] = new ReportParameter("ShiftName", shiftname);
            ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ShowPrintButton = true;
            ReportViewer1.Visible = true;
        }
    }
}