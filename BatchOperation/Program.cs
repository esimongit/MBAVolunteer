using System;
using System.Collections.Generic;
using System.Text;
using NQN.Bus;
using System.Configuration;

namespace BatchOperation
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubstitutesBusiness sb = new SubstitutesBusiness();
            //sb.SendReminders();
            
            sb.SubNotices(ConfigurationManager.AppSettings["VolunteerUrl"].ToString());
        }
    }
}
