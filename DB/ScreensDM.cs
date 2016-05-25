
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class ScreensDM : DBAccess<ScreensObject>
	{
        public ObjectList<ScreensObject> FetchForPrivs()
        {
            ObjectList<ScreensObject> Results = new ObjectList<ScreensObject>();
            string qry = ReadAllCommand() + @"  
                order by [Text]  ";

            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    ScreensObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }
		public void Update(ScreensObject obj)
		{
			 string qry = @"UPDATE  Screens SET 
				ScreenName=@ScreenName
				,Text=@Text
				,ToolTip=@ToolTip
				 WHERE ScreenID = @ScreenID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ScreenID",obj.ScreenID));
				myc.Parameters.Add(new SqlParameter("ScreenName",obj.ScreenName));
				myc.Parameters.Add(new SqlParameter("Text",obj.Text));
				myc.Parameters.Add(new SqlParameter("ToolTip",obj.ToolTip));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(ScreensObject obj)
		{
			 string qry = @"INSERT INTO Screens (
				[ScreenName]
				,[Text]
				,[ToolTip]
				)
			VALUES(
				@ScreenName
				,@Text
				,@ToolTip
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ScreenName",obj.ScreenName));
				myc.Parameters.Add(new SqlParameter("Text",obj.Text));
				myc.Parameters.Add(new SqlParameter("ToolTip",obj.ToolTip));
				myc.ExecuteNonQuery();
			}
		}

		public void Delete(int pkey)
		{
			string qry = @"DELETE FROM Screens WHERE [ScreenID] = @ScreenID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ScreenID",pkey));
				 myc.ExecuteNonQuery();
			}
		}

		protected override ScreensObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			ScreensObject obj = new ScreensObject();
			obj.ScreenID = GetNullableInt32(reader, "ScreenID",0);
			obj.ScreenName = GetNullableString(reader, "ScreenName",String.Empty);
			obj.Text = GetNullableString(reader, "Text",String.Empty);
			obj.ToolTip = GetNullableString(reader, "ToolTip",String.Empty);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				[ScreenID]
				,[ScreenName]
				,[Text]
				,[ToolTip]
				FROM Screens ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('Screens')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
