using System;
using System.Collections.Generic; 
using System.Text;
using NQN.DB;
using NQN.Core;

namespace NQN.Bus
{
    public class GuidesBusiness
    {
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
        public ObjectList<GuideSubstituteObject> SelectForDate( DateTime dt)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            ObjectList<GuideSubstituteObject> dList = dm.FetchForDate( dt);
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
            return dList;
        }
        public ObjectList<GuideSubstituteObject> SelectRequestsForDate(DateTime dt)
        {
            ObjectList<GuideSubstituteObject> dList = new ObjectList<GuideSubstituteObject>();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            foreach (GuideSubstituteObject obj in dm.FetchForDate(dt))
            {
                if (obj.SubstituteID == 0)
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
        public ObjectList<GuidesObject> Roster(int ShiftID, DateTime dt)
        {
            ObjectList<GuidesObject> Results = new ObjectList<GuidesObject>();
            GuidesDM dm = new GuidesDM();
            ObjectList<GuidesObject> dList = dm.FetchForShift(ShiftID);
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideSubstituteObject> sList = sdm.FetchForShift(ShiftID, dt);
            ObjectList<GuideDropinsObject> pList = ddm.FetchForShift(ShiftID, dt);
            foreach (GuidesObject obj in dList)
            {
                GuideSubstituteObject sub = sList.Find(x => x.GuideID == obj.GuideID);
                if (sub != null)
                {
                    obj.SubRequested = true;
                    obj.Sub = sub.Sub;
                    obj.SubDescription = sub.Sub == String.Empty ? "No substitute" : "Substitute: " + sub.SubName + ", " + sub.SubRole;
                    
                }
                Results.Add(obj);
            }
            foreach (GuideDropinsObject drop in pList)
            {
                GuidesObject obj = new GuidesObject();
                obj.FirstName = drop.FirstName;
                obj.LastName = drop.LastName;
                obj.Phone = drop.Phone;
                obj.Email = drop.Email;
                obj.VolID = drop.VolID;
                obj.Sequence = drop.Sequence;
                obj.GuideID = drop.GuideID;
                obj.SubDescription = "Drop In";
                obj.RoleName = drop.Role;
                Results.Add(obj);
            }
             Results.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
            return Results;
        }

        // Staff Update
        public void UpdateRoster( string Notes, DateTime dt, bool SubRequested, string Sub,   int GuideID)
        {
            SubstitutesBusiness sb = new SubstitutesBusiness();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            int SubstituteID = 0;
            if (Sub != null && Sub.Trim() != String.Empty)
            {
                SubstituteID = gdm.GuideForVol(Sub);
                if (SubstituteID == 0)
                    throw new Exception("Guide ID entered is unknown");
            }
            // SubstituteID = 0 means remove sub
            GuideSubstituteObject obj = dm.FetchForGuide(GuideID, dt);
            if (SubRequested)
            {
                if (obj == null)
                {
                    obj = new GuideSubstituteObject();
                    obj.GuideID = GuideID;
                    obj.ShiftID = guide.ShiftID;
                    obj.SubDate = dt;
                    obj.SubstituteID = SubstituteID;
                    obj.DateEntered = DateTime.Today;

                    dm.Save(obj);
                    // Re-fetch to populate fields
                    obj = dm.FetchRecord("GuideSubstituteID", dm.GetLast());
                    sb.NotifyCaptains(obj);
                }
                else
                {
                    if (Sub != null && Sub.Trim() != String.Empty)
                    {
                        obj.SubstituteID = gdm.GuideForVol(Sub);
                        sb.AddSub(obj);
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
                    sb.NotifyCaptains(obj);
                }
            }
        }
        public void AddDropin(string VolID, DateTime dt, int ShiftID)
        {
            if (VolID == null || VolID.Trim() == String.Empty)
                return;
            GuidesDM gdm = new GuidesDM();
            int GuideID = gdm.GuideForVol(VolID);
            if (GuideID == 0)
                throw new Exception("Guide ID entered is unknown");
            GuideDropinsDM dm = new GuideDropinsDM();
            GuideDropinsObject dropin = dm.FetchForGuide(GuideID, dt, ShiftID);
            if (dropin != null)
                return;
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
        public void UpdateGuide(int GuideID, string VolID, string FirstName, string LastName, string Phone, string Email, 
            int ShiftID, int RoleID, int AltRoleID, bool Inactive, string Notes)
        {
            GuidesDM dm = new GuidesDM();
            GuidesObject guide = dm.FetchGuide(GuideID);
         
            guide.LastName = LastName;
            guide.FirstName = FirstName;
            guide.Phone = Phone;
            guide.Email = Email;
            guide.ShiftID = ShiftID;
            guide.RoleID = RoleID;
            guide.AltRoleID = AltRoleID;
            guide.Inactive = Inactive;
            guide.Notes = Notes;
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
