using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace VolManager
{
    public class DateValidator
    {
        public static string ErrorMessage = "Incorrect Date.  Date must be between 1/1/2000 and 12/31/2199";

        public static void CheckDate(ServerValidateEventArgs args)
        {
            args.IsValid = true;
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.ParseExact(args.Value, "d", null);
            }
            catch
            {
                args.IsValid = false;
                return;
            }
            if (dt < new DateTime(2000, 1, 1) ||
                dt > new DateTime(2199, 12, 31))
                args.IsValid = false;

        }
    }
}
