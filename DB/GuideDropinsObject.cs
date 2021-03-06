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
        private bool _onshift = false;
        public bool OnShift
        {
            get
            {
                return _onshift;
            }
            set
            {
                _onshift = value;
            }
        }
        private int _roleid = 0;
        public int RoleID
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
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
        public bool IsInfo
        {
            get; set;
        }
        public bool Recurring
        {
            get; set;
        }
        public string dtString
        {
            get
            {
                return _dropindate.ToShortDateString();
            }
        }
        public ObjectList<GuideRoleObject> Roles
        {
            get; set;
        }
        public GuideDropinsObject()
		{
			_tablename = "GuideDropins";
			_primarykey = "GuideDropinID";
		}
	}
}
