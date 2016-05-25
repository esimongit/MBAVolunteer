using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using NQN.Bus;
using NQN.DB;

namespace VolManager
{

    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected static string ErrorMessageText = "";
        protected static string InfoMessageText = "";
        /* Needed to preserve the TreeView expansion state */


        public void GetMenu()
        {
            if (Menu1.Items.Count == 0)
            {
                
                if (Session["MenuItems"] == null)
                {
                    NavHierarchy nh = new NavHierarchy();
                    if (Session["MenuID"] != null)
                        Session["MenuItems"] = (object)nh.BuildMenu(Convert.ToInt32(Session["MenuID"]));
                    //else
                    //    Response.Redirect("~/Account/Login.aspx");
                }
                if (Session["MenuItems"] != null)
                {
                    foreach (MenuItem itm in (MenuItemCollection)Session["MenuItems"])
                    {
                        Menu1.Items.Add(itm);
                    }
                }
            }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            ErrorMessageText = ErrorMessage.Get();
            InfoMessageText = InfoMessage.Get();
            ErrorMessageLabel.Visible = false;
            InfoMessageLabel.Visible = false;

            if (ErrorMessageText.Length > 0)
            {
                ErrorMessageLabel.Visible = true;
                ErrorMessageLabel.Text = ErrorMessageText;
                ErrorMessage.Clear();
                ErrorMessageText = "";
            }
            if (InfoMessageText.Length > 0)
            {
                InfoMessageLabel.Visible = true;
                InfoMessageLabel.Text = InfoMessageText;
                InfoMessage.Clear();
                InfoMessageText = "";
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (!(Page.IsCrossPagePostBack || Page.IsPostBack))
            {
                InfoMessage.Clear();
                ErrorMessage.Clear();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Role"] == null)
            //    Response.Redirect("~/Account/Login.aspx");
            if (ProgramLabel.Text == String.Empty)
                ProgramLabel.Text = "MBA Guide Substitute Management";
                   
            GetMenu();
            NavHierarchy nh = new NavHierarchy();
            string sel = Request.AppRelativeCurrentExecutionFilePath;
            

            string txt = "";

            MenuItem tn = nh.FindNodeByLink(sel, Menu1.Items, ref txt);
            // Don't allow access to restricted pages

            if (tn != null)
            {

                //PagePrivilege pp = PagePrivilege.PagePrivilegeFactory();

                //if (pp.HasPriv(tn.Value))
                //{
                tn.Selected = true;
                string title = txt.Replace("Menu/", "");
                TitleLabel.Text = title;
                //}
                //else
                //{
                //    Response.Redirect("~/Default.aspx");
                //}

            }
            else
                TitleLabel.Text = "";
        }

        protected void ClearSession(object sender, EventArgs e)
        {
            Session.Clear();

        }
    }
}
