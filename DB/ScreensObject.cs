using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class ScreensObject : RootObject
	{
#region AutoAttributes
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
#endregion
        public ObjectList<ScreenGroupObject> Roles
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
		public ScreensObject()
		{
			_tablename = "Screens";
			_primarykey = "ScreenID";
		}
	}
}
