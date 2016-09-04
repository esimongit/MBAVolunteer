using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
    public class SiteMembershipDM : DBAccess<SiteMembershipUser>
	{
        private string _applicationname = String.Empty;
        private string _namestable = "Guides";
        public string ApplicationName
        {
            get
            {
                return _applicationname;
            }
            set
            {
                _applicationname = value;
            }
        }
        public SiteMembershipDM(string AppName)
        {
            _applicationname = AppName;
        }
        public SiteMembershipDM(string AppName, string Names)
        {
            _applicationname = AppName;
            _namestable = Names;
        }
        public override ObjectList<SiteMembershipUser> FetchAll()
        {
            return FetchAll(String.Empty);
        }

        public SiteMembershipUser FetchUser(Guid UserId)
        {
            SiteMembershipUser obj = null;
            string qry = ReadAllCommand() + " WHERE  u.UserId = @UserId ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("UserId", UserId));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }

            return obj;
        }
        public SiteMembershipUser FetchUser(string UserName)
        {
            SiteMembershipUser obj = null;
            string qry = ReadAllCommand() + " WHERE a.ApplicationName = @ApplicationName and u.UserName = @UserName ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("UserName", UserName));
                myc.Parameters.Add(new SqlParameter("ApplicationName", _applicationname));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }

            return obj;
        }

        public int FetchCount()
        {
            string qry = @" select count(*) from aspnet_Membership u
                    join aspnet_Applications a on a.ApplicationId = u.ApplicationId
                    and a.ApplicationName = @ApplicationName  ";           
            int cnt = 0;
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ApplicationName", _applicationname));
               try
               {
                   cnt = Convert.ToInt32(myc.ExecuteScalar());
               } catch {}
            }
                return cnt;
        }

        public  ObjectList<SiteMembershipUser> FetchAll(string Pattern)
        {
            if (Pattern == null)
                Pattern = String.Empty;
            ObjectList<SiteMembershipUser> Results = new ObjectList<SiteMembershipUser>();
            string qry = ReadAllCommand() + @" WHERE 1 = 1 ";
            if (@Pattern != String.Empty)
                qry += " and (U.UserName = @Pattern or n.Email = @Pattern or FirstName = @Pattern or LastName = @Pattern) ";
            qry += " order by UserName " ;
           
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                myc.Parameters.Add(new SqlParameter("Pattern", Pattern));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    SiteMembershipUser obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
        
        

        public bool MemberExists(string Email)
        {
            int cnt = 0;
            string qry = String.Format(@"Select Count(*)  
                 FROM   dbo.aspnet_Membership m join dbo.aspnet_Users u
             on    u.UserId = m.UserId  
            join {0} n on n.Email = m.Email
	         join aspnet_Applications a on a.ApplicationId = u.ApplicationId
                WHERE a.ApplicationName = @ApplicationName and m.Email = @Email", _namestable);
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ApplicationName", _applicationname));
                myc.Parameters.Add(new SqlParameter("Email", Email));
                try
                {
                    cnt = Convert.ToInt32(myc.ExecuteScalar());
                }
                catch { }
            }
            return cnt > 0;
        }
      
        protected override SiteMembershipUser LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
            SiteMembershipUser obj = new SiteMembershipUser();
            obj.NameID = GetNullableInt32(reader, "GuideID", 0);
            obj.UserId = GetNullableGuid(reader, "UserId", Guid.Empty );
            obj.First = GetNullableString(reader, "FirstName", String.Empty);
            obj.Last = GetNullableString(reader, "LastName", String.Empty);
            obj.UserName = GetNullableString(reader, "UserName", String.Empty);
            obj.Email = GetNullableString(reader, "Email", String.Empty);
            obj.CreateDate = GetNullableDateTime(reader, "CreateDate", obj.SQLMinDate);
            obj.LastLoginDate = GetNullableDateTime(reader, "LastLoginDate", obj.SQLMinDate);
            obj.IsLockedOut = GetNullableBoolean(reader, "IsLockedOut", false);
			 
            return obj;
		}

		protected override string ReadAllCommand()
		{
            return String.Format(@" 		SELECT
            n.GuideID,
            n.FirstName,
            n.LastName,
			 u.UserId,
            u.UserName, 
            m.Email,
            m.CreateDate,
            m.LastLoginDate, 
            m.IsLockedOut
             FROM   dbo.aspnet_Membership m join dbo.aspnet_Users u
             on    u.UserId = m.UserId           
	         join aspnet_Applications a  on a.ApplicationId = u.ApplicationId and a.ApplicationName = '{1}'
			  left join {0} n on n.Email = m.Email 
	         ", _namestable, _applicationname);
		}
		public int GetLast()
		{
            return 0;
		}

	}
}
