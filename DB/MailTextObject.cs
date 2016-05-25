using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using NQN.Core;

namespace NQN.DB 
{
	public class MailTextObject : RootObject
	{
#region AutoAttributes
		private int _mailtextid = 0;
		public int MailTextID 
		{
			get
			{
				return _mailtextid;
			}
			set
			{
				 _mailtextid = value;
			}
		}
		private string _symbol = String.Empty;
		public string Symbol 
		{
			get
			{
				return _symbol;
			}
			set
			{
				 _symbol = value;
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
        private string _subject = String.Empty;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }
        private string _mailfrom = String.Empty;
        public string MailFrom
        {
            get
            {
                return _mailfrom;
            }
            set
            {
                _mailfrom = value;
            }
        }
		private string _textvalue = String.Empty;
        public string TextValue 
		{
			get
			{
                return _textvalue;
			}
			set
			{
                _textvalue = value;
			}
		}
        private bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        private bool _ishtml = false;
        public bool IsHtml
        {
            get { return _ishtml; }
            set { _ishtml = value; }
        }
        private string _objecttype = String.Empty;
        public string ObjectType
        {
            get { return _objecttype; }
            set { _objecttype = value; }
        }
        private int _calendarid = 0;
        public int CalendarID
        {
            get
            {
                return _calendarid;
            }
            set
            {
                _calendarid = value;
            }
        }
#endregion

		public MailTextObject()
		{
			_tablename = "MailText";
			_primarykey = "MailTextID";
		}

        public string CompletedText( object Context)
        {
            string txt = _textvalue;
            PropertyInfo[] srcinfo = Context.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo prop in srcinfo)
            {               
                if (prop.GetValue(Context, null) != null)
                    txt = txt.Replace("[" + prop.Name + "]", prop.GetValue(Context, null).ToString());               
            }
            return txt;
        }
	}
}
