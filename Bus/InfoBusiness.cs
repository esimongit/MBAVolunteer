using System;
using System.Collections.Generic;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class InfoBusiness
    {
        public ObjectList<ShiftsObject> InfoList(DateTime StartDate, DateTime EndDate)
        {
            if (StartDate == DateTime.MinValue || EndDate == DateTime.MinValue)
                return null;
            ShiftsDM dm = new ShiftsDM();
            int StartMonth = StartDate.Month;
            int StartYr = StartDate.Year;
            int EndMonth = EndDate.Month;
            int EndYr = EndDate.Year; 
            ObjectList<ShiftsObject> dList = new ObjectList<ShiftsObject>();
            for (int yr = StartYr; yr <= EndYr; yr++)
            {
                if (yr > StartYr)
                    StartMonth = 1;
                if (yr < EndYr)
                    EndMonth = 12;
                for(int mo = StartMonth; mo <= EndMonth; mo++)
                {

                    ObjectList<ShiftsObject> mlist = dm.InfoForMonth(yr, mo);
                    mlist.RemoveAll(x => x.ShiftDate < StartDate);
                    dList.AddRange(mlist);
                }

            }
            return dList;

        }
    }
}
