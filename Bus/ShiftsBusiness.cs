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
            ShiftsObject obj = dm.ShiftWithDate(ShiftID, dt);
            if (obj != null)
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
        public ObjectList<ShiftSummaryObject> ShiftsToday()
        {
            ShiftsDM dm = new ShiftsDM();
            return dm.ShiftCountReport(DateTime.Today, DateTime.Today);
        }
        public ObjectList<ShiftsObject> ShiftsOnDate(DateTime dt)
        {
            ShiftsDM dm = new ShiftsDM();
            ObjectList<ShiftsObject> dList = dm.ShiftsForDate(dt);
            GuidesDM gdm = new GuidesDM();
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();

            for (int i = 0; i < dList.Count; i++)
            {
                int Current = gdm.FetchForShift(dList[i].ShiftID).Count;
                foreach (GuideSubstituteObject sub in sdm.FetchForShift(dList[i].ShiftID, dt))
                {
                    if (sub.NoSub)
                        Current--;
                }
               Current += ddm.FetchForShift(dList[i].ShiftID, dt).Count;
                dList[i].Attendance = Current;
            }
            return dList;
        }
    }
}
