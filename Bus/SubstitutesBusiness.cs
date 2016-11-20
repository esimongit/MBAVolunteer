using System;
using System.Collections.Generic; 
using System.Text;
using NQN.Core;
using NQN.DB;

namespace NQN.Bus
{
    public class SubstitutesBusiness
    {
        public ObjectList<GuideSubstituteObject> SelectAllCommitments(int GuideID)
        {

            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideSubstituteObject> subs = sdm.FetchAllForSub(GuideID);


            ObjectList<GuideDropinsObject> drops = ddm.FetchAllForGuide(GuideID, false);
            foreach (GuideDropinsObject drop in drops)
            {
                GuideSubstituteObject obj = new GuideSubstituteObject(drop);
                subs.Add(obj);
            }
            subs.Sort(SubSort);
            return subs;
        }

        public void DeleteCommitment(int GuideSubstituteID)
        {
            if (GuideSubstituteID > 0)
            {
                GuideSubstituteDM dm = new GuideSubstituteDM();
                GuideSubstituteObject guide = dm.FetchRecord("GuideSubstituteID", GuideSubstituteID);
                if (guide == null)
                    return;
                guide.SubstituteID = 0;
                dm.Update(guide);
            }
            else if (GuideSubstituteID < 0)
            {
                GuideDropinsDM ddm = new GuideDropinsDM();
                ddm.Delete(-GuideSubstituteID);
            }
        }
        public string SubShiftsForGuideAndDate(int GuideID, DateTime dt)
        {
            string Shifts = String.Empty;
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideSubstituteObject> subs = sdm.FetchForSub(GuideID, dt);
            string sep = String.Empty;

            foreach (GuideSubstituteObject sub in subs)
            {
                Shifts += sep + sub.Sequence.ToString();
                sep = ", ";
            }

            ObjectList<GuideDropinsObject> drops = ddm.FetchForGuide(GuideID, dt);
            foreach (GuideDropinsObject drop in drops)
            {
                Shifts += sep + drop.Sequence.ToString();
                sep = ", ";
            }

            return Shifts;
        }

        public ObjectList<GuideSubstituteObject> SelectShiftsForGuideAndDate(int GuideID, DateTime dt)
        {
           
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ShiftsDM dm = new ShiftsDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            ObjectList<GuideSubstituteObject> subs = sdm.FetchForSub(GuideID, dt);
             
            ObjectList<GuideDropinsObject> drops = ddm.FetchForGuide(GuideID, dt);
            foreach (GuideDropinsObject drop in drops)
            {
                GuideSubstituteObject obj = new GuideSubstituteObject(drop);
                
                subs.Add(obj);
            }
            ObjectList<ShiftsObject> shifts = dm.FetchAll();
            for (int i = 0; i < subs.Count; i++)
            {
                ShiftsObject shift = shifts.Find(x => x.ShiftID == subs[i].ShiftID);
                subs[i].ShiftStart = shift.ShiftStart;
                subs[i].ShiftEnd = shift.ShiftEnd;
                subs[i].CalendarType = guide.CalendarType;
            }
            return subs;
             
        }

        public  GuideSubstituteObject SelectSubstituteShift(int GuideID, DateTime dt, int ShiftID)
        {

            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            ShiftsDM dm = new ShiftsDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
             GuideSubstituteObject sub = sdm.FetchBySub(GuideID, dt, ShiftID);
            if (sub == null)
            {
                GuideDropinsObject drop = ddm.FetchForGuide(GuideID, dt, ShiftID);
                if (drop != null)
                {
                    sub = new GuideSubstituteObject(drop);
                   
                }
            }
            if (sub == null)
                return sub;
            ShiftsObject shift = dm.FetchShift(ShiftID);
            sub.ShiftStart = shift.ShiftStart;
            sub.ShiftEnd = shift.ShiftEnd;
            sub.CalendarType = guide.CalendarType;
            return sub;
        }

