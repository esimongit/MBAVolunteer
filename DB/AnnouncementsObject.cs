using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{
	public class AnnouncementsObject : RootObject
	{
#region AutoAttributes
		private int _announcementid = 0;
		public int AnnouncementID 
		{
			get
			{
				return _announcementid;
			}
			set
			{
				 _announcementid = value;
			}
		}
		private int _shiftid = 0;
		public int ShiftID 
		{
			get
			{
				return _shiftid;
			}
			set
			{
				 _shiftid = value;
			}
		}
		private string _announcementtext = String.Empty;
		public string AnnouncementText 
		{
			get
			{
				return _announcementtext;
			}
			set
			{
				 _announcementtext = value;
			}
		}
#endregion

        
		public AnnouncementsObject()
		{
			_tablename = "Announcements";
			_primarykey = "AnnouncementID";
		}
	}
}
