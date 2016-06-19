using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
    public enum ShiftDayOfWeek
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7,
       
    }
    public class ShiftTimesObject : RootObject
	{
#region AutoAttributes
		private int _shifttimeid = 0;
		public int ShiftTimeID 
		{
			get
			{
				return _shifttimeid;
			}
			set
			{
				 _shifttimeid = value;
			}
		}
		private DateTime _shiftstart = DateTime.Now;
		public DateTime ShiftStart 
		{
			get
			{
				return _shiftstart;
			}
			set
			{
				 _shiftstart = value;
			}
		}
		private DateTime _shiftend = DateTime.Now;
		public DateTime ShiftEnd 
		{
			get
			{
				return _shiftend;
			}
			set
			{
				 _shiftend = value;
			}
		}
		private string _timeslotname = String.Empty;
		public string TimeSlotName 
		{
			get
			{
				return _timeslotname;
			}
			set
			{
				 _timeslotname = value;
			}
		}
#endregion

        public string TimeString
        {
            get
            {
                return _shiftstart.ToShortTimeString() + " - " + _shiftend.ToShortTimeString();
            }
        }
		public ShiftTimesObject()
		{
			_tablename = "ShiftTimes";
			_primarykey = "ShiftTimeID";
		}
	}
}