        public ObjectList<CalendarDateObject> CalendarList(int Year, int Month, int GuideID)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            GuidesDM gdm = new GuidesDM();
            ShiftsDM sdm = new ShiftsDM();
            ObjectList<CalendarDateObject> Results = new ObjectList<CalendarDateObject>();
            ObjectList<GuideSubstituteObject> sList = dm.FetchForMonth(Year, Month);
            ObjectList<GuideDropinsObject> dList = ddm.FetchForMonth(Year, Month, GuideID);
             GuidesObject guide = gdm.FetchGuide(GuideID);
            DateTime CurDay = new DateTime(Year, Month, 1);
           
            while (CurDay.Month == Month)
            {
                CalendarDateObject obj = new CalendarDateObject();
                obj.Dt = CurDay;
                if (sdm.IsShiftOnDate(guide.ShiftID, CurDay))
                    obj.IsSubstitute = true;
                foreach (GuideSubstituteObject sub in sList.FindAll(x => x.SubDate == CurDay))
                {
                    if (sub.HasSub)
                        obj.HasSubstitutes = true;
                    if (sub.NoSub)
                        obj.NeedSubstitutes = true;
                    if (sub.SubstituteID == GuideID)
                        obj.IsSubstitute = true;
                    if (sub.GuideID == GuideID)
                        obj.IsSubstitute = false;
                   obj.Critical = sub.Critical;
                }
                GuideDropinsObject drop = dList.Find(x => x.DropinDate == CurDay);
                if (drop != null)
                    obj.IsSubstitute = true;
                
                // No volunteers on Christmas day
                if (!(CurDay.Month == 12 && CurDay.Day == 25))
                    Results.Add(obj);
                CurDay = CurDay.AddDays(1);
            }
            return Results;
        }
        public ObjectList<GuideSubstituteObject> SubReport()
        {
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            Results = dm.SubReport();
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (GuideDropinsObject drop in ddm.FetchFuture())
            {
                Results.Add(new GuideSubstituteObject(drop));
            }
            Results.Sort(SubSort);
            return Results;
        }
        public ObjectList<GuideSubstituteObject> SelectOpen()
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            ObjectList<GuideSubstituteObject> dList = dm.FetchForDate(DateTime.Today);
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            foreach (GuideSubstituteObject sub in dList)
            {
                if (sub.NoSub)
                    Results.Add(sub);
            }
            return Results;
        }
        public void SubRequest(int GuideID, DateTime dt, int ShiftID, string Sub)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuidesDM gdm = new GuidesDM();
            GuideSubstituteObject obj = dm.FetchForGuide(GuideID, ShiftID, dt);
         
            if (obj == null)
            {
                obj = new GuideSubstituteObject();
                obj.GuideID = GuideID;
                obj.ShiftID = ShiftID;
                obj.DateEntered = DateTime.Now;
                obj.SubDate = dt;
                dm.Save(obj);
                obj = dm.FetchRecord("GuideSubstituteID", dm.GetLast()); 
            }
            // Have Sub VolID in hand,
            if (Sub != null && Sub.Trim() != String.Empty)
            {
                Sub = Sub.Trim();
                obj.SubstituteID = gdm.GuideForVol(Sub);
                AddSub(obj);  
            }         
        }

        public bool AddSub(GuideSubstituteObject obj)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            GuideSubstituteObject sub = dm.FetchBySub(obj.SubstituteID, obj.SubDate, obj.ShiftID);
            if (sub != null)
            {
                if ( sub.GuideID != obj.GuideID)
                {
                    throw new Exception(String.Format("{0} is already substituting on this Shift", sub.SubName));
                }
                // Otherwise this sub is already registered for you on this shift, just quit here.
                return false;
            }
            sub = dm.FetchForGuide(obj.GuideID, obj.ShiftID, obj.SubDate);
            if (sub != null && sub.SubstituteID > 0)
           {
                throw new Exception(String.Format("{0} is already substituting for {1}.", sub.SubName, sub.GuideName));
            }
            // Remove a redundant dropin offer.
            GuideDropinsObject dropin = ddm.FetchForGuide(obj.SubstituteID, obj.SubDate, obj.ShiftID);
            if (dropin != null)
            {
                ddm.Delete(dropin.GuideDropinID);
            }
           
            dm.Update(obj);
            return true;
        }

        // Get a year's worth of the future dates for a given shift, mark as selected if already a drop-in
        public ObjectList<GuideDropinsObject> FutureDatesForShift(int ShiftID, int GuideID)
        {
            if (ShiftID == 0)
                return null;
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideDropinsObject> dList = ddm.FetchOnShiftForGuide(GuideID,ShiftID);
            ObjectList<GuideDropinsObject> Results = new ObjectList<GuideDropinsObject>();
            ShiftsDM dm = new ShiftsDM();
            ShiftsObject shift = dm.FetchRecord("ShiftID", ShiftID);
            int Increment = 14;
            if (shift.AWeek && shift.BWeek)
                Increment = 7;
            DateTime dt =  DateTime.Today;
            while (!dm.IsShiftOnDate(ShiftID, dt))
                dt = dt.AddDays(1);
            int index = -1;
            while (dt < DateTime.Today.AddYears(1))
            {
                GuideDropinsObject obj =  dList.Find(x=> x.DropinDate == dt);
                if (obj == null)
                {
                    obj = new GuideDropinsObject();
                    obj.Selected = false;
                    obj.GuideDropinID = index--;
                    obj.DropinDate = dt;
                    obj.ShiftID = ShiftID;
                    obj.GuideID = GuideID;
                }
                else
                    obj.Selected = true;
                Results.Add(obj);
                dt = dt.AddDays(Increment);
            }
            return Results;

        }

        //Nightly notifications of who needs a sub
        public void SubNotices(string VolunteerUrl)
        {
            GuidesDM gdm = new GuidesDM();
            SubOffersDM dm = new SubOffersDM();
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            List<int> Guides = dm.GuidesWithOffers();
            ObjectList<GuideSubstituteObject> dList = new ObjectList<GuideSubstituteObject>();
            EmailBusiness eb = new EmailBusiness();
            foreach (int GuideID in Guides)
            {
                dList = sdm.FetchRequests(GuideID);
                if (dList.Count == 0)
                    continue;
                GuidesObject guide = gdm.FetchGuide(GuideID);
                bool HasInfo = guide.RoleName == "Info Desk" || guide.AltRoleName == "InfoDesk";
                string msg = String.Format("Dear {0}<br/><br/> <p>Here is a list of Guides who have outstanding requests for substitutes on shifts in which you have expressed an interest</p><ul>",
                    guide.FirstName);
                DateTime odate = DateTime.Today;
                foreach (GuideSubstituteObject obj in dList)
                {
                    if (obj.Role == "Info Desk" && !HasInfo)
                        continue;
                    string flag = (obj.DateEntered > DateTime.Today.AddDays(-1)) ? " *NEW*" : String.Empty;
                    if (odate != obj.SubDate)
                    {
                        odate = obj.SubDate;
                        msg += "<br />";
                    }
                        string link = String.Format("{0}/SubRequest.aspx?dt={1}",VolunteerUrl, obj.SubDate);
                    msg += String.Format("<li><a href='{0}'>{1}: {2} (3) needs a substitute for shift {4} {5}.</a></li>",
                        link, obj.SubDate.ToLongDateString(),
                        obj.GuideName, obj.Role, obj.Sequence, flag);
                   
                }
                msg += "</ul>. <p>Click on any record in the list to open the Substitute Website for the date listed.</p>";
                eb.SendMail("substitute@mbayaq.org", "ed_simon@yahoo.com", "Pending Substitute Requests", msg, true);
                break;
            }
        }

 
        public void SendReminders()
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            StaticFieldsDM sdm = new StaticFieldsDM();
            foreach (StaticFieldsObject stat in sdm.FetchValue("ReminderDays"))
            {
                int ndays = Convert.ToInt32(stat.FieldValue);
                DateTime dt = DateTime.Today;
                dt.AddDays(ndays);
                ObjectList<GuideSubstituteObject> dList = dm.FetchForDate(dt);
                ObjectList<GuideDropinsObject> eList = ddm.FetchForDate(dt);
                EmailBusiness eb = new EmailBusiness();
                MailTextDM mtdm = new MailTextDM();
                MailTextObject mtobj = mtdm.FetchForSymbol("SubReminder");
                MailTextObject mzobj = mtdm.FetchForSymbol("NeedSubReminder");
                foreach (GuideSubstituteObject obj in dList)
                {
                    if (obj.Sub != String.Empty)
                    {
                        if (obj.Email != String.Empty)
                            eb.SendMail(mtobj.MailFrom, obj.SubEmail, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                    }
                    else
                    {
                        // Reminders to unfulfilled Sub requests.
                        eb.SendMail(mzobj.MailFrom, obj.SubEmail, mzobj.Subject, mzobj.CompletedText(obj), mzobj.IsHtml);
                    }
                }
                mtobj = mtdm.FetchForSymbol("DropinReminder");
                foreach (GuideDropinsObject obj in eList)
                {
                    if (obj.Email != String.Empty)
                        eb.SendMail(mtobj.MailFrom, obj.Email, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                }
            }
        }

        public void NotifyOffers(int GuideID, int ShiftID, DateTime dt)
        {
            EmailBusiness eb = new EmailBusiness();
            SubOffersDM dm = new SubOffersDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            ObjectList<SubOffersObject> dList = dm.FetchForShift(ShiftID, false);
            MailTextDM mtdm = new MailTextDM();
            MailTextObject mtobj = mtdm.FetchForSymbol("InterestNotify");
            foreach(SubOffersObject obj in dList)
            {
                if (!obj.NotifySubRequests)
                    continue;
                obj.dt = dt;
                obj.RequestorID = guide.VolID;
                obj.RequestorName = guide.GuideName;
                obj.RequestorEmail = guide.Email;
                obj.RequestorPhone = guide.Phone;
                obj.Sequence = guide.Sequence;
               // eb.SendMail(mtobj.MailFrom,  obj.Email, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                eb.SendMail(mtobj.MailFrom, "ed_simon@yahoo.com", mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
            }
        }
        public void NotifyCaptains(GuideSubstituteObject obj)
        {
            GuidesDM dm = new GuidesDM();
            MailTextDM mtdm = new MailTextDM();
            ObjectList<GuidesObject> captains = dm.FetchCaptains(obj.ShiftID);
            MailTextObject mtobj = mtdm.FetchForSymbol("NotifyCaptains");
            string msg = mtobj.CompletedText(obj);
            Notify(captains, msg);
        }
        public void NotifyVO(string msg)
        {
            MailTextDM mtdm = new MailTextDM();
            EmailBusiness eb = new EmailBusiness(); 
            MailTextObject mtobj = mtdm.FetchForSymbol("NotifyCaptains");
            string Email = StaticFieldsObject.StaticValue("GuideNotificationEmail");
            string Subject = "Guide Substitute or Drop-in Notice.";
            if (mtobj != null & mtobj.Enabled)
            {
                eb.SendMail(mtobj.MailFrom, Email, Subject, msg, true);
            }
        }
        public void Notify (ObjectList<GuidesObject> recipients, string msg)
        {
            if (recipients.Count == 0)
                return;
            MailTextDM mtdm = new MailTextDM();
            EmailBusiness eb = new EmailBusiness();
            GuidesDM dm = new GuidesDM();
            MailTextObject mtobj = mtdm.FetchForSymbol("NotifyCaptains");

            if (mtobj != null & mtobj.Enabled)
            {
                foreach (GuidesObject recipient in recipients)
                {
                    if (recipient.Email != String.Empty)
                    {
                        // eb.SendMail(mtobj.MailFrom, recipient.Email, mtobj.Subject, msg, mtobj.IsHtml);
                        eb.SendMail(mtobj.MailFrom, "Substitute@mbayaq.org", mtobj.Subject, msg, mtobj.IsHtml);
                    }
                }
            }
            NotifyVO(msg);
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
