using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class GuidesObject : RootObject
	{
        //public const string ValidEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public const string ValidEmail = @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,22}|[0-9]{1,3})(\]?)$";
        public static string ValidPhone = @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";
#region AutoAttributes
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
		private string _firstname = String.Empty;
		public string FirstName 
		{
			get
			{
				return _firstname;
			}
			set
			{
				 _firstname = value;
			}
		}
		private string _lastname = String.Empty;
		public string LastName 
		{
			get
			{
				return _lastname;
			}
			set
			{
				 _lastname = value;
			}
		}
		private string _phone = String.Empty;
		public string Phone 
		{
			get
			{
				return _phone;
			}
			set
			{
				 _phone = value;
			}
		}
        private string _cell = String.Empty;
        public string Cell
        {
            get
            {
                return _cell;
            }
            set
            {
                _cell = value;
            }
        }
        private bool _cellpreferred = false;
        public bool CellPreferred
        {
            get
            {
                return _cellpreferred;
            }
            set
            {
                _cellpreferred = value;
            }
        }
        private string _email = String.Empty;
		public string Email 
		{
			get
			{
				return _email;
			}
			set
			{
				 _email = value;
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
		private bool _inactive = false;
		public bool Inactive 
		{
			get
			{
				return _inactive;
			}
			set
			{
				 _inactive = value;
			}
		}
        private bool _maskpersonalinfo = false;
        public bool MaskPersonalInfo
        {
            get
            {
                return _maskpersonalinfo;
            }
            set
            {
                _maskpersonalinfo = value;
            }
        }
        private string _phonedigits = String.Empty;
		public string PhoneDigits 
		{
			get
			{
				return _phonedigits;
			}
			set
			{
				 _phonedigits = value;
			}
		}
		private string _searchkey = String.Empty;
		public string SearchKey 
		{
			get
			{
				return _searchkey;
			}
			set
			{
				 _searchkey = value;
			}
		}
		private string _notes = String.Empty;
		public string Notes 
		{
			get
			{
				return _notes;
			}
			set
			{
				 _notes = value;
			}
		}
		private string _volid = String.Empty;
		public string VolID 
		{
			get
			{
				return _volid;
			}
			set
			{
				 _volid = value;
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
        
 
		private string _updateby = String.Empty;
		public string UpdateBy 
		{
			get
			{
				return _updateby;
			}
			set
			{
				 _updateby = value;
			}
		}
		private DateTime _lastupdate = new DateTime();
		public DateTime LastUpdate 
		{
			get
			{
				return _lastupdate;
			}
			set
			{
				 _lastupdate = value;
			}
		} 
        private string _calendartype = String.Empty;
        public string CalendarType
        {
            get
            {
                return _calendartype;
            }
            set
            {
                _calendartype = value;
            }
        }
        private bool _notifysubrequests = false;
        public bool NotifySubRequests
        {
            get
            {
                return _notifysubrequests;
            }
            set
            {
                _notifysubrequests = value;
            }
        }
#endregion

        public string GuideName
        {
            get;
            set;

        }
        public string RoleName
        {
            get;
            set;
        }
        public string AltRoleName
        {
            get;
            set;
        }
        public ObjectList<GuideRoleObject> Roles
        {
            get; set;
        }
        public string ShiftName
        {
            get;
            set;
        }
        public string ShortName
        {
            get;
            set;
        }
        public int Sequence
        {
            get;
            set;
        }
        public int DOW
        {
            get;
            set;
        }
        public int AddShift
        {
            get; set;
        }
        public int AddRole
        {
            get; set;
        }
        public bool HasSub
        {
            get;
            set;
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
        public string Sub
        {
            get;
            set;
        }
        public bool SubRequested
        {
            get;
            set;
        }
        public string SubDescription
        {
            get;
            set;
        }
        public bool HasLogin
        {
            get;
            set;
        }
        public bool NeedsLogin
        {
            get
            {
                return !HasLogin;
            }
             
        }
        public Guid UserId
        {
            get;
            set;
        }
        public DateTime dt
        {
            get;
            set;
        }
        private ObjectList<ShiftsObject> _shifts = new ObjectList<ShiftsObject>();
        public ObjectList<ShiftsObject> Shifts
        {
            get
            {
                return _shifts;
            }
            set
            {
                _irregularshift = false;
                _shifts = value;
                if (_shifts.Count == 0)
                    _irregularshift = true;
                else if (_shifts.Count == 1)
                {
                    ShiftsObject pshift = Shifts[0];
                    _shiftid = pshift.ShiftID;
                    ShiftName = pshift.ShiftName;
                    ShortName = pshift.ShortName;
                    Sequence = pshift.Sequence;
                    DOW = pshift.DOW;
                    if (DOW == 0)
                        _irregularshift = true;
                }
                else
                {
                    string sep = String.Empty;
                    foreach (ShiftsObject sobj in _shifts)
                    {
                        ShiftName += sep + sobj.ShiftName;
                        ShortName += sep + sobj.ShortName;
                        sep = ", ";
                    }
                }
            }
        }
        private bool _irregularshift = false;
        public bool IrregularShift
        {
            get
            {
                return _irregularshift;
            }
            
        }
        public string EmailLink
        {
            get
            {
                if (_email == String.Empty)
                    return String.Empty;
                return String.Format("<a href='mailto:{0}'><b>{0}</b></a>", _email);
            }
        }
       
		public GuidesObject()
		{
			_tablename = "Guides";
			_primarykey = "GuideID";
		}
        public GuidesObject(SubOffersObject obj)
        {
            _tablename = "Guides";
            _primarykey = "GuideID";
            _guideid = obj.GuideID;
            _shiftid = obj.ShiftID;
            _email = obj.Email;
        }
        public bool HasDate(DateTime dt)
        {
            
            ShiftsDM dm = new ShiftsDM();
            bool ret = false;
            foreach (ShiftsObject shift in Shifts)
            {
                if (dm.IsShiftOnDate(shift.ShiftID, dt))
                    ret = true;
            }
            return ret;
        }

        public bool HasRole(string Role)
        {

            bool ret = false;
            if (RoleName == Role)
                return true;
            foreach (GuideRoleObject obj in Roles)
            {
                if (obj.RoleName == Role)
                    ret = true;
            }
            return ret;
        }

        public static string DefaultAreaCode = "831";
        private int _id = 0;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public static string DigitsOnly(string Phone)
        {
            if (Phone == null || Phone.Length == 0) return String.Empty;
            string newphone = String.Empty;
            for (int i = 0; i < Phone.Length; i++)
            {
                if (Char.IsDigit(Phone[i]))
                    newphone += Phone[i];
            }
            if (newphone.Length > 10)
            {
                while (newphone[0] == '1')
                    newphone = newphone.Substring(1, 10);
            }
            if (newphone.Length > 10)
                newphone = newphone.Substring(0, 10);
            return newphone;
        }
        public static string Standardize(string phone)
        {

            string ret = String.Empty;
            if (phone == null || phone == String.Empty)
                return ret;
            if (phone.Contains(","))
            {
                string[] phones = phone.Split(new char[] { ',' });
                phone = phones[0];
            }
            string digits = GuidesObject.DigitsOnly(phone);
            if (digits == null)
                return ret;
            if (digits.Length == 10)
            {
                ret = digits.Substring(0, 3) + "-" + digits.Substring(3, 3) +
                    "-" + digits.Substring(6, 4);
            }
            else if (digits.Length == 7)
            {
                ret = GuidesObject.DefaultAreaCode + "-" + digits.Substring(0, 3) +
                    "-" + digits.Substring(3, 4);
            }
            else ret = phone;
            return ret;
        }
	}
}
