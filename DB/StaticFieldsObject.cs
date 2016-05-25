using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class StaticFieldsObject : RootObject
	{
#region AutoAttributes
        private int _staticfieldid = 0;
        public int StaticFieldID
        {
            get
            {
                return _staticfieldid;
            }
            set
            {
                _staticfieldid = value;
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
		private string _fieldname = String.Empty;
		public string FieldName 
		{
			get
			{
				return _fieldname;
			}
			set
			{
				 _fieldname = value;
			}
		}
		private string _fieldvalue = String.Empty;
		public string FieldValue 
		{
			get
			{
				return _fieldvalue;
			}
			set
			{
				 _fieldvalue = value;
			}
		}
#endregion
        private string _description = String.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

		public StaticFieldsObject()
		{
			_tablename = "StaticFields";
			_primarykey = "StaticFieldID";
		}

       

        public static string StaticValue(string name)
        {
            return StaticFieldsObject.StaticValue(name, String.Empty);
            
            
        }
        public static string StaticValue(string name, string db)
        {
            StaticFieldsDM dm = (db == String.Empty) ? new StaticFieldsDM() : new StaticFieldsDM(db);
            ObjectList<StaticFieldsObject> dList = dm.FetchValue(name);
            if (dList.Count == 0) return String.Empty;
            string ret = String.Empty;
            string sep = String.Empty;
            foreach (StaticFieldsObject obj in dList)
            {
                ret += sep + obj.FieldValue;
                sep = "<br/><br/>";
            }
            return ret;
        }
	}
    public class StaticTagsObject : RootObject
    {
        private string _fieldname = String.Empty;
		public string FieldName 
		{
			get
			{
				return _fieldname;
			}
			set
			{
				 _fieldname = value;
			}
		}
        private string _description = String.Empty;
		public string Description 
		{
			get
			{
				return _description;
			}
			set
			{
				 _description = value;
			}
		}
        public StaticTagsObject()
		{
			_tablename = "StaticTags";
			_primarykey = "FieldName";
		}
    }
}
