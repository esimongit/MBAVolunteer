using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace VolManager
{
    public partial class TextEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                MultiView1.SetActiveView(View1);

        }
        protected void MailSelected(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
             
        }
        protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
            
        }
        protected void RebindList(object sender, EventArgs e)
        {
            MessageList1.ReBind();

        }
        protected void OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
           
            }

            
        }
           
        
    }
}
