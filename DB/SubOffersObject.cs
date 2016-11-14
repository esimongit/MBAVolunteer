using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
    public class SubOffersObject : RootObject
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
        #endregion
        public string GuideName
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
        public string VolID
        {
            get;
            set;
        }
        public string HomeShift
        {
            get;
            set;
        }
        public string RequestorName
        {
            get;
            set;
        }
        public string RequestorID
        {
            get;
            set;
        }
        public string RequestorEmail
        {
            get;
            set;
        }
        public string RequestorPhone
        {
            get;
            set;
        }
        public DateTime dt
        {
            get;
            set;
        }
        public string ShiftName
        {
            get;
            set;
        }
       
        public string ShiftLink
        {
            get
            {
                return String.Format("{0}/SubRequest.aspx?dt={0}", StaticFieldsObject.StaticValue("GuideURL"), dt.ToShortDateString());
            }
        }
        public int Sequence
        {
            get;
            set;
        }
        public bool MaskContactInfo
        {
            get; set;
        }
        public bool NotifySubRequests
        {
            get; set;
        }
        public bool ShowContactInfo
        {
            get
            {
                return !MaskContactInfo;
            }
        }
        public string dtString
        {
            get
            {
                if (dt == null)
                    return String.Empty;
                return dt.ToShortDateString();
            }
        }
        public SubOffersObject()
        {
            _tablename = "SubOffers";
            _primarykey = "";
        }
    }
}
