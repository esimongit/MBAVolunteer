using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class ShiftsObject : RootObject
	{
#region AutoAttributes
		private int _shiftid = 0;
		public int ShiftID 
		{
			get
			{
				return _shiftid;
			}
			set
			{
				 _shiftid = value;
			}
		}
		private string _shiftname = String.Empty;
		public string ShiftName 
		{
			get
			{
				return _shiftname;
			}
			set
			{
				 _shiftname = value;
			}
		}
		private int _dow = 0;
		public int DOW 
		{
			get
			{
				return _dow;
			}
			set
			{
				 _dow = value;
			}
		}
		private bool _aweek = false;
		public bool AWeek 
		{
			get
			{
				return _aweek;
			}
			set
			{
				 _aweek = value;
			}
		}
		private bool _bweek = false;
		public bool BWeek 
		{
			get
			{
				return _bweek;
			}
			set
			{
				 _bweek = value;
			}
		}
		private int _sequence = 0;
		public int Sequence 
		{
			get
			{
				return _sequence;
			}
			set
			{
				 _sequence = value;
			}
		}
		private string _shortname = String.Empty;
		public string ShortName 
		{
			get
			{
				return _shortname;
			}
			set
			{
				 _shortname = value;
			}
		}
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
#endregion
        public bool Selected
        {
            get;
            set;
        }
        public DateTime ShiftDate
        {
            get;
            set;
        }
        public string Captains
        {
            get;
            set;
        }
        public string InfoDesk
        {
            get;
            set;
        }
        public DateTime ShiftStart
        {
            get;
            set;
        }
        public DateTime ShiftEnd
        {
            get;
            set;
        }
		public ShiftsObject()
		{
			_tablename = "Shifts";
			_primarykey = "ShiftID";
		}
        public string Url(DateTime dt)
        {
            return String.Format("/ShiftDetail.aspx?ShiftID={0}&ShiftDate={1:d}", _shiftid, dt);
        }
	}
}
