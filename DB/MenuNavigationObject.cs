using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class MenuNavigationObject : RootObject
	{
#region AutoAttributes
		private int _menuid = 0;
		public int MenuID 
		{
			get
			{
				return _menuid;
			}
			set
			{
				 _menuid = value;
			}
		}
		private int _screenid = 0;
		public int ScreenID 
		{
			get
			{
				return _screenid;
			}
			set
			{
				 _screenid = value;
			}
		}
		private int _parentid = 0;
		public int ParentID 
		{
			get
			{
				return _parentid;
			}
			set
			{
				 _parentid = value;
			}
		}
		private bool _selectable = false;
		public bool Selectable 
		{
			get
			{
				return _selectable;
			}
			set
			{
				 _selectable = value;
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
		private bool _enabled = false;
		public bool Enabled 
		{
			get
			{
				return _enabled;
			}
			set
			{
				 _enabled = value;
			}
		}
#endregion
        private string _screenname = String.Empty;
        public string ScreenName
        {
            get
            {
                return _screenname;
            }
            set
            {
                _screenname = value;
            }
        }
        private string _text = String.Empty;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        private string _tooltip = String.Empty;
        public string ToolTip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                _tooltip = value;
            }
        }
        private string _menu = String.Empty;
        public string Menu
        {
            get
            {
                return _menu;
            }
            set
            {
                _menu = value;
            }
        }
        public int NavID
        {
            get
            {
                return _screenid;
            }
        }
       
       
		public MenuNavigationObject()
		{
			_tablename = "MenuNavigation";
			_primarykey = "";
		}
	}
}
