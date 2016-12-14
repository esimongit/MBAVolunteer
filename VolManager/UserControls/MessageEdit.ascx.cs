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

        

        public void Setup()
        {
            //FormView1.DataBind();
        }
        protected virtual void OnMessageChanged(EventArgs e)
        {
            if (MessageChanged != null)
                MessageChanged(this, e);
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