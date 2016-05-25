using System;
using System.Collections.Generic;
using System.Text;

namespace NQN.Core
{
    // Special database object that has Email and LoginID 
    // and gets registered with the Membership Class.
    public class UsersObject : RootObject
    {
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
        private string _loginid = String.Empty;
        public string LoginID
        {
            get
            {
                return _loginid;
            }
            set
            {
                _loginid = value;
            }
        }

        public UsersObject()
        {
        }
    }
}
