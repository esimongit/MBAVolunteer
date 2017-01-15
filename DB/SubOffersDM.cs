
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{
	public class SubOffersDM : DBAccess<SubOffersObject>
	{
        public ObjectList<SubOffersObject> FetchForShift(int ShiftID, bool IsCaptain)
        {
            ObjectList<SubOffersObject> Results = new ObjectList<SubOffersObject>();
            

            string qry = ReadAllCommand() + " WHERE o.ShiftID = @ShiftID order by g.FirstName  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    SubOffersObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        if (IsCaptain)
                            obj.MaskContactInfo = false;
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public ObjectList<SubOffersObject> FetchForGuide(int GuideID)
        {
            ObjectList<SubOffersObject> Results = new ObjectList<SubOffersObject>();
            string qry = ReadAllCommand() + " WHERE o.GuideID = @GuideID  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
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
        
		public void Delete(int GuideID, int ShiftID)
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
            obj.FirstName = GetNullableString(reader, "FirstName", String.Empty);
            obj.LastName = GetNullableString(reader, "LastName", String.Empty);
            obj.Email = GetNullableString(reader, "Email", String.Empty);
            obj.Phone = GetNullableString(reader, "Phone", String.Empty);         
            obj.VolID = GetNullableString(reader, "VolID", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.HomeShift = GetNullableString(reader, "HomeShift", String.Empty);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            obj.NotifySubRequests = GetNullableBoolean(reader, "NotifySubRequests", false);
            obj.HasInfoDesk = GetNullableBoolean(reader, "HasInfoDesk", false);
            obj.DateEntered = GetNullableDateTime(reader, "DateEntered", DateTime.Today);
            return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				o.[ShiftID]
				,o.[GuideID]
                ,o.[DateEntered]
                ,g.FirstName
                ,g.LastName
                ,g.Email
                 ,Phone = case g.CellPreferred WHEN 1 THEN g.Cell ELSE g.Phone END
                ,g.VolID
                ,g.NotifySubRequests
                ,HomeShift=dbo.FlattenShortShifts(o.GuideID)
                ,s.Sequence
                ,MaskContactInfo = r.[MaskContactInfo] | isnull(g.MaskPersonalInfo,0)
                ,HasInfoDesk = case r.IsInfo WHEN 1 then r.IsInfo else (select cast(count(*) as bit)
                         from GuideRole gr join Roles r2 on gr.RoleID = r2.RoleID where gr.GuideID = g.GuideID and r2.IsInfo = 1) end
				FROM SubOffers o  join Guides g on o.GuideID = g.GuideID join Shifts s on s.ShiftID = o.ShiftID
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
