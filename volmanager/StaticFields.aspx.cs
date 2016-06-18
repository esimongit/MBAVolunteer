using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace VolManager
{
    public partial class StaticFields : System.Web.UI.Page
    {
      
        protected void AddButton_Clicked(object sender, EventArgs e)
        {
            FormView2.Visible = true;
            TextBox SequenceTB = (TextBox)FormView2.FindControl("SequenceTextBox");
            SequenceTB.Text = "1";
        }
        protected void InsertCancel(object sender, EventArgs e)
        {
            FormView2.Visible = false;
        }
       
        protected void OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                
                e.ExceptionHandled = true;
            }
            else 
            {
                FormView2.Visible = false;
                NQNGridView1.DataBind();
            }

        }
        protected void OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);

                e.ExceptionHandled = true;
            }
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
