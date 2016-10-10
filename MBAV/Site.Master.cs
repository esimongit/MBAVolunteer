using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;

namespace MBAV
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected static string ErrorMessageText = "";
        protected static string InfoMessageText = "";
        string GuideName = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            Label HeadLoginName = (Label)HeadLoginView.FindControl("HeadLoginName");
            if (GuideName == String.Empty)
            {  
                int GuideID = 0;
                try
                {
                    GuideID = Convert.ToInt32(Session["GuideID"]);
                }
                catch { }
                if (GuideID > 0)
                {
                    GuidesDM dm = new GuidesDM();
                    GuidesObject guide = dm.FetchGuide(GuideID);
                    GuideName = guide.GuideName;
                }
            }

            HeadLoginName.Text = GuideName;
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
