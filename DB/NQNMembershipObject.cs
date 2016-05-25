using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

using NQN.Core;

namespace NQN.DB
{
    public class NQNMembershipObject  
    {
        
        public string VolAccessUrl
        {
            get;
            set;
        }
        private string _username = String.Empty;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _email = String.Empty;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _password = String.Empty;
        public string Password
        {
            get { return  _password == String.Empty ? "(Unchanged)" : _password; }
            set { _password = value;}
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        private Guid _prkey = Guid.NewGuid();
        public Guid PRKey
        {
            get
            {
                return _prkey;
            }
            set
            {
                _prkey = value;
            }

        }
        public string Link
        {
            get
            {
                return String.Format("{0}/Recovery/default.aspx?key={1}", VolAccessUrl, _prkey);
            }
        }
        public NQNMembershipObject(string email)
        {
            this._username = email;
            this._email = email;           
            this.FindName();
        }
        public NQNMembershipObject(string email, string password)
        {
            this._email = email;
            this._password = password;
            this.FindName();
        }
        public NQNMembershipObject(string username, string email, string password)
        {
            this._username = username;
            this._email = email;
            this._password = password;
            this.FindName();
        }
        protected void FindName()
        {
            GuidesDM dm = new GuidesDM();
            GuidesObject name = dm.FetchByEmail(_email);
            if (name != null)
            {
                FirstName = name.FirstName;
                LastName = name.LastName;
            }
        }
    }
    public class SiteMembershipUser : RootObject
    {
        private MembershipUser _muser = null;
        public MembershipUser MUser
        {
            get { return _muser; }
            set { _muser = value; }
        }
        private string _addrole = String.Empty;
        public string AddRole
        {
            get { return _addrole; }
            set { _addrole = value; }
        }
        public string UniqueID
        {
            get
            {
                return _userid.ToString() + _nameid.ToString();
            }
        }

        private Guid _userid = Guid.Empty;
        public Guid UserId
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }
        private string _first = String.Empty;
        public string First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }
        private string _last = String.Empty;
        public string Last
        {
            get
            {

                return _last;
            }
            set
            {
                _last = value;
            }
        }
        private string _email = String.Empty;
        public string Email
        {
            get
            {
                if (_muser != null)
                    return _muser.Email;
                return _email;
            }
            set
            {
                _email = value;
            }
        }
       
        public string MailTo
        {
            get
            {
                if (Email == String.Empty)
                    return String.Empty;
                else return "<a href=\"mailto:" + Email + "\">" + Email + "</a>";
            }
        }
        private string _username = String.Empty;
        public string UserName
        {
            get
            {
                if (_muser != null)
                     return _muser.UserName;                    
                else
                    return _username;
            }
            set { _username = value; }
        }
        private int _nameid = 0;
        public int NameID
        {
            get { return _nameid; }
            set { _nameid = value; }
        }
        private DateTime _createdate = DateTime.MinValue;
        public DateTime CreateDate
        {
            get
            {
                if (_muser != null)
                    return _muser.CreationDate;
                else
                    return _createdate;
            }
            set
            {
                _lastlogindate = value;
            }
        }
        private DateTime _lastlogindate = DateTime.MinValue;
        public DateTime LastLoginDate
        {
            get
            {
                if (_muser != null)
                    return _muser.LastLoginDate;
                else
                    return _lastlogindate;
            }
            set
            {
                _lastlogindate = value;
            }
        }
        private bool _isonline = false;
        public bool IsOnline
        {
            get
            {
                if (_muser != null)
                    return _muser.IsOnline;
                else
                    return _isonline;
            }
            set
            {
                _isonline = value;
            }
        }
        private bool _islockedout = false;

        public bool IsLockedOut
        {
            get 
            { 
                if ( _muser != null)
                    return _muser.IsLockedOut; 
                else
                return _islockedout;
            }
            set
            {
                _islockedout = value;
            }
        }
        public string[] UserRoles
        {
            get
            {
                return Roles.GetRolesForUser(UserName);
            }
        }
         public SiteMembershipUser()
        {
             _tablename = "aspnet_Users";
			_primarykey = "UniqueID";
         }
        public SiteMembershipUser(MembershipUser u)
        {
            _muser = u;

        }
        
    }
}
