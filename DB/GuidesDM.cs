
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using NQN.Core;

namespace  NQN.DB
{
	public class GuidesDM : DBAccess<GuidesObject>
	{
        public int GuideForVol(string VolID)
        {
            int GuideID = 0;
            string qry = "select min(GuideID) from Guides where   VolID = @VolID ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("VolID", VolID));
                try
                {
                    GuideID = Convert.ToInt32(myc.ExecuteScalar());
                }
                catch { }
            }
            return GuideID;
        }

        public bool CheckVolID(GuidesObject obj)
        {
            bool ret = false;
            string qry = "select cast(1 as bit) from Guides where  VolID = @VolID and GuideID != @GuideID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("VolID", obj.VolID));
                myc.Parameters.Add(new SqlParameter("GuideID", obj.GuideID)); 
                try
                {
                    ret = Convert.ToBoolean(myc.ExecuteScalar());
                }
                catch { }
            }
            return ret;
        }
        public GuidesObject FetchByEmail (string Email)
        {
            GuidesObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.Email = @Email";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Email", Email));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public GuidesObject FetchByNameAndEmail(string First, string Last, string Email)
        {
            GuidesObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.FirstName = @First and g.LastName = @Last and g.Email = @Email";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Email", Email));
                myc.Parameters.Add(new SqlParameter("First", First));
                myc.Parameters.Add(new SqlParameter("Last", Last));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public GuidesObject FetchGuide(int GuideID)
        {
            GuidesObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.GuideID = @GuideID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }
        public GuidesObject FetchGuide(string VolID)
        {
            GuidesObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.VolID = @VolID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("VolID", VolID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            return obj;
        }

        public  ObjectList<GuidesObject> FetchRoleForShift(int ShiftID, string Role)
        {
            RolesDM dm = new RolesDM();
            RolesObject role = dm.FetchRole(Role);
            if (role == null)
                return null;
             ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
             string qry = ReadAllCommand() + " where  isnull(Inactive,0) = 0 and (r.RoleName = @Role or r2.RoleName = @Role )";
             if (ShiftID > 0)
                 qry += " and g.ShiftID = @ShiftID ";
             qry += " order by DOW,Sequence,FirstName  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("Role", Role));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public DataTable Search(string Pattern, int ShiftID, bool Inactive)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllCommand() + " where 1 = 1 ";
            if (!Inactive)
                qry += " and isnull(Inactive,0) = 0 ";

            if (Pattern == null)
                Pattern = String.Empty;
            string Wildcard = String.Empty;
            Pattern = SearchString(Pattern);
            if (Pattern != String.Empty)
            {
                Wildcard = "%"+ Pattern + "%";
                qry += " and VolID = @Pattern or (FirstName like @Wildcard or LastName like @Wildcard) ";
            }
            if (ShiftID > 0)
                qry += " and gs.ShiftID = @ShiftID ";
            qry += " order by FirstName, LastName ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Pattern", Pattern));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("Wildcard", Wildcard));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results.RenderAsTable();
        }
        public ObjectList<GuidesObject> FetchMissingLogins()
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllCommand() + @" where nullif(Email,'') is not null and isnull(Inactive,0) = 0 and VolID not in (select UserName from aspnet_Users u
				 join aspnet_Applications a on u.ApplicationId = a.ApplicationID and a.ApplicationName = 'MBAV') ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public ObjectList<GuidesObject> FetchForShift(int ShiftID)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllCommand() + " WHERE g.ShiftID = @ShiftID and isnull(Inactive,0) = 0 order by FirstName  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID)); 
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        }

        public ObjectList<GuidesObject> FetchCaptains(int ShiftID)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllCommand() + " WHERE  isnull(Inactive,0) = 0 and r.IsCaptain = 1 ";
            if (ShiftID > 0)
                qry += " and g.ShiftID = @ShiftID ";
            qry += " order by DOW,Sequence,FirstName  ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        Results.Add(obj);
                        obj = LoadFrom(reader);
                    }
                }
            }
            return Results;
        } 

        public void Update(string FirstName, string LastName, string Phone, string Email, string CalendarType,   int GuideID)
        {
            GuidesObject obj = FetchGuide(GuideID);
            if (obj == null)
                return;
            obj.FirstName = FirstName;
            obj.LastName = LastName;
            obj.Phone = GuidesObject.Standardize(Phone);
            obj.PhoneDigits = GuidesObject.DigitsOnly(Phone);
            obj.Email = Email;
            obj.CalendarType = CalendarType;
            obj.UpdateBy = "self";
            obj.LastUpdate = DateTime.Now;
            
            Update(obj);
        }
		public void Update(GuidesObject obj)
		{
            if (CheckVolID(obj))
                throw new Exception("An active Guide with this ID Number already exists");
            obj.Phone = GuidesObject.Standardize(obj.Phone);
            obj.PhoneDigits = GuidesObject.DigitsOnly(obj.Phone);
           
			 string qry = @"UPDATE  Guides SET 
				FirstName=@FirstName
				,LastName=@LastName
				,Phone=@Phone
				,Email=@Email 
				,Inactive=@Inactive
				,PhoneDigits=@PhoneDigits 
				,Notes=@Notes
				,VolID=@VolID
				,RoleID=@RoleID
                ,AltRoleID=nullif(@AltRoleID,0)
				,UpdateBy=@UpdateBy
				,LastUpdate=@LastUpdate
				,PreferredName=@PreferredName
                ,CalendarType = nullif(@CalendarType, '')
				 WHERE GuideID = @GuideID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("FirstName",obj.FirstName));
				myc.Parameters.Add(new SqlParameter("LastName",obj.LastName));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
				myc.Parameters.Add(new SqlParameter("Email",obj.Email)); 
				myc.Parameters.Add(new SqlParameter("Inactive",obj.Inactive));
				myc.Parameters.Add(new SqlParameter("PhoneDigits",GuidesObject.DigitsOnly(obj.Phone))); 
				myc.Parameters.Add(new SqlParameter("Notes",obj.Notes));
				myc.Parameters.Add(new SqlParameter("VolID",obj.VolID));
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID));
                myc.Parameters.Add(new SqlParameter("AltRoleID", obj.AltRoleID));
				myc.Parameters.Add(new SqlParameter("UpdateBy",obj.UpdateBy));
				myc.Parameters.Add(new SqlParameter("LastUpdate",obj.LastUpdate));
                myc.Parameters.Add(new SqlParameter("PreferredName", obj.PreferredName));
                myc.Parameters.Add(new SqlParameter("CalendarType", obj.CalendarType));
				myc.ExecuteNonQuery();
			}
		}

		public void Save(GuidesObject obj)
		{
            if (CheckVolID(obj))
                throw new Exception("An active Guide with this ID Number already exists");
			 string qry = @"INSERT INTO Guides (
				[FirstName]
				,[LastName]
				,[Phone]
				,[Email] 
				,[Inactive]
				,[PhoneDigits]
				,[Notes]
				,[VolID]
				,[RoleID]
                ,[AltRoleID]
				,[UpdateBy]
				,[LastUpdate]
				,[PreferredName]
                ,[CalendarType]
				)
			VALUES(
				@FirstName
				,@LastName
				,@Phone
				,@Email 
				,@Inactive
				,@PhoneDigits 
				,@Notes
				,@VolID
				,@RoleID
                ,nullif(@AltRoleID, 0)
				,@UpdateBy
				,getdate()
				,@PreferredName
                ,nullif(@CalendarType, '')
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("FirstName",obj.FirstName));
				myc.Parameters.Add(new SqlParameter("LastName",obj.LastName));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
				myc.Parameters.Add(new SqlParameter("Email",obj.Email)); 
				myc.Parameters.Add(new SqlParameter("Inactive",obj.Inactive));
				myc.Parameters.Add(new SqlParameter("PhoneDigits",GuidesObject.DigitsOnly(obj.Phone))); 
				myc.Parameters.Add(new SqlParameter("Notes",obj.Notes));
				myc.Parameters.Add(new SqlParameter("VolID",obj.VolID));
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID));
                myc.Parameters.Add(new SqlParameter("AltRoleID", obj.AltRoleID));
				myc.Parameters.Add(new SqlParameter("UpdateBy",obj.UpdateBy)); 
				myc.Parameters.Add(new SqlParameter("PreferredName",obj.PreferredName));
                myc.Parameters.Add(new SqlParameter("CalendarType", obj.CalendarType));
				myc.ExecuteNonQuery();
			}
            int GuideID = GetLast();
            if (obj.ShiftID > 0)
                SaveGuideShift(GuideID, obj.ShiftID, true);
		}

        public void Delete(GuidesObject obj)
        {
            Delete(obj.GuideID);
        }
		public void Delete(int GuideID)
		{
            // Drop-ins and Guide Offers are cascade delete, but Subs must be handled.
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();

            foreach (GuideSubstituteObject obj in dm.FetchAllForGuide(GuideID))
            {
                if (obj.SubstituteID > 0)
                {
                    GuideDropinsObject dropin = ddm.FetchForGuide(GuideID, obj.SubDate, obj.ShiftID);
                    if (dropin == null)
                    {
                        // Replace a Sub with a drop-in, since we will drop the Sub request
                        dropin = new GuideDropinsObject();
                        dropin.DropinDate = obj.SubDate;
                        dropin.ShiftID = obj.ShiftID;
                        dropin.GuideID = GuideID;
                        ddm.Save(dropin);
                    }
                }
                dm.Delete(obj.GuideSubstituteID);

            }
            // Guide had offered to sub, just clear the record, and leave the request behind.
            foreach (GuideSubstituteObject obj in dm.FetchAllForSub(GuideID))
            {
                obj.SubstituteID = 0;
                dm.Update(obj);
            }
			string qry = @"DELETE FROM Guides WHERE [GuideID] = @GuideID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
				 myc.ExecuteNonQuery();
			}
		}

        protected void SaveGuideShift(int GuideID, int ShiftID, bool IsPrimary)
        {
            string qry = @"insert into GuideShift (GuideID, ShiftID, IsPrimary)
                select @GuideID, @ShiftID, @IsPrimary where not exists (select 1 from GuideShift where GuideID = @GuideID and ShiftID = @ShiftID)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("IsPrimary", IsPrimary));
                myc.ExecuteNonQuery();
            }
        }
		protected override GuidesObject LoadFrom(SqlDataReader reader)
		{
			if (reader == null) return null;
			if (!reader.Read()) return null;
			GuidesObject obj = new GuidesObject();
			obj.GuideID = GetNullableInt32(reader, "GuideID",0);
			obj.FirstName = GetNullableString(reader, "FirstName",String.Empty);
			obj.LastName = GetNullableString(reader, "LastName",String.Empty);
			obj.Phone = GetNullableString(reader, "Phone",String.Empty);
			obj.Email = GetNullableString(reader, "Email",String.Empty);
			obj.ShiftID = GetNullableInt32(reader, "ShiftID",0);
            obj.Inactive = GetNullableBoolean(reader, "Inactive",false);
			obj.PhoneDigits = GetNullableString(reader, "PhoneDigits",String.Empty);
			obj.SearchKey = GetNullableString(reader, "SearchKey",String.Empty);
			obj.Notes = GetNullableString(reader, "Notes",String.Empty);
			obj.VolID = GetNullableString(reader, "VolID",String.Empty);
			obj.RoleID = GetNullableInt32(reader, "RoleID",0);
			obj.UpdateBy = GetNullableString(reader, "UpdateBy",String.Empty);
			obj.LastUpdate = GetNullableDateTime(reader, "LastUpdate",new DateTime());
			obj.PreferredName = GetNullableString(reader, "PreferredName",String.Empty);
            obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);
            obj.AltRoleID = GetNullableInt32(reader, "AltRoleID", 0);
            obj.AltRoleName = GetNullableString(reader, "AltRoleName", String.Empty);
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.ShortName = GetNullableString(reader, "ShortName", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.DOW = GetNullableInt32(reader, "DOW", 0);
            obj.HasLogin = GetNullableBoolean(reader, "HasLogin", false);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            obj.GuideName =  obj.FirstName + " " + obj.LastName ;
            obj.CalendarType = GetNullableString(reader, "CalendarType", String.Empty);
          
			return obj;
		}

		protected override string ReadAllCommand()
		{
			return @"
			SELECT
				g.[GuideID]
				,[FirstName]
				,[LastName]
				,[Phone]
				,[Email]
				,gs.[ShiftID]
				,[Inactive]
				,[PhoneDigits]
				,[SearchKey]
				,[Notes]
				,[VolID]
                ,[CalendarType]
				,g.[RoleID]
                ,g.[AltRoleID]
                ,r.[MaskContactInfo]
				,[UpdateBy]
				,[LastUpdate]
				,[PreferredName]
                ,r.RoleName
                ,AltRoleName = r2.RoleName
                ,s.ShiftName
                ,s.ShortName
                ,s.Sequence
                ,s.DOW
                ,HasLogin=(select cast(1 as bit) from aspnet_Users where UserName = g.VolID)
				FROM Guides g join Roles r on g.RoleID = r.RoleID
                join GuideShift gs on g.GuideID = gs.GuideID and gs.IsPrimary =1 join Shifts s on s.ShiftID = gs.ShiftID 
                left join Roles r2 on g.AltRoleID = r2.RoleID ";
		}
		public int GetLast()
		{
			string qry = "SELECT IDENT_CURRENT('Guides')";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				return  Convert.ToInt32( myc.ExecuteScalar());
			}
		}

	}
}
