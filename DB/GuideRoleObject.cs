using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class GuideRoleObject : RootObject
	{
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
		private int _condition = 0;
		public int Condition 
		{
			get
			{
				return _condition;
			}
			set
			{
				 _condition = value;
			}
		}
#endregion
        public string RoleName
        {
            get; set;
        }
		public GuideRoleObject()
		{
			_tablename = "GuideRole";
			_primarykey = "";
		}
	}
}
