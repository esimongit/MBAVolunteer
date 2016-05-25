using System;
using System.Web;

namespace NQN.Core
{
    public class CookieManager
    {
        public static void SetCookie(string CookieName, string Parameter, string Value)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie[Parameter] = Value;
            cookie.Expires = DateTime.Now.AddDays(3000);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string ReadCookie(string CookieName, string Parameter)
        {
            string Location = String.Empty;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                Location = HttpContext.Current.Request.Cookies[CookieName][Parameter];
            }
            return Location;
        }
    }
}
