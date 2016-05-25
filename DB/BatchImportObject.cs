using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;
using FileHelpers;

namespace NQN.DB
{
    public enum ImportStatusValues { Imported = 0, MatchFound = 1, Conflict = 2, NewRecord = 3, UnknownRole = 4,UnknownShift=5 }
    public class BatchImportObject : RootObject
    {
        
        #region AutoAttributes
        private int _importid = 0;
        public int ImportID
        {
            get
            {
                return _importid;
            }
            set
            {
                _importid = value;
            }
        }
        private string _id = String.Empty;
        public string ID
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
        private string _last = String.Empty;
        public string Last
        {
            get
            {
                return _last;
            }
            set
            {
                _last = value;
            }
        }
        private string _first = String.Empty;
        public string First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
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
        private string _shift = String.Empty;
        public string Shift
        {
            get
            {
                return _shift;
            }
            set
            {
                _shift = value;
            }
        }
        private string _role = String.Empty;
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        private int _importstatus = 0;
        public int ImportStatus
        {
            get
            {
                return _importstatus;
            }
            set
            {
                _importstatus = value;
            }
        }
        #endregion


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
        public string RecordStatus
        {
            get
            {
                return ((ImportStatusValues)_importstatus).ToString();
            }
        }
        public string Color
        {
            get
            {
                string ret = String.Empty;
                switch (_importstatus)
                {
                    case (int)ImportStatusValues.Imported:
                        ret = "background-color:silver";
                        break;
                    case (int)ImportStatusValues.Conflict:
                        ret = "background-color:yellow";
                        break;
                    case (int)ImportStatusValues.MatchFound:
                        ret = "background-color:#eef";
                        break;
                    case (int)ImportStatusValues.NewRecord:
                        ret = "background-color:pink";
                        break;
                }
                return ret;
            }
        }
        public BatchImportObject()
        {
            _tablename = "BatchImport";
            _primarykey = "ImportID";
        }
        public BatchImportObject(BatchImportCSV obj)
        {
            _tablename = "BatchImport";
            _primarykey = "ImportID";
            _id = obj.ID;
            _first = obj.First;
            _last = obj.Last;
            _email = obj.Email;
            _phone = obj.Phone;
            _shift = obj.Shift;
            _role = obj.Role;

            //if (obj.OptValue != String.Empty)
            //{
            //    int ival = 0;
            //    try 
            //    {
            //        ival = Convert.ToInt32(obj.OptValue);
            //    } catch {}
            //    if (ival != 0)
            //        if (ival > 0)
            //            _intvalue = ival;
            //    else
            //        _stringvalue = obj.OptValue;
            //}
        }
    }

    [DelimitedRecord(",")]
    public class BatchImportCSV
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string ID;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string First;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Last;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Email;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Phone;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Shift;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Role;



        // [FieldOptional()]
        // [FieldNullValue("-1")]
        // [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        //public string OptValue;


    }
}
