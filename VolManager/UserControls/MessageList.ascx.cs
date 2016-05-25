using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VolManager
{
    public partial class MessageList : System.Web.UI.UserControl
    {
        public event EventHandler MessageSelected;

        public void ReBind()
        {
            MailSelect.DataBind();
        }
        protected void MailSelected(object sender, EventArgs e)
        {
            Session["MailTextID"] = MailSelect.SelectedValue;
            OnMessageSelected(new EventArgs());
        }
        protected virtual void OnMessageSelected(EventArgs e)
        {
            if (MessageSelected != null)
                MessageSelected(this, e);
        }
    }
}