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
        // For CalendarList on mbav for Info Center Only
        public ObjectList<GuideSubstituteObject> SelectInfoCommitments(int GuideID)
        {
            
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            GuideSubstituteDM dm = new GuideSubstituteDM();
            foreach (GuideSubstituteObject sub in  dm.FetchAllForSub(GuideID))
            {
                if (!sub.IsInfo)
                    continue;
                Results.Add(sub);
            }
            GuideDropinsDM gdm = new GuideDropinsDM();
            foreach (GuideDropinsObject dropin in gdm.FetchAllForGuide(GuideID, false))
            {
                if (!dropin.IsInfo)
                    continue;
                if (dropin.Recurring && dropin.OnShift)
                    continue;
                GuideSubstituteObject sub = new GuideSubstituteObject(dropin);
                
                sub.ShortName = dropin.ShiftName;
                Results.Add(sub);
            }
            return Results;
        }

        // For CalendarList on mbav for Info Center Only
        public ObjectList<GuideSubstituteObject> SelectInfoRequests()
        {
            
            ObjectList<GuideSubstituteObject> Results = new ObjectList<GuideSubstituteObject>();
            ShiftsDM dm = new ShiftsDM();
            ObjectList<ShiftSummaryObject> dList = dm.MissingInfo(DateTime.Today, DateTime.Today.AddMonths(4));
         
            foreach (ShiftSummaryObject obj in dList)
            {
                for (int i = 1; i < 1 + obj.InfoQuota - obj.InfoCnt; i++)
                {
                    GuideSubstituteObject sub = new GuideSubstituteObject();
                    sub.ShiftID = obj.ShiftID;
                    sub.GuideID = i;
                    sub.SubDate = obj.ShiftDate;
                    sub.PartnerName = obj.Info;
                    sub.ShortName = obj.ShortName;
                    sub.ShiftStart = obj.ShiftStart;
                    sub.ShiftEnd = obj.ShiftEnd;
                    Results.Add(sub);
                }
            }

            return Results;
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

        public ObjectList<CalendarDateObject> CalendarList(int Year, int Month, int GuideID, int RoleID)
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            GuidesDM gdm = new GuidesDM();
            ShiftsDM sdm = new ShiftsDM();
            ObjectList<CalendarDateObject> Results = new ObjectList<CalendarDateObject>();
            ObjectList<GuideSubstituteObject> sList = dm.FetchForMonth(Year, Month, RoleID);
            ObjectList<GuideDropinsObject> dList = ddm.FetchForMonth(Year, Month, GuideID, RoleID);
            ObjectList<ShiftsObject> tList = sdm.ShiftsForMonth(Year, Month);
             GuidesObject guide = gdm.FetchGuide(GuideID);
            DateTime CurDay = new DateTime(Year, Month, 1);
           
            while (CurDay.Month == Month)
            {
                GuideDropinsObject drop = dList.Find(x => x.DropinDate == CurDay);
                CalendarDateObject obj = new CalendarDateObject();
                obj.Critical = false;
                obj.Dt = CurDay;
                // Regular volunteers on shift -- IsSubstitute means you are on shift for that day
                if (!guide.IrregularSchedule && sdm.IsShiftOnDate(guide.ShiftID, CurDay) && (RoleID == 0  || RoleID == guide.RoleID))
                    obj.IsSubstitute = true;

                if (drop != null && guide.IrregularSchedule && drop.OnShift && ((RoleID == RolesDM.GetInfo() && drop.IsInfo) || (RoleID == 0 && !drop.IsInfo)))
                    obj.IsSubstitute = true;
                // Substitutes. If you asked for a sub, you are not on shift that day, so remove the prior IsSubstitute
                // If you are a sub, you are on shift for the day
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
                  
                }
                // If you are dropping in without an OnShift flag, you are on shift, adjust for Info on RoleID filter
              
                if (drop != null && !guide.IrregularSchedule)
                {                   
                    obj.IsSubstitute = (!drop.OnShift && ((RoleID == RolesDM.GetInfo() && drop.IsInfo) || (RoleID == 0 && !drop.IsInfo)));
                }

                // Lookup the shift 
                foreach (ShiftsObject shift in tList.FindAll(x => x.ShiftDate == CurDay))
                {
                    if (shift.Quota > shift.Attendance)
                        obj.Critical = true;
                }
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
            ObjectList<GuideSubstituteObject> dList = dm.FetchForDate(DateTime.Today, 0);
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
            if (!String.IsNullOrWhiteSpace(Sub))
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
             
            if (obj.SubstituteID == 0)
            {
                // Remove an existing sub.
                dm.Update(obj);
                return true;
            }
            // Remove a redundant dropin offer.
            GuideDropinsObject dropin = ddm.FetchForGuide(obj.SubstituteID, obj.SubDate, obj.ShiftID);
            if (dropin != null)
            {
                ddm.Delete(dropin.GuideDropinID);
            }
           
            //  add a new sub or replace one.
            // In either case, notify folks
            dm.Update(obj);
            return true;
        }

        // Get a year's worth of the future dates for a given shift, mark as selected if already a drop-in
        public ObjectList<GuideDropinsObject> FutureDatesForShift(int ShiftID, int GuideID, int RoleID)
        {
            if (ShiftID == 0)
                return null;
            GuideDropinsDM ddm = new GuideDropinsDM();
            ObjectList<GuideDropinsObject> dList = ddm.FetchOnShiftForGuide(GuideID,ShiftID, RoleID);
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

        public int SetFutureABShifts(int GuideID, int ShiftID, int RoleA, int RoleB)
        {
            GuideDropinsDM dm = new GuideDropinsDM();
            int cnt = 0;
            ShiftsDM sdm = new ShiftsDM();
            ShiftsObject shift = sdm.FetchRecord("ShiftID", ShiftID);
            DateTime Adt;
            DateTime Bdt;
            if (shift.AWeek)
            {
                Adt = sdm.NextDate("A", ShiftID);
                while (Adt < DateTime.Today.AddYears(1))
                {
                    dm.SaveOnShift(GuideID, ShiftID, Adt, RoleA);
                    Adt = Adt.AddDays(14);
                    cnt++;
                }
            }
            if (shift.BWeek)
            {
                Bdt = sdm.NextDate("B", ShiftID);
                while (Bdt < DateTime.Today.AddYears(1))
                {
                    dm.SaveOnShift(GuideID, ShiftID, Bdt, RoleB);
                    Bdt = Bdt.AddDays(14);
                    cnt++;
                }
            }
            return cnt;
        }
        //Nightly notifications of who needs a sub.  Only send the mail if there is at least one new request.
        public int SubNotices(string VolunteerUrl)
        {
            GuidesDM gdm = new GuidesDM();
             
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            ObjectList<GuidesObject> offers = gdm.GuidesWithOffers();
            ObjectList<GuideSubstituteObject> dList = new ObjectList<GuideSubstituteObject>();
            EmailBusiness eb = new EmailBusiness();
            int cnt = 0;
            DateTime Yesterday = DateTime.Today.AddDays(-1);
            string NewFlag = "*NEW*";
            foreach (GuidesObject offer in offers)
            {

                bool doPrint = false;
                dList = sdm.FetchRequests(offer.GuideID, 0);
                if (dList.Count == 0)
                    continue;

                string msg = String.Format("Dear {0}<br/><br/> <p>Here is a list of Volunteers who have outstanding requests for substitutes on shifts in which you have expressed an interest</p><ul>",
                    offer.FirstName);
                DateTime odate = DateTime.Today;
         
                foreach (GuideSubstituteObject obj in dList)
                {
                    // If the request is for Info Desk, make sure the offer can do Info desk.
                    if (obj.IsInfo && !offer.HasInfoDesk)
                        continue;
                    // Only send exclusive Info Center guides requests for Info center 
                    if (offer.IsInfo && !obj.IsInfo)
                        continue;
                   
                    string flag = (obj.DateEntered > Yesterday) ? NewFlag : String.Empty;
                    if (odate != obj.SubDate)
                    {
                        odate = obj.SubDate;
                        msg += "<br />";
                    }
                    string link = String.Format("{0}/SubRequest.aspx?dt={1}", VolunteerUrl, obj.SubDate.ToShortDateString());
                    msg += String.Format("<li><a href='{0}'>{1}: {2} ({3}) needs a substitute for shift {4} {5}.</a></li>",
                        link, obj.SubDate.ToLongDateString(),
                        obj.GuideName, obj.Role, obj.Sequence, flag);
                    if (flag == NewFlag)
                        doPrint = true;
                }
                if (!doPrint)
                {
                    // If there is an offer from this guide less than 2 days old, they get the mail.
                    SubOffersDM odm = new SubOffersDM();
                    foreach (SubOffersObject soffer in odm.FetchForGuide(offer.GuideID))
                    {
                        if (soffer.DateEntered > DateTime.Today.AddDays(-2))
                            doPrint = true;
                    }
                }
                if (doPrint)
                {
                    msg += "</ul>. <p>Click on any record in the list to open the Substitute Website for the date listed.</p>";
                    eb.SendMail("substitute@mbayaq.org", offer.Email, "Pending Substitute Requests", msg, true);
                    cnt++;
                }
            }
            return cnt;
        }

 
        public int SendReminders()
        {
            GuideSubstituteDM dm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            StaticFieldsDM sdm = new StaticFieldsDM();
            int cnt = 0;
            foreach (StaticFieldsObject stat in sdm.FetchValue("ReminderDays"))
            {
                int ndays = Convert.ToInt32(stat.FieldValue);
                DateTime dt = DateTime.Today;
                dt = dt.AddDays(ndays);
                ObjectList<GuideSubstituteObject> dList = dm.FetchForDate(dt,0);
                ObjectList<GuideDropinsObject> eList = ddm.FetchForDate(dt, 0);
                EmailBusiness eb = new EmailBusiness();
                MailTextDM mtdm = new MailTextDM();
                MailTextObject mtobj = mtdm.FetchForSymbol("SubReminder");
                MailTextObject mzobj = mtdm.FetchForSymbol("NeedSubReminder");
                foreach (GuideSubstituteObject obj in dList)
                {
                    if (obj.Sub != String.Empty)
                    {
                        if (obj.Email != String.Empty)
                        {
                            eb.SendMail(mtobj.MailFrom, obj.SubEmail, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                            cnt++;
                        }
                    }
                    else
                    {
                        // Reminders to unfulfilled Sub requests.
                        eb.SendMail(mzobj.MailFrom, obj.SubEmail, mzobj.Subject, mzobj.CompletedText(obj), mzobj.IsHtml);
                        cnt++;
                    }
                }
                mtobj = mtdm.FetchForSymbol("DropinReminder");
                foreach (GuideDropinsObject obj in eList)
                {
                    if (obj.Email != String.Empty)
                    {

                        cnt++;
                        eb.SendMail(mtobj.MailFrom, obj.Email, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                    }
                }
            }
            return cnt;
        }

        public void NotifyOffers(int GuideID, int ShiftID, DateTime dt)
        {
            EmailBusiness eb = new EmailBusiness();
            SubOffersDM dm = new SubOffersDM();
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            ObjectList<SubOffersObject> dList = dm.FetchForShift(ShiftID, false, 0);
            MailTextDM mtdm = new MailTextDM();
            MailTextObject mtobj = mtdm.FetchForSymbol("InterestNotify");
            foreach(SubOffersObject obj in dList)
            {
                if (!obj.NotifySubRequests)
                    continue;
                if (guide.IsInfo && !obj.HasInfoDesk)
                    continue;
                obj.dt = dt;
                obj.RequestorID = guide.VolID;
                obj.RequestorName = guide.GuideName;
                obj.RequestorEmail = guide.Email;
                obj.RequestorPhone = guide.Phone;
                obj.Sequence = guide.Sequence;
                eb.SendMail(mtobj.MailFrom,  obj.Email, mtobj.Subject, mtobj.CompletedText(obj), mtobj.IsHtml);
                
            }
        }
        public void NotifyCaptains(GuideSubstituteObject obj, int RoleID)
        {
            GuidesDM dm = new GuidesDM();
            MailTextDM mtdm = new MailTextDM();
            ObjectList<GuidesObject> captains = dm.FetchCaptains(obj.ShiftID);
            MailTextObject mtobj = mtdm.FetchForSymbol("NotifyCaptains");
            string msg = mtobj.CompletedText(obj);
            Notify(captains, msg, RoleID);
        }

        // Info Center notify
        public void NotifyGE(string msg)
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
        public void NotifyVO(string msg)
        {
            MailTextDM mtdm = new MailTextDM();
            EmailBusiness eb = new EmailBusiness(); 
            MailTextObject mtobj = mtdm.FetchForSymbol("NotifyCaptains");
            string Email = StaticFieldsObject.StaticValue("InfoNotificationEmail");
            string Subject = "Info Center Substitute Notice.";
            if (mtobj != null & mtobj.Enabled)
            {
                eb.SendMail(mtobj.MailFrom, Email, Subject, msg, true);
            }
        }
        public void Notify (ObjectList<GuidesObject> recipients, string msg, int RoleID)
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
                        eb.SendMail(mtobj.MailFrom, recipient.Email, mtobj.Subject, msg, mtobj.IsHtml);
                         
                    }
                }
            }
            NotifyVO(msg);
            if (RoleID == RolesDM.GetInfo())
                NotifyGE(msg);
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
