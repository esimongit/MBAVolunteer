using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class GuideDropinsObject : RootObject
	{
#region AutoAttributes
		private int _guidedropinid = 0;
		public int GuideDropinID 
		{
			get
			{
				return _guidedropinid;
			}
			set
			{
				 _guidedropinid = value;
			}
		}
		private int _guideid = 0;
		public int GuideID 
		{
			get
			{
				return _guideid;
			}
			set
			{
				 _guideid = value;
			}
		}
		private DateTime _dropindate = new DateTime();
		public DateTime DropinDate 
		{
			get
			{
				return _dropindate;
			}
			set
			{
				 _dropindate = value;
			}
		}
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
#endregion
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string VolID
        {
            get;
            set;
        }
        public int Sequence
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public string ShiftName
        {
            get;
            set;
        }
        public string GuideName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public bool Selected
        {
            get;
            set;
        }
        public string Email
        {
            get;set;
        }
        public string Phone
        {
            get; set;
        }
        public bool MaskContactInfo
        {
            get;
            set;
        }
        public bool ShowContactInfo
        {
            get
            {
                return !MaskContactInfo;
            }
        }
        public GuideDropinsObject()
		{
			_tablename = "GuideDropins";
			_primarykey = "GuideDropinID";
		}
	}
}
