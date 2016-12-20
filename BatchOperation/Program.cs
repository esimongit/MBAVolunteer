using System;
using System.Collections.Generic;
using System.Text;
using NQN.Bus;
using NQN.DB;
using System.Configuration;
using NQN.Core;
namespace BatchOperation
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubstitutesBusiness sb = new SubstitutesBusiness();
            int cnt1 = 0;
            int cnt2 = 0;
            cnt1 = sb.SendReminders();
            
            cnt2 = sb.SubNotices(StaticFieldsObject.StaticValue("GuideURL"));

            EmailBusiness eb = new EmailBusiness();
            string msg = String.Format("{0} Reminders were sent. {1} Notices were sent.", cnt1, cnt2);
            eb.SendMail("substitute@mbayaq.org", ConfigurationManager.AppSettings["ErrorEmail"] , "MBAV Night notices sent", msg, true);
        }
    }
}
