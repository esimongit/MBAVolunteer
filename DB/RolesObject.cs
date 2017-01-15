using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class RolesObject : RootObject
	{
#region AutoAttributes
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
		private bool _iscaptain = false;
		public bool IsCaptain 
		{
			get
			{
				return _iscaptain;
			}
			set
			{
				 _iscaptain = value;
			}
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
        private bool _maskcontactinfo = false;
        public bool MaskContactInfo
        {
            get
            {
                return _maskcontactinfo;
            }
            set
            {
                _maskcontactinfo = value;
            }
        }
        #endregion

        public int Number
        {
            get;
            set;
        }
		public RolesObject()
		{
			_tablename = "Roles";
			_primarykey = "RoleID";
		}
	}
}
