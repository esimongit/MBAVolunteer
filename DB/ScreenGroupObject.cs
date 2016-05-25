using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class ScreenGroupObject : RootObject
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
        private Guid _roleid = Guid.Empty;
        public Guid RoleID
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
		
#endregion
        private string _rolename = String.Empty;
        public string RoleName
        {
            get
            {
                return _rolename;
            }
            set
            {
                _rolename = value;
            }
        }
        private string _screenhandle = String.Empty;
        public string ScreenHandle
        {
            get
            {
                return _screenhandle;
            }
            set
            {
                _screenhandle = value;
            }
        }
        public bool Checked
        {
            get
            {
                return _screenhandle != String.Empty;
            }
        }
		public ScreenGroupObject()
		{
			_tablename = "ScreenGroup";
			_primarykey = "";
		}
	}
    public class StaffRolesObject
    {
        public Guid RoleID
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }

    }
}
