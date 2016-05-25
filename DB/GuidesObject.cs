using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class GuidesObject : RootObject
	{
        public static string ValidEmail = @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
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
        private int _altroleid = 0;
        public int AltRoleID
        {
            get
            {
                return _altroleid;
            }
            set
            {
                _altroleid = value;
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
		private string _preferredname = String.Empty;
		public string PreferredName 
		{
			get
			{
				return _preferredname;
			}
			set
			{
				 _preferredname = value;
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
        public bool HasSub
        {
            get;
            set;
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
            return dm.IsShiftOnDate(_shiftid, dt);
        }

        public static string DefaultAreaCode = "813";
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
            if (Phone == null || Phone.Length == 0) return null;
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
