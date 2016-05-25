using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class NavigationMenusObject : RootObject
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
#endregion

		public NavigationMenusObject()
		{
			_tablename = "NavigationMenus";
			_primarykey = "MenuID";
		}
	}
}
