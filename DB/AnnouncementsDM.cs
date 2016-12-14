
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class AnnouncementsDM : DBAccess<AnnouncementsObject>
	{
        public AnnouncementsObject FetchMain()
        {
            AnnouncementsObject obj = null;
            string qry = ReadAllCommand() + " WHERE ShiftID is null";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
		public void Update(AnnouncementsObject obj)
		{
			 string qry = @"UPDATE  Announcements SET 
				ShiftID=nullif(@ShiftID,0)
				,AnnouncementText=@AnnouncementText
				 WHERE AnnouncementID = @AnnouncementID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("AnnouncementID",obj.AnnouncementID));
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.Parameters.Add(new SqlParameter("AnnouncementText",obj.AnnouncementText));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(AnnouncementsObject obj)
		{
			 string qry = @"INSERT INTO Announcements (
				[ShiftID]
				,[AnnouncementText]
				)
			VALUES(
				nullif(@ShiftID,0)
				,@AnnouncementText
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.Parameters.Add(new SqlParameter("AnnouncementText",obj.AnnouncementText));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM Announcements WHERE [AnnouncementID] = @AnnouncementID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("AnnouncementID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override AnnouncementsObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			AnnouncementsObject obj = new AnnouncementsObject();
			obj.AnnouncementID = GetNullableInt32(reader, "AnnouncementID",0);
			obj.ShiftID = GetNullableInt32(reader, "ShiftID",0);
			obj.AnnouncementText = GetNullableString(reader, "AnnouncementText",String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[AnnouncementID]
				,[ShiftID]
				,[AnnouncementText]
				FROM Announcements ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('Announcements')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
