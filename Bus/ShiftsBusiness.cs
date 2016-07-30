using System;
using System.Collections.Generic; 
using System.Text;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class ShiftsBusiness
    { 
        public ObjectList<ShiftsObject> SelectShiftsForMonth(int Yr, int Mo)
        {
            ShiftsDM dm = new ShiftsDM();
             
            ObjectList<ShiftsObject> CurrentShifts = new ObjectList<ShiftsObject>();
           CurrentShifts = dm.ShiftsForMonth(Yr, Mo);
            
            return CurrentShifts;
        }

        protected int ShiftSort(ShiftsObject x, ShiftsObject y)
        {
            int ret = 0;
            ret = x.ShiftDate.CompareTo(y.ShiftDate);
            if (ret == 0)
                ret = x.Sequence.CompareTo(y.Sequence);
            return ret;
        }
    }
}
