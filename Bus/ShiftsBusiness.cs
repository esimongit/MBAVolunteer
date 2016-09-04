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
        public ShiftsObject SelectShift(int ShiftID, DateTime dt)
        {
            ShiftsDM dm = new ShiftsDM();
            ShiftsObject obj = dm.FetchShift(ShiftID);
            obj.ShiftDate = dt;
            return obj;
        }
        protected int ShiftSort(ShiftsObject x, ShiftsObject y)
        {
            int ret = 0;
            ret = x.ShiftDate.CompareTo(y.ShiftDate);
            if (ret == 0)
                ret = x.Sequence.CompareTo(y.Sequence);
            return ret;
        }
        public ObjectList<ShiftsObject> ShiftsToday()
        {
            return ShiftsOnDate(DateTime.Today);
        }
        public ObjectList<ShiftsObject> ShiftsOnDate(DateTime dt)
        {
            ShiftsDM dm = new ShiftsDM();
            ObjectList<ShiftsObject> dList = dm.ShiftsForDate(dt);
            GuidesBusiness gb = new GuidesBusiness();
            for (int i = 0; i < dList.Count; i++)
            {
                dList[i].Attendance = gb.RosterList(dList[i].ShiftID, dt).Count;
            }
            return dList;
        }
    }
}
