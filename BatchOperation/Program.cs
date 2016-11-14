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
            //sb.SendReminders();
            
            sb.SubNotices(StaticFieldsObject.StaticValue("GuideURL"));
        }
    }
}
