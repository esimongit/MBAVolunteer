
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
            string qry = "select min(GuideID) from Guides where   VolID = @VolID and isnull(Inactive,0) = 0  ";
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
            if (String.IsNullOrEmpty(obj.VolID))
                return false;
            try
            {
                int volid = Convert.ToInt32(obj.VolID);
            }
            catch
            {
                return true;
            }
            string qry = @"select cast(1 as bit) from Guides where  VolID = @VolID and GuideID != @GuideID
                and isnull(Inactive,0) = 0";
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
            ShiftsDM dm = new ShiftsDM();
            string qry = ReadAllCommand() + " WHERE g.Email = @Email and  isnull(Inactive,0) = 0 ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Email", Email));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                }
            }
            if (obj != null)
                obj.Shifts = dm.ShiftsForGuide(obj.GuideID);
            
            return obj;
        }
        public GuidesObject FetchByNameAndEmail(string First, string Last, string Email)
        {
            GuidesObject obj = null;
            ShiftsDM dm = new ShiftsDM();
            string qry = ReadAllCommand() + @" WHERE g.FirstName = @First and g.LastName = @Last and g.Email = @Email
                 and isnull(Inactive,0) = 0 ";
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
            if (obj != null)
                obj.Shifts = dm.ShiftsForGuide(obj.GuideID);
            return obj;
        }
        public GuidesObject FetchGuide(int GuideID, bool IsCaptain)
        {            
            GuidesObject obj =  FetchGuide(GuideID);
            if (obj != null && IsCaptain)
                obj.MaskContactInfo = false;
            return obj;
        }
        public GuidesObject FetchGuide(int GuideID)
        {
            ShiftsDM dm = new ShiftsDM();
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
            if (obj != null)
                obj.Shifts = dm.ShiftsForGuide(obj.GuideID);
            return obj;
        }
         
        public GuidesObject FetchGuide(string VolID)
        {
            ShiftsDM dm = new ShiftsDM();
            GuidesObject obj = null;
            string qry = ReadAllCommand() + " WHERE g.VolID = @VolID and  isnull(Inactive,0) = 0 ";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("VolID", VolID));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);
                   
                }
            }
            if (obj != null)
                obj.Shifts = dm.ShiftsForGuide(obj.GuideID);
            return obj;
        }

        public  ObjectList<GuidesObject> FetchInfoForShift(int ShiftID)
        {
            RolesDM dm = new RolesDM();
            
             ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
             string qry = ReadAllShiftsCommand() + " where  isnull(Inactive,0) = 0 and r.IsInfo = 1 ";
             if (ShiftID > 0)
                 qry += " and gs.ShiftID = @ShiftID ";
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

        public DataTable Search(string Pattern, int ShiftID, int RoleID, bool Inactive)
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
                qry += " and (VolID = @Pattern or FirstName like @Wildcard or LastName like @Wildcard) ";
            }
            if (ShiftID > 0)
                qry += " and @ShiftID in (select ShiftID from GuideShift where GuideID = g.GuideID) ";
            if (RoleID > 0)
                qry += " and (g.RoleID = @RoleID  or @RoleID in (select RoleID from GuideRole where GuideID = g.GuideID)) ";
            qry += " order by FirstName, LastName ";
            ShiftsDM dm = new ShiftsDM();
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("Pattern", Pattern));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.Parameters.Add(new SqlParameter("RoleID", RoleID));
                myc.Parameters.Add(new SqlParameter("Wildcard", Wildcard));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    GuidesObject obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        obj.Shifts = dm.ShiftsForGuide(obj.GuideID);
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

        public DataTable FetchTableForShift(int ShiftID)
        {
            return FetchForShift(ShiftID).RenderAsTable();
        }
        public ObjectList<GuidesObject> FetchForShift(int ShiftID)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllShiftsCommand() + " WHERE gs.ShiftID = @ShiftID and isnull(Inactive,0) = 0 and isnull(IrregularSchedule,0) = 0 order by FirstName  ";
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
        public ObjectList<GuidesObject> FetchGuidesForShift(int ShiftID)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            string qry = ReadAllShiftsCommand() + " WHERE gs.ShiftID = @ShiftID and isnull(Inactive,0) = 0 and isnull(r.IsInfo,0) = 0  order by FirstName  ";
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
            string qry = ReadAllShiftsCommand() + " WHERE  isnull(Inactive,0) = 0 and r.IsCaptain = 1 ";
            if (ShiftID > 0)
                qry += " and gs.ShiftID = @ShiftID ";
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
        public ObjectList<GuidesObject> GuidesWithOffers()
        {
            return Fetch(@"  where g.NotifySubRequests = 1 and isnull(g.Inactive,0) = 0 
                     and g.GuideID in (select GuideID from SubOffers)");

        }

        // Update from guide uses subset of fields.
        public void Update(string FirstName, string LastName, string Phone, string Email, string CalendarType,  string Cell, bool CellPreferred,
            int GuideID, bool MaskPersonalInfo, bool NotifySubRequests)
        {
            GuidesObject obj = FetchGuide(GuideID);
            if (obj == null)
                return;
            obj.FirstName = FirstName;
            obj.LastName = LastName;
            obj.Phone = GuidesObject.Standardize(Phone);
            obj.Cell = GuidesObject.Standardize(Cell);
            obj.CellPreferred = CellPreferred;
            if (String.IsNullOrEmpty(Phone) && !String.IsNullOrEmpty(Cell))
                obj.CellPreferred = true;
            obj.Email = Email;
            obj.MaskPersonalInfo = MaskPersonalInfo;
            obj.NotifySubRequests = NotifySubRequests;
            obj.CalendarType = CalendarType;
            obj.UpdateBy = "self";
            obj.LastUpdate = DateTime.Now;
            
            Update(obj);
        }
		public void Update(GuidesObject obj)
		{
            if (CheckVolID(obj))
                throw new Exception("This Guide ID has the wrong format or an active Guide with this ID Number already exists");
            obj.Phone = GuidesObject.Standardize(obj.Phone);
            obj.Cell = GuidesObject.Standardize(obj.Cell);
            obj.PhoneDigits = GuidesObject.DigitsOnly(obj.CellPreferred ? obj.Cell : obj.Phone);
           
			 string qry = @"UPDATE  Guides SET 
				FirstName=@FirstName
				,LastName=@LastName
				,Phone=@Phone
                ,Cell=@Cell
                ,CellPreferred = @CellPreferred
				,Email=@Email 
				,Inactive=@Inactive
				,PhoneDigits=@PhoneDigits 
				,Notes=@Notes
				,VolID=@VolID
				,RoleID=@RoleID 
                ,MaskPersonalInfo = isnull(@MaskPersonalInfo,0)
                ,NotifySubRequests = isnull(@NotifySubRequests,0)
				,UpdateBy=@UpdateBy
				,LastUpdate=@LastUpdate 
                ,CalendarType = nullif(@CalendarType, '')
                ,IrregularSchedule = @IrregularSchedule
				 WHERE GuideID = @GuideID";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("GuideID",obj.GuideID));
				myc.Parameters.Add(new SqlParameter("FirstName",obj.FirstName));
				myc.Parameters.Add(new SqlParameter("LastName",obj.LastName));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
                myc.Parameters.Add(new SqlParameter("Cell", obj.Cell));
                myc.Parameters.Add(new SqlParameter("CellPreferred", obj.CellPreferred));
                myc.Parameters.Add(new SqlParameter("Email",obj.Email)); 
				myc.Parameters.Add(new SqlParameter("Inactive",obj.Inactive));
                myc.Parameters.Add(new SqlParameter("MaskPersonalInfo", obj.MaskPersonalInfo));
                myc.Parameters.Add(new SqlParameter("NotifySubRequests", obj.NotifySubRequests));
                myc.Parameters.Add(new SqlParameter("PhoneDigits", obj.PhoneDigits));
				myc.Parameters.Add(new SqlParameter("Notes",obj.Notes));
				myc.Parameters.Add(new SqlParameter("VolID",obj.VolID));
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID)); 
				myc.Parameters.Add(new SqlParameter("UpdateBy",obj.UpdateBy));
				myc.Parameters.Add(new SqlParameter("LastUpdate",obj.LastUpdate)); 
                myc.Parameters.Add(new SqlParameter("CalendarType", obj.CalendarType));
                myc.Parameters.Add(new SqlParameter("IrregularSchedule", obj.IrregularSchedule));
                myc.ExecuteNonQuery();
			}
            if (obj.AddShift > 0 && obj.AddShift != obj.ShiftID)
                SaveGuideShift(obj.GuideID, obj.AddShift);
            if (obj.AddRole > 0)
                AddRole(obj.GuideID, obj.AddRole);
        }

		public void Save(GuidesObject obj)
		{
            if (CheckVolID(obj))
                throw new Exception("This Guide ID has the wrong format or an active Guide with this ID Number already exists");
            obj.Phone = GuidesObject.Standardize(obj.Phone);
            obj.Cell = GuidesObject.Standardize(obj.Cell);
            obj.PhoneDigits = GuidesObject.DigitsOnly(obj.CellPreferred ? obj.Cell : obj.Phone);
            string qry = @"INSERT INTO Guides (
				[FirstName]
				,[LastName]
				,[Phone]
                ,[Cell] 
                ,[CellPreferred]
				,[Email] 
				,[Inactive]
				,[PhoneDigits]
				,[Notes]
				,[VolID]
				,[RoleID] 
				,[UpdateBy]
				,[LastUpdate] 
                ,[CalendarType]
                ,[NotifySubRequests]
                ,[IrregularSchedule]
				)
			VALUES(
				@FirstName
				,@LastName
				,@Phone
                ,@Cell 
                ,@CellPreferred
				,@Email 
				,@Inactive
				,@PhoneDigits 
				,@Notes
				,@VolID
				,@RoleID 
				,@UpdateBy
				,getdate() 
                ,nullif(@CalendarType, '')
                ,1
                ,@IrregularSchedule
				)";
			 using (SqlConnection conn = ConnectionFactory.getNew())
			{
				SqlCommand myc = new SqlCommand(qry, conn);
				myc.Parameters.Add(new SqlParameter("FirstName",obj.FirstName));
				myc.Parameters.Add(new SqlParameter("LastName",obj.LastName));
				myc.Parameters.Add(new SqlParameter("Phone",obj.Phone));
				myc.Parameters.Add(new SqlParameter("Email",obj.Email)); 
				myc.Parameters.Add(new SqlParameter("Inactive",obj.Inactive));
                myc.Parameters.Add(new SqlParameter("Cell", obj.Cell));
                myc.Parameters.Add(new SqlParameter("CellPreferred", obj.CellPreferred));
                myc.Parameters.Add(new SqlParameter("PhoneDigits", obj.PhoneDigits)); 
				myc.Parameters.Add(new SqlParameter("Notes",obj.Notes));
				myc.Parameters.Add(new SqlParameter("VolID",obj.VolID));
				myc.Parameters.Add(new SqlParameter("RoleID",obj.RoleID)); 
				myc.Parameters.Add(new SqlParameter("UpdateBy",obj.UpdateBy));  
                myc.Parameters.Add(new SqlParameter("CalendarType", obj.CalendarType));
                myc.Parameters.Add(new SqlParameter("IrregularSchedule", obj.IrregularSchedule));
                myc.ExecuteNonQuery();
			}
            int GuideID = GetLast();
            if (obj.ShiftID > 0)
                SaveGuideShift(GuideID, obj.ShiftID);
            if (obj.AddRole > 0)
                AddRole(GuideID, obj.AddRole);
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

        public void DeleteGuideShift(int GuideID, int ShiftID)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            if (dm.FetchForGuide(GuideID, ShiftID).Count > 0)
                throw new Exception("This shift has outstanding substitute requests.");
            string qry = @"delete GuideShift  where GuideID = @GuideID and ShiftID = @ShiftID";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
                myc.ExecuteNonQuery();
            }
        }
        protected void SaveGuideShift(int GuideID, int ShiftID)
        {
            string qry = @"insert into GuideShift (GuideID, ShiftID)
                select @GuideID, @ShiftID where not exists (select 1 from GuideShift where GuideID = @GuideID and ShiftID = @ShiftID)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("GuideID", GuideID));
                myc.Parameters.Add(new SqlParameter("ShiftID", ShiftID));
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
            obj.Cell = GetNullableString(reader, "Cell", String.Empty);
            obj.CellPreferred = GetNullableBoolean(reader, "CellPreferred", false);
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
            obj.RoleName = GetNullableString(reader, "RoleName", String.Empty);
            obj.AltRoleName = GetNullableString(reader, "AltRoleName", String.Empty);
            obj.ShiftName = GetNullableString(reader, "ShiftName", String.Empty);
            obj.ShortName = GetNullableString(reader, "ShortName", String.Empty);
            obj.Sequence = GetNullableInt32(reader, "Sequence", 0);
            obj.DOW = GetNullableInt32(reader, "DOW", 0);
            obj.HasLogin = GetNullableBoolean(reader, "HasLogin", false);
            obj.MaskContactInfo = GetNullableBoolean(reader, "MaskContactInfo", false);
            obj.NotifySubRequests = GetNullableBoolean(reader, "NotifySubRequests", false);
            obj.MaskPersonalInfo = GetNullableBoolean(reader, "MaskPersonalInfo", false);
            obj.IrregularSchedule = GetNullableBoolean(reader, "IrregularSchedule", false);
            obj.HasInfoDesk = GetNullableBoolean(reader, "HasInfoDesk", false);
            obj.GuideName =  obj.FirstName + " " + obj.LastName ;
            obj.CalendarType = GetNullableString(reader, "CalendarType", String.Empty);
            GuideRoleDM dm = new GuideRoleDM();
            obj.Roles = dm.FetchForGuide(obj.GuideID);
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
                ,[Cell]
                ,[CellPreferred]
				,[Email]
				,[ShiftID] = 0
				,[Inactive]
				,[PhoneDigits]
				,[SearchKey]
				,[Notes]
				,[VolID]
                ,[CalendarType]
                ,[IrregularSchedule]
				,g.[RoleID]
                ,MaskContactInfo = r.[MaskContactInfo] | isnull(g.MaskPersonalInfo,0)
                ,MaskPersonalInfo
				,[UpdateBy]
				,[LastUpdate] 
                ,r.RoleName
                ,AltRoleName = dbo.FlattenRoles(g.GuideID) 
                ,ShiftName = ''
                ,ShortName = ''
                ,Sequence = 0
                ,DOW = 0
                ,NotifySubRequests 
                ,HasLogin=(select cast(1 as bit) from aspnet_Users where UserName = g.VolID)
                ,HasInfoDesk = case r.IsInfo WHEN 1 then r.IsInfo else (select cast(count(*) as bit)
                         from GuideRole gr join Roles r2 on gr.RoleID = r2.RoleID where gr.GuideID = g.GuideID and r2.IsInfo = 1) end
				FROM Guides g join Roles r on g.RoleID = r.RoleID ";
        }
        protected  string ReadAllShiftsCommand()
		{
			return @"
			SELECT
				g.[GuideID]
				,[FirstName]
				,[LastName]
				,[Phone]
                ,[Cell]
                ,[CellPreferred]
				,[Email]
				,gs.[ShiftID]
				,[Inactive]
				,[PhoneDigits]
				,[SearchKey]
				,[Notes]
				,[VolID]
                ,[CalendarType]
                ,[IrregularSchedule]
				,g.[RoleID] 
                ,MaskContactInfo = r.[MaskContactInfo] | isnull(g.MaskPersonalInfo,0)
                ,MaskPersonalInfo
				,[UpdateBy]
				,[LastUpdate] 
                ,r.RoleName
                ,AltRoleName = dbo.FlattenRoles(g.GuideID) 
                ,s.ShiftName
                ,s.ShortName
                ,s.Sequence
                ,s.DOW
                ,NotifySubRequests
                ,HasLogin=(select cast(1 as bit) from aspnet_Users where UserName = g.VolID)
                ,HasInfoDesk = case r.IsInfo WHEN 1 then r.IsInfo else (select cast(count(*) as bit)
                         from GuideRole gr join Roles r2 on gr.RoleID = r2.RoleID where gr.GuideID = g.GuideID and r2.IsInfo = 1) end
				FROM Guides g join Roles r on g.RoleID = r.RoleID
                join GuideShift gs on g.GuideID = gs.GuideID  join Shifts s on s.ShiftID = gs.ShiftID  ";
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

    
        public void AddRole(int GuideID, int RoleID)
        {
            GuideRoleDM dm = new GuideRoleDM();
            GuideRoleObject obj = new GuideRoleObject();
            obj.GuideID = GuideID;
            obj.RoleID = RoleID;
            obj.Condition = 0;
            dm.Save(obj);
        }
        public void RemoveRole(int GuideID, int RoleID)
        {
            GuideRoleDM dm = new GuideRoleDM();
            dm.DeleteRole(GuideID, RoleID);
        }
	}
}
