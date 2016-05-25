using System;
using System.Collections.Generic;
using System.Text;
using NQN.Bus;

namespace BatchOperation
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubstitutesBusiness sb = new SubstitutesBusiness();
            sb.SendReminders();
        }
    }
}
