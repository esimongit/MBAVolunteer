﻿using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;
using NQN.Core;

namespace MBAV
{
    public partial class Special : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int GuideID = Convert.ToInt32(Session["GuideID"]);
                if (GuideID == 0)
                    return;
                GuidesDM dm = new GuidesDM();
                GuidesObject guide = dm.FetchGuide(GuideID);
                int RoleID = Convert.ToInt32(Session["RoleID"]);
                if (RoleID == 0)
                    RoleLabel.Text = guide.RoleName;
                else
                {
                    RolesDM rdm = new RolesDM();
                    RolesObject role = rdm.FetchRecord("RoleID", RoleID);
                    RoleLabel.Text = role.RoleName;
                }
            }
        }
        protected void SpecialShiftChanged(object sender, EventArgs e)
        {
            GuideDropinsDM dm = new GuideDropinsDM();
            int GuideID = Convert.ToInt32(Session["GuideID"]);
            if (GuideID == 0)
                return;
            GuidesDM gdm = new GuidesDM();
            GuidesObject guide = gdm.FetchGuide(GuideID);
            int RoleID = Convert.ToInt32(Session["RoleID"]);
            if (RoleID == 0)
                RoleID = guide.RoleID;
            CheckBox cb = (CheckBox)sender;
            HiddenField hf = (HiddenField)(cb.Parent.FindControl("ShiftIDHidden"));
            int ShiftID = Convert.ToInt32(hf.Value);
            if (cb.Checked)
                cb.Checked = dm.AddSpecial(GuideID, ShiftID, RoleID);
            else
            {
                dm.DeleteSpecial(GuideID, ShiftID);
                cb.Checked = false;
            }
            cb.Focus();
            InfoMessage.Set("Changes Accepted");
            SpecialRepeater.DataBind();
        }
    }
}