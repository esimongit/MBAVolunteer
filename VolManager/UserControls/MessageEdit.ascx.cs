using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.DB;

namespace VolManager
{
    public partial class MessageEdit : System.Web.UI.UserControl
    {
        public bool CanClone = false;

        public event EventHandler MessageChanged;

        protected void Form_PreRender(object sender, EventArgs e)
        {
            Panel clonetable = (Panel)FormView1.FindControl("CloneTable");
            if (clonetable == null)
                return;
            clonetable.Visible = CanClone;
            NQN.Controls.NQNButton CloneButton = (NQN.Controls.NQNButton)FormView1.FindControl("CloneButton");
            CloneButton.Visible = CanClone;
        }

        public void Setup()
        {
            //FormView1.DataBind();
        }
        protected virtual void OnMessageChanged(EventArgs e)
        {
            if (MessageChanged != null)
                MessageChanged(this, e);
        }
        public void CloneMessage(object sender, EventArgs e)
        {
            int MailTextID = 0;
            try
            {
                MailTextID = Convert.ToInt32(Session["MailTextID"]);
            }
            catch { }
            if (MailTextID == 0) return;
            TextBox NewSymbolTextBox = (TextBox) FormView1.FindControl("NewSymbolTextBox");
            string NewSymbol = NewSymbolTextBox.Text;
            if (NewSymbol == String.Empty)
            {
                ErrorMessage.Set("Please Enter a new Symbol");
                return;
            }
            MailTextDM dm = new MailTextDM();
            dm.Clone(MailTextID, NewSymbol);
            InfoMessage.Set("New Message Created");
            OnMessageChanged(new EventArgs());
        }
        protected void CatchDB(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);

                e.ExceptionHandled = true;
            }
            else
            {
                 
                InfoMessage.Set("Changes Accepted");
                OnMessageChanged(new EventArgs());
            }
        }
    }
}