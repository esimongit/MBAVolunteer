using System;
using System.Data;
using System.Collections.Generic;
 
using NQN.Core;

namespace NQN.DB 
{
	public class GuideSubstituteObject : RootObject
	{
#region AutoAttributes
		private int _guidesubstituteid = 0;
		public int GuideSubstituteID 
		{
			get
			{
				return _guidesubstituteid;
			}
			set
			{
				 _guidesubstituteid = value;
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
		private DateTime _subdate = new DateTime();
		public DateTime SubDate 
		{
			get
			{
				return _subdate;
			}
			set
			{
				 _subdate = value;
			}
		}
		private int _substituteid = 0;
		public int SubstituteID
        {
            get
            {
                return _substituteid;
            }
            set
            {
                _substituteid = value;
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
        private DateTime _dateentered = new DateTime();
		public DateTime DateEntered 
		{
			get
			{
				return _dateentered;
			}
			set
			{
				 _dateentered = value;
			}
		}
#endregion
        public string dtString
        {
            get
            {    
                return _subdate.ToShortDateString();
            }
        }
        public string ShiftLink
        {
            get
            {
                return String.Format("{0}/SubRequest.aspx?dt={1}", StaticFieldsObject.StaticValue("GuideURL"),dtString);
            }
        }
        public string ShiftName
        {
            get; set;
        }
        public string ShortName
        {
            get; set;
        }
        public int Sequence
        {
            get; set;
        }
        public string FirstName 
        {
            get; set;
        }
        public string LastName
        {
            get;set;
        }
		public string VolID
        {
            get; set;
        }
        public string Role 
        {
            get; set;
        }
        public string Sub
        {
            get; set;
        }
         
        public string SubFirst
        {
            get; set;
        }
        public string SubLast
        {
            get; set;
        }
        public string SubRole
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string SubEmail
        {
            get;
            set;
        }
        public string SubPhone
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
        public string SubName
        {
            get
            {
                if (SubLast == null || SubLast == String.Empty)
                    return "Sub Needed";
                return SubFirst + " " + SubLast;
            }
        }
        public System.Drawing.Color SubColor
        {
            get
            {
                return System.Drawing.ColorTranslator.FromHtml((SubLast == null || SubLast == String.Empty) ? "#ff0000" : "#000000");
            }
        }
        public bool HasSub
        {
            get;
            set;

        }
        public bool NoSub
        {
            get;
            set;
        }
        public bool CanSub
        {
            get;
            set;
        }
        public bool IsSub
        {
            get;
            set;
        }
        public bool SubOffer
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
        public string CalendarType
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
        public int Attendance
        {
            get; set;
        }
        public GuideSubstituteObject()
		{
			_tablename = "GuideSubstitute";
			_primarykey = "GuideSubstituteID";
		}

        private bool _isinfo = false;
        public bool IsInfo
        {
            get
            {
                return _isinfo;
            }
            set
            {
                _isinfo = value;
            }
        }

       
        public GuideSubstituteObject(GuideDropinsObject obj)
        {
            _tablename = "GuideSubstitute";
            _primarykey = "GuideSubstituteID";
            _shiftid = obj.ShiftID;
            _substituteid = obj.GuideID;
            Sequence = obj.Sequence;
            _subdate = obj.DropinDate;
            SubFirst = obj.FirstName;
            SubLast = obj.LastName;
            ShiftName = obj.ShiftName;
            Role = obj.Role;
            SubRole = Role;
            Sub = obj.VolID;
            VolID = obj.VolID;
            _guideid = obj.GuideID;
            FirstName = "Drop";
            LastName = "In";
            GuideSubstituteID = -obj.GuideDropinID;
        }
     
        public bool Critical
        {
            get; set;
        }
    }
    public class CalendarDateObject : RootObject
    {
        public DateTime Dt
        {
            get;
            set;
        }
        private bool _hassubstitutes = false;
        public bool HasSubstitutes
        {
            get
            {
                return _hassubstitutes;
            }
            set
            {
                _hassubstitutes = value;
            }
        }
        private bool _needsubstitutes = false;
        public bool NeedSubstitutes
        {
            get
            {
                return _needsubstitutes;
            }
            set
            {
                _needsubstitutes = value;
            }
        }
        private bool _issubstitute = false;
        public bool IsSubstitute
        {
            get
            {
                return _issubstitute;
            }
            set
            {
                _issubstitute = value;
            }
        }
      
        public bool Critical
        {
            get; set;
        }
    }
}
