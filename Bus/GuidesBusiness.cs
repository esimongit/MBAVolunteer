using System;
using System.Collections.Generic; 
using System.Text;
using System.Data;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class GuidesBusiness
    {
        public DataTable Search(string Pattern)
        {
            if (String.IsNullOrEmpty(Pattern) || Pattern.Length < 3)
                return null;
            GuidesDM dm = new GuidesDM();
            return dm.Search(Pattern, 0, 0, false);
        }
        public ObjectList<GuideSubstituteObject> SelectForShift(int ShiftID, DateTime dt)
        {
            GuideSubstituteDM dm= new GuideSubstituteDM();
            ObjectList<GuideSubstituteObject> dList = dm.FetchForShift(ShiftID, dt);
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (GuideDropinsObject obj in ddm.FetchForShift(ShiftID, dt))
            {
                GuideSubstituteObject newsubs = new GuideSubstituteObject();
                newsubs.ShiftID = obj.ShiftID;
                newsubs.Sequence = obj.Sequence;
                newsubs.Sub = obj.VolID;
                newsubs.SubDate = obj.DropinDate;
                newsubs.FirstName = "Drop-in";
                newsubs.SubFirst = obj.FirstName;
                newsubs.SubLast = obj.LastName;
                newsubs.Role = obj.Role;
                dList.Add(newsubs);
            }
            return dList;
        }

        public ObjectList<GuideSubstituteObject> SelectForGuide(int GuideID )
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            ObjectList<GuideSubstituteObject> dList = dm.FetchAllForGuide(GuideID );
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (GuideDropinsObject obj in ddm.FetchAllForGuide(GuideID, false))
            {
                GuideSubstituteObject newsubs = new GuideSubstituteObject();
                newsubs.ShiftID = obj.ShiftID;
                newsubs.Sequence = obj.Sequence;
                newsubs.Sub = obj.VolID;
                newsubs.SubDate = obj.DropinDate;
                newsubs.FirstName = "Drop-in";
                newsubs.SubFirst = obj.FirstName;
                newsubs.SubLast = obj.LastName;
                newsubs.Role = obj.Role;
                dList.Add(newsubs);
            }
            dList.Sort(SubSort);
            return dList;
        }
        public ObjectList<GuideSubstituteObject> SelectForDate( DateTime dt, int GuideID)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            ObjectList<GuideSubstituteObject> eList = dm.FetchForSub(GuideID, dt);
            ObjectList<GuideSubstituteObject> dList = dm.FetchForDate( dt);
            List<int> ShiftsOccupied = new List<int>();
            foreach(GuideSubstituteObject obj in eList)
            {
                if (!ShiftsOccupied.Contains(obj.Sequence))
                    ShiftsOccupied.Add(obj.Sequence);
            }
            for (int i = 0; i < dList.Count; i++)
            {
                if (ShiftsOccupied.Contains(dList[i].Sequence) && dList[i].SubstituteID != GuideID)
                {
                    dList[i].NoSub = false;
                    dList[i].IsSub = true;
                }
                 
                dList[i].CanSub = (dList[i].NoSub   && (dList[i].GuideID != GuideID));
            }
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (GuideDropinsObject obj in ddm.FetchForDate( dt))
            {
                GuideSubstituteObject newsubs = new GuideSubstituteObject();
                newsubs.ShiftID = obj.ShiftID;
                newsubs.Sequence = obj.Sequence;
                newsubs.Sub = obj.VolID;
                newsubs.SubDate = obj.DropinDate;
                newsubs.FirstName = "Drop-in";
                newsubs.NoSub = false;
                newsubs.HasSub = true;
                newsubs.SubFirst = obj.FirstName;
                newsubs.SubLast = obj.LastName;
                newsubs.Role = obj.Role;
                dList.Add(newsubs);
            }
            dList.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
            return dList;
        }
        public ObjectList<GuideSubstituteObject> SelectRequestsForDate(DateTime dt, int GuideID )
        {
            ObjectList<GuideSubstituteObject> dList = new ObjectList<GuideSubstituteObject>();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            foreach (GuideSubstituteObject obj in dm.FetchForDate(dt))
            {
                if (obj.SubstituteID == 0)
                {
                    obj.CanSub = (GuideID != obj.GuideID);                   
                }
                dList.Add(obj);
            }
            return dList;
        }
        public ObjectList<GuideSubstituteObject> SelectSubsForDate(DateTime dt)
        {
            ObjectList<GuideSubstituteObject> dList = new ObjectList<GuideSubstituteObject>();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            foreach (GuideSubstituteObject obj in dm.FetchForDate(dt))
            {
                if (obj.SubstituteID > 0)
                    dList.Add(obj);
            }
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (GuideDropinsObject obj in ddm.FetchForDate(dt))
            {
                GuideSubstituteObject newsubs = new GuideSubstituteObject();
                newsubs.ShiftID = obj.ShiftID;
                newsubs.Sequence = obj.Sequence;
                newsubs.Sub = obj.VolID;
                newsubs.SubDate = obj.DropinDate;
                newsubs.FirstName = "Drop-in";
                newsubs.NoSub = false;
                newsubs.HasSub = true;
                newsubs.SubFirst = obj.FirstName;
                newsubs.SubLast = obj.LastName;
                newsubs.Role = obj.Role;
                dList.Add(newsubs);
            }
            return dList;
        }
        
        
        public DataTable Roster(int ShiftID, DateTime dt)
        {
            return RosterList(ShiftID, dt).RenderAsTable();
        }
        public ObjectList<GuidesObject> RosterList(int ShiftID, DateTime dt)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            GuidesDM dm = new GuidesDM();
            ObjectList<GuidesObject> dList = dm.FetchForShift(ShiftID);
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideSubstituteObject> sList = sdm.FetchForShift(ShiftID, dt);
            ObjectList<GuideDropinsObject> pList = ddm.FetchForShift(ShiftID, dt);
            ObjectList<GuideDropinsObject> rList = ddm.FetchOnShift(ShiftID, dt);
            ObjectList<GuidesObject> iList = new ObjectList<GuidesObject>();
            foreach (GuidesObject obj in dList)
            {
                GuideSubstituteObject sub = sList.Find(x => x.GuideID == obj.GuideID);
                if (sub != null)
                {
                    obj.SubRequested = true;
                    obj.Sub = sub.Sub;
                    obj.SubDescription = sub.Sub == String.Empty ? "No substitute" : "Substitute: " + sub.SubName + ", " + sub.SubRole;
                    
                }
                if (obj.RoleName.Contains("Info"))
                    iList.Add(obj);
                else
                    Results.Add(obj);
            }
            foreach (GuideDropinsObject drop in pList)
            {
                GuidesObject obj = new GuidesObject();
                obj.FirstName = drop.FirstName;
                obj.LastName = drop.LastName;
                obj.GuideName = drop.FirstName + " " + drop.LastName;
                obj.Phone = drop.Phone;
                obj.Email = drop.Email;
                obj.VolID = drop.VolID;
                obj.Sequence = drop.Sequence;
                obj.GuideID = drop.GuideID;
                obj.SubDescription = "Drop In";
                obj.RoleName = drop.Role;
                if (obj.RoleName.Contains("Info"))
                    iList.Add(obj);
                else
                    Results.Add(obj); 
            }
            foreach (GuideDropinsObject drop in rList)
            {
                GuidesObject obj = new GuidesObject();
                obj.FirstName = drop.FirstName;
                obj.LastName = drop.LastName;
                obj.GuideName = drop.FirstName + " " + drop.LastName;
                obj.Phone = drop.Phone;
                obj.Email = drop.Email;
                obj.VolID = drop.VolID;
                obj.Sequence = drop.Sequence;
                obj.GuideID = drop.GuideID;
                obj.SubDescription = "Special";
                obj.RoleName = drop.Role;
                if (obj.RoleName.Contains("Info"))
                    iList.Add(obj);
                else
                    Results.Add(obj);
            }
            Results.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
            Results.AddRange(iList);
            return Results;
        }

        // Staff Update
        public void UpdateRoster( DateTime dt, bool SubRequested, string Sub,  int ShiftID, int GuideID)
        {
            SubstitutesBusiness sb = new SubstitutesBusiness();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            int SubstituteID = 0;
            if (!String.IsNullOrEmpty(Sub))
            {
                SubstituteID = gdm.GuideForVol(Sub);
                if (SubstituteID == 0)
                    throw new Exception("Guide ID entered is unknown");
            }
            // SubstituteID = 0 means remove sub
            GuideSubstituteObject obj = dm.FetchForGuide(GuideID, ShiftID, dt);
            if (SubRequested)
            {
                if (obj == null)
                {
                    obj = new GuideSubstituteObject();
                    obj.GuideID = GuideID;
                    obj.ShiftID = ShiftID;
                    obj.SubDate = dt;
                    obj.SubstituteID = SubstituteID;
                    obj.DateEntered = DateTime.Today;

                    dm.Save(obj);
                    // Re-fetch to populate fields
                    obj = dm.FetchRecord("GuideSubstituteID", dm.GetLast());
                    sb.NotifyCaptains(obj);
                    // Immediate notification if the request is for today
                    if (obj.SubDate == DateTime.Today && SubstituteID == 0)
                        sb.NotifyOffers(GuideID, ShiftID, dt);
                }
                else
                {
                    // Previously entered sub, SubstituteID = 0 means no sub.
                  
                    obj.SubstituteID = SubstituteID;

                    if (sb.AddSub(obj))
                    {
                        // Refetch to get the new Sub info (or "Need Sub")
                        obj = dm.FetchRecord("GuideSubstituteID", obj.GuideSubstituteID);
                        sb.NotifyCaptains(obj);
                    }
                }
            }
            else
            {
                // No Sub requested -- is this a delete?
                if (obj != null)
                {
                    dm.Delete(obj.GuideSubstituteID);
                    obj.GuideSubstituteID = 0;

                    sb.NotifyCaptains(obj);
                }
            }
        }
        public void AddDropIn(string VolID, DateTime dt, int ShiftID)
        {
            if (String.IsNullOrEmpty(VolID))
                return;
            GuidesDM gdm = new GuidesDM();
            int GuideID = gdm.GuideForVol(VolID);
            if (GuideID == 0)
                throw new Exception("Guide ID entered is unknown");
            GuideDropinsDM dm = new GuideDropinsDM();
            // Already dropping in this shift
            GuideDropinsObject dropin = dm.FetchForGuide(GuideID, dt, ShiftID);
            if (dropin != null)
                return;
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideSubstituteObject sub  = sdm.FetchBySub(GuideID, dt, ShiftID );
            if (sub != null)
                throw new Exception("Guide is already substituting on this shift and date.");
            GuidesObject guide = gdm.FetchGuide(GuideID);
            if (guide.Shifts.Find(x => x.ShiftID == ShiftID) != null)
                throw new Exception("Guide is serving on this shift.");
            dropin = new GuideDropinsObject();
            dropin.GuideID = GuideID;
            dropin.DropinDate = dt;
            dropin.ShiftID = ShiftID;
            dm.Save(dropin);
        }

        public GuidesObject SelectGuide(int GuideID)
        {
            GuidesDM dm = new GuidesDM();
            GuidesObject obj =  dm.FetchGuide(GuideID);
            if (obj != null && obj.HasLogin)
            {
                SiteMembershipDM sdm = new SiteMembershipDM("MBAV");
                SiteMembershipUser u = sdm.FetchUser(obj.VolID);
                if (u != null)
                    obj.UserId = u.UserId;
            }
            return obj;
        } 
        public void UpdateGuide(int GuideID, string VolID, string FirstName, string LastName, string Phone, string Cell, bool CellPreferred, string Email, 
            int AddShift, int RoleID,  bool Inactive, string Notes, bool MaskPersonalInfo, int AddRole)
        {
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(GuideID);
         
            guide.LastName = LastName;
            guide.FirstName = FirstName;
            guide.Phone = Phone;
            guide.Cell = Cell;
            guide.CellPreferred = CellPreferred;
            if (String.IsNullOrEmpty(Phone) && !String.IsNullOrEmpty(Cell))
                guide.CellPreferred = true;
            guide.Email = Email;
            // If AddShift is set, Update will try to add that shift for this guide
            guide.AddShift = AddShift;
            guide.AddRole = AddRole;
            guide.RoleID = RoleID;
            guide.Inactive = Inactive;
            guide.Notes = Notes;
            guide.MaskPersonalInfo = MaskPersonalInfo;
            guide.VolID = VolID;
            guide.LastUpdate = DateTime.Now;
            guide.UpdateBy = UserSecurity.GetUserName();
            dm.Update(guide);
            if (Inactive)
            {
                GuideSubstituteDM sdm = new GuideSubstituteDM();
                SubOffersDM odm = new SubOffersDM();
                sdm.DeleteAllForGuide(GuideID);
                odm.DeleteAllForGuide(GuideID);
            }
        }
        protected int SubSort(GuideSubstituteObject x, GuideSubstituteObject y)
        {
            int ret = 0;
            ret = x.SubDate.CompareTo(y.SubDate);
            if (ret == 0)
                ret = x.Sequence.CompareTo(y.Sequence);
            return ret;
        }
    }
}
