
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class SubOffersDM : DBAccess<SubOffersObject>
	{
        public ObjectList<SubOffersObject> FetchForShift(int ShiftID)
        {
            ObjectList<SubOffersObject> Results = new ObjectList<SubOffersObject>();
            string qry = ReadAllCommand() + " WHERE o.ShiftID = @ShiftID order by VolID  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    SubOffersObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public List<int> GuidesWithOffers()
        {
            List<int> Results = new List<int>();
            string qry = @"SELECT distinct GuideID from SubOffers";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Results.Add(GetNullableInt32(reader, "GuideID", 0));
                    }  
                }
            }
            return Results;
        }
   
		public void Save(SubOffersObject obj)
		{
			 string qry = @"INSERT INTO SubOffers (
				[ShiftID]
				,[GuideID]
				)
			VALUES(
				@ShiftID
				,@GuideID
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("ShiftID",obj.ShiftID));
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.ExecuteNonQuery();
			}
		}

        public bool AddSub(int GuideID, int ShiftID)
        {
            
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(GuideID);
            if (guide.ShiftID == ShiftID)
                return false;
            string qry = @"insert into SubOffers (GuideID, ShiftID) select @GuideID, @ShiftID where 
                    not exists (select 1 from SubOffers where ShiftID = @ShiftID and GuideID = @GuideID)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.ExecuteNonQuery();
            }
            return true;
        }
        
		public void Delete(int ShiftID, int GuideID)
		{
            string qry = "delete from SubOffers where ShiftID = @ShiftID and GuideID = @GuideID ";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
				 myc.ExecuteNonQuery();
			}
		}
        public void DeleteAllForGuide(int GuideID)
        {
            string qry = "delete from SubOffers where  GuideID = @GuideID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.ExecuteNonQuery();
            }
        }
		protected override SubOffersObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			SubOffersObject obj = new SubOffersObject();
			obj.ShiftID = GetNullableInt32(reader, "ShiftID",0);
			obj.GuideID = GetNullableInt32(reader, "GuideID",0);
            obj.GuideName = GetNullableString(reader, "GuideName", String.Empty);
            obj.Email = GetNullableString(reader, "Email", String.Empty);
            obj.Phone = GetNullableString(reader, "Phone", String.Empty);
            obj.VolID = GetNullableString(reader, "VolID", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.HomeShift = GetNullableString(reader, "HomeShift", String.Empty);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				o.[ShiftID]
				,o.[GuideID]
                ,GuideName =  case  isnull(g.PreferredName, '') WHEN '' then g.FirstName ELSE g.PreferredName END + ' ' + g.LastName
                ,g.Email
                ,g.Phone
                ,g.VolID
                ,HomeShift=s.ShortName
                ,s.Sequence
                ,r.MaskContactInfo
				FROM SubOffers o join (Guides g join GuideShift gs on g.GuideID = gs.GuideID and gs.IsPrimary = 1) on o.GuideID = g.GuideID join Shifts s on gs.ShiftID = s.ShiftID 
                 join Roles r on r.RoleID = g.RoleID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('SubOffers')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
