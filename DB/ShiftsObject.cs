using System;
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
        private bool _recurring = false;
        public bool Recurring
        {
            get
            {
                return _recurring;
            }
            set
            {
                _recurring = value;
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
        private int _quota = 0;
        public int Quota
        {
            get
            {
                return _quota;
            }
            set
            {
                _quota = value;
            }
        }
        private DateTime _shiftdate = DateTime.MinValue;
        public DateTime ShiftDate
        {
            get
            {
                return _shiftdate;
            }
            set
            {
                _shiftdate = value;
            }
        }
        #endregion
        public bool Selected
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
        public int Attendance
        {
            get;set;
        }
        public bool NonRecurring
        {
            get
            {
                return !_recurring;
            }
        }
        public int Needed
        {
            get
            {
                return ShiftQuota - Attendance;
            }
        }
        public int ShiftQuota
        {
            get; set;
        }
        public string Type
        {
            get
            {
                return _recurring ? "Recurring" : "Special";
            }
        }
        public int InfoGuideID
        {
            get;set;
        }
        public string InfoVolID
        {
            get;set;
        }
        public string InfoFirst
        {
            get;set;
        }
        public string InfoLast
        {
            get;set;
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
    public class ShiftSummaryObject : RootObject
    {
        public int ShiftID
        { get; set; }
         public string ShiftName
        { get; set; }
        public string ShortName
        { get; set; }
        public bool Recurring
        { get; set; }
	    public DateTime ShiftDate
        { get; set; }
        public DateTime ShiftStart
        { get; set; }
        public DateTime ShiftEnd
        { get; set; }
        public int Sequence
        { get; set; }
        public string Captains
        { get; set; }
        public string Info
        { get; set; }
        public int Total
        { get
            { return BaseCnt - SubRequests + Substitutes + Dropins; }
        }
        public int BaseCnt
        {
            get;set;
        }
        public int SubRequests
        {
            get; set;
        }
        public int ShiftQuota
        {
            get;set;
        }
        public int Substitutes
        { get; set; }
        public int Dropins
        { get; set; }
      
    }
}
