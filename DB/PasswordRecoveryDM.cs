
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class PasswordRecoveryDM : DBAccess<PasswordRecoveryObject>
	{
        public PasswordRecoveryObject FetchRecord(Guid prkey)
        {
            PasswordRecoveryObject obj = null;
            string qry = ReadAllCommand() + " WHERE ID = @prkey ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("prkey", prkey));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public PasswordRecoveryObject FetchUser(string username, string ApplicationName )
        {
            PasswordRecoveryObject obj = null;
            string qry = ReadAllCommand() + " WHERE username = @username and ApplicationID = (select ApplicationId from aspnet_Applications where ApplicationName = @ApplicationName)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("username", username));
                myc.Parameters.Add(new SqlParameter("ApplicationName", ApplicationName));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public Guid GetApplicationId(string ApplicationName)
        {
            Guid ApplicationId = Guid.NewGuid();
            string qry = "SELECT ApplicationId FROM aspnet_Applications WHERE ApplicationName=@ApplicationName";
             using (SqlConnection conn = ConnectionFactory.getNew())
            {
             SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ApplicationName", ApplicationName));

                SqlDataReader reader = myc.ExecuteReader();
                if (reader.Read())
                    ApplicationId = GetNullableGuid(reader, "ApplicationID", Guid.NewGuid());
            }
            return ApplicationId;
        }

       
		public void Update(PasswordRecoveryObject obj)
		{
			 string qry = @"UPDATE  PasswordRecovery SET 
				username=@username
                ,ApplicationId = @ApplicationID
				,tstamp=@tstamp
				 WHERE ID = @ID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ID",obj.ID));
				myc.Parameters.Add(new SqlParameter("username",obj.username));
                myc.Parameters.Add(new SqlParameter("ApplicationID", obj.ApplicationID));
				myc.Parameters.Add(new SqlParameter("tstamp",obj.tstamp));
				myc.ExecuteNonQuery();
			}
		}

		public void Save( PasswordRecoveryObject obj)
		{
			 string qry = @"DELETE FROM PasswordRecovery WHERE username = @username;
                INSERT INTO PasswordRecovery (
                [ID]
				,[username]
                ,[ApplicationID]
				,[tstamp]
				)
			VALUES(
                @ID
				,@username
                ,@ApplicationID
				,@tstamp
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ID", obj.ID));
                myc.Parameters.Add(new SqlParameter("username", obj.username));
                myc.Parameters.Add(new SqlParameter("ApplicationID", obj.ApplicationID));
				myc.Parameters.Add(new SqlParameter("tstamp",obj.tstamp));
				myc.ExecuteNonQuery();
			}
             
		}

		public void Delete(Guid pkey)
		{
			string qry = @"DELETE FROM PasswordRecovery WHERE [ID] = @ID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override PasswordRecoveryObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			PasswordRecoveryObject obj = new PasswordRecoveryObject();
			obj.ID = GetNullableGuid(reader, "ID", Guid.NewGuid());
            obj.ApplicationID = GetNullableGuid(reader, "ApplicationID", Guid.NewGuid());
			obj.username = GetNullableString(reader, "username",String.Empty);
			obj.tstamp = GetNullableDateTime(reader, "tstamp",new DateTime());
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[ID]
				,[username]
                ,[ApplicationID]
				,[tstamp]
				FROM PasswordRecovery ";
		}
		public int GetLast()
		{
            return 0;
		}

	}
}
