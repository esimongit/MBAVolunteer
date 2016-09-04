using System;
using System.Collections.Generic;
using System.Text;
using NQN.DB;
using NQN.Core;
using FileHelpers;
namespace NQN.Bus
{
    public class BatchBusiness
    {
        public string Import(string FileName)
        {
            string ret = String.Empty;
            string errors = String.Empty;
            int errcnt = 0;
            BatchImportDM dm = new BatchImportDM();

            dm.Clear();
            FileHelperEngine engine = new FileHelperEngine(typeof(BatchImportCSV));
            engine.Options.IgnoreFirstLines = 1;
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            BatchImportCSV[] res = engine.ReadFile(FileName) as BatchImportCSV[];
            ret = res.Length.ToString() + " records processed.  ";
            if (engine.ErrorManager.HasErrors)
            {
                foreach (ErrorInfo err in engine.ErrorManager.Errors)
                {
                    errcnt++;
                    errors += "Line: " + err.LineNumber + " " + err.ExceptionInfo + " Record: " + err.RecordString;
                }
                ret += errors;
                return ret;
            }

            foreach (BatchImportCSV obj in res)
            {
                BatchImportObject import = new BatchImportObject(obj);
                try
                {
                    dm.Save(import);
                }
                catch (Exception ex)
                {
                    ret += " Save Error: " + ex.Message;
                }
            }
            ConditionRecords();
            return ret;
        }

        protected void ConditionRecords()
        {
            ShiftsDM sdm = new ShiftsDM();
            RolesDM rdm = new RolesDM();
            ObjectList<ShiftsObject> shifts = sdm.FetchAll();
            ObjectList<RolesObject> roles = rdm.FetchAll();
            BatchImportDM dm = new BatchImportDM();
            GuidesDM ndm = new GuidesDM();
            foreach (BatchImportObject import in dm.FetchAll())
            {
                GuidesObject name = null;
                if (import.ID != String.Empty)
                {
                    name = ndm.FetchGuide(import.ID);
                    if (name != null)
                    {
                        import.ImportStatus = (int)ImportStatusValues.MatchFound;
                        import.ID = name.VolID;
                    }

                }
                if (import.ImportStatus == (int)ImportStatusValues.Imported)
                {
                    if (import.Email != String.Empty)
                    {
                        name = ndm.FetchByNameAndEmail(import.First, import.Last, import.Email);
                        if (name != null)
                        {
                            import.ID = name.VolID;
                            import.ImportStatus = (int)ImportStatusValues.MatchFound;
                        }
                        else
                        {
                            //if( ndm.FetchAllByEmail(import.Email).Count > 1)
                            //{
                            //    import.ID = 0;
                            //    import.ImportStatus = (int)ImportStatusValues.Conflict;
                            //}
                        }
                    }
                }

                if (import.ImportStatus == (int)ImportStatusValues.Imported)
                {
                    if (import.First != String.Empty && import.Last != String.Empty && import.Email != String.Empty)
                        import.ImportStatus = (int)ImportStatusValues.NewRecord;
                }
                if (!roles.Exists(x => x.RoleName == import.Role))
                    import.ImportStatus = (int)ImportStatusValues.UnknownRole;
                if (!shifts.Exists(x => x.ShortName == import.Shift))
                    import.ImportStatus = (int)ImportStatusValues.UnknownShift;
                dm.Update(import);
            }
        }

        public int Merge()
        {
            int cnt = 0;
            GuidesDM gdm = new GuidesDM();
            BatchImportDM dm = new BatchImportDM();
            foreach (BatchImportObject import in dm.FetchDecoded())
            {
                GuidesObject guide = new GuidesObject();
                if (import.ImportStatus == (int)ImportStatusValues.NewRecord)
                {
                    cnt++;
                    guide.VolID = import.ID;
                    guide.FirstName = import.First;
                    guide.LastName = import.Last;
                    guide.Email = import.Email;
                    guide.Phone = import.Phone;
                    guide.Cell = import.Cell;
                    if (String.IsNullOrEmpty(import.Phone) && !String.IsNullOrEmpty(import.Cell))
                        guide.CellPreferred = true;
                    guide.RoleID = import.RoleID;
                    guide.ShiftID = import.ShiftID;
                    guide.UpdateBy = UserSecurity.GetUserName();
                    guide.LastUpdate = DateTime.Now;
                    gdm.Save(guide);
                    
                    dm.Delete(import.ImportID);
                }
                if (import.ImportStatus == (int)ImportStatusValues.MatchFound)
                {
                    cnt++;
                    guide = gdm.FetchGuide(import.ID);
                    guide.RoleID = import.RoleID;
                    if (guide.ShiftID != import.ShiftID)
                    {
                        // Do what here?
                    }
                    guide.Phone = import.Phone;
                    guide.Email = import.Email;
                    guide.UpdateBy = UserSecurity.GetUserName();
                    guide.LastUpdate = DateTime.Now;
                    gdm.Update(guide);
                    dm.Delete(import.ImportID);
                }
            }
            return cnt;
        }
        public int Delete()
        {
            int cnt = 0;
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = new GuidesObject();
            BatchImportDM dm = new BatchImportDM();
            foreach (BatchImportObject import in dm.FetchDecoded())
            {
                if (import.ImportStatus == (int)ImportStatusValues.MatchFound)
                {
                    cnt++;
                    guide = gdm.FetchGuide(import.ID);
                    if (guide != null)
                        gdm.Delete(guide);
                    dm.Delete(import.ImportID);
                }
            }
            return cnt;
        }
        public int Inactive()
        {
            int cnt = 0;
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = new GuidesObject();
            BatchImportDM dm = new BatchImportDM();
            GuideSubstituteDM sdm = new GuideSubstituteDM();
            GuideDropinsDM ddm = new GuideDropinsDM();
            foreach (BatchImportObject import in dm.FetchDecoded())
            {
                if (import.ImportStatus == (int)ImportStatusValues.MatchFound)
                {
                    
                    guide = gdm.FetchGuide(import.ID);
                    if (guide != null)
                    {
                        cnt++;
                        sdm.DeleteAllForGuide(guide.GuideID);
                        ddm.Delete(guide.GuideID);
                    }
                }
            }
            return cnt;
        }
    }
}