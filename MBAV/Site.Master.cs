using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBAV
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected static string ErrorMessageText = "";
        protected static string InfoMessageText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
             
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
        
    }
}
