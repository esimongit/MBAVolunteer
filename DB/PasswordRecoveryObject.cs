using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class PasswordRecoveryObject : RootObject
	{
#region AutoAttributes
		private Guid _id = Guid.NewGuid();
		public Guid ID 
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
        private Guid _applicationid = Guid.NewGuid();
        public Guid ApplicationID
        {
            get
            {
                return _applicationid;
            }
            set
            {
                _applicationid = value;
            }
        }
		private string _username = String.Empty;
		public string username 
		{
			get
			{
				return _username;
			}
			set
			{
				 _username = value;
			}
		}
		private DateTime _tstamp = new DateTime();
		public DateTime tstamp 
		{
			get
			{
				return _tstamp;
			}
			set
			{
				 _tstamp = value;
			}
		}
#endregion

		public PasswordRecoveryObject()
		{
			_tablename = "PasswordRecovery";
			_primarykey = "ID";
		}
        public PasswordRecoveryObject(string ApplicationName)
        {
            _tablename = "PasswordRecovery";
            _primarykey = "ID";
            PasswordRecoveryDM dm = new PasswordRecoveryDM();
            _applicationid = dm.GetApplicationId(ApplicationName);
        }
	}
}
