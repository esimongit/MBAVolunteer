using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class MailTextObjectVarsObject : RootObject
	{
#region AutoAttributes
		private int _mailtextobjectvarid = 0;
		public int MailTextObjectVarID 
		{
			get
			{
				return _mailtextobjectvarid;
			}
			set
			{
				 _mailtextobjectvarid = value;
			}
		}
		private string _objecttype = String.Empty;
		public string ObjectType 
		{
			get
			{
				return _objecttype;
			}
			set
			{
				 _objecttype = value;
			}
		}
		private string _varsymbol = String.Empty;
		public string VarSymbol 
		{
			get
			{
				return _varsymbol;
			}
			set
			{
				 _varsymbol = value;
			}
		}
		private string _vardescription = String.Empty;
		public string VarDescription 
		{
			get
			{
				return _vardescription;
			}
			set
			{
				 _vardescription = value;
			}
		}
#endregion

		public MailTextObjectVarsObject()
		{
			_tablename = "MailTextObjectVars";
			_primarykey = "MailTextObjectVarID";
		}
	}
}
