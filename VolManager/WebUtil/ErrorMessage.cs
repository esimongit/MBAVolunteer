using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Store and forward current Error Messages
/// </summary>
namespace VolManager
{
    public class ErrorMessage
    {
        public ErrorMessage()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void Clear()
        {
            HttpContext.Current.Session.Remove("CurrentErrorMessage");
        }
        public static void Set(string msg)
        {
            HttpContext.Current.Session["CurrentErrorMessage"] = msg;
            AlertMsg.Show(msg);
        }
        public static void Add(string msg)
        {
            if (HttpContext.Current.Session["CurrentErrorMessage"] != null)
            {
                msg = HttpContext.Current.Session["CurrentErrorMessage"].ToString() + "<br/>" + msg;
            }
            HttpContext.Current.Session["CurrentErrorMessage"] = msg;
        }
        public static string Get()
        {
            string msg = "";
            if (HttpContext.Current.Session["CurrentErrorMessage"] != null)
            {
                msg = HttpContext.Current.Session["CurrentErrorMessage"].ToString();
            }
            return msg;
        }
    }
    public class InfoMessage
    {
        public InfoMessage()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void Clear()
        {
            HttpContext.Current.Session.Remove("CurrentInfoMessage");
        }
        public static void Set(string msg)
        {
            HttpContext.Current.Session["CurrentInfoMessage"] = msg;
        }
        public static void Add(string msg)
        {
            if (HttpContext.Current.Session["CurrentInfoMessage"] != null)
            {
                msg = HttpContext.Current.Session["CurrentInfoMessage"].ToString() + msg;
            }
            HttpContext.Current.Session["CurrentInfoMessage"] = msg;
        }
        public static string Get()
        {
            string msg = "";
            if (HttpContext.Current.Session["CurrentInfoMessage"] != null)
            {
                msg = HttpContext.Current.Session["CurrentInfoMessage"].ToString();
            }
            return msg;
        }
    }
}