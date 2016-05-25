using System;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for alert
/// </summary>
namespace VolManager
{
    public class AlertMsg
    {
        // <summary>
        // Shows a client-side JavaScript alert in the browser
        // </summary>
        // <param name="messsage">The message to appear i the alert. </param>
        public static void Show(String message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("\"", "\\\"");
            cleanMessage = cleanMessage.Replace("'", "\'");
            string script = "<script type='text/javascript'>alert(\"" + cleanMessage + "\");</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager cs = page.ClientScript;
            // Checks if the handler is a Page and that the script isn't already on the Page
            if (!cs.IsClientScriptBlockRegistered("AlertMsg"))
            {
                cs.RegisterClientScriptBlock(typeof(AlertMsg), "AlertMsg", script);
            }
        }
    }
    public class NQNWOpen
    {
        public static void Show(string rpt)
        {
            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager cs = page.ClientScript;
            string script = "<script type='text/javascript'>javascript:window.open('" + rpt + "','sec','resizable=yes,width=860,height=600,scrollbars=yes,toolbar=yes');</script>";
            if (!cs.IsClientScriptBlockRegistered(typeof(NQNWOpen), "NQNWOpen"))
            {
                cs.RegisterClientScriptBlock(typeof(NQNWOpen),
                "NQNWOpen", script);
            }
        }
        public static void Message(string rpt)
        {
            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager cs = page.ClientScript;

            string script = "<script type='text/javascript'>javascript:window.open('" + rpt + "','sec','resizable=yes,width=600,height=400,scrollbars=yes,toolbar=yes');</script>";

            if (!cs.IsClientScriptBlockRegistered(typeof(NQNWOpen), "NQNWOpen"))
            {
                cs.RegisterClientScriptBlock(typeof(NQNWOpen),
                "NQNWOpen", script);
            }
        }
    }
}
