using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace VolManager
{
    public partial class Announcement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MultiView1.SetActiveView(View1);
            }
        }
        protected void ToView1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }
        protected void ToView2(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }
        protected void ImportButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string UploadPath = "C:\\Windows\\temp";
                string savePath = Path.Combine(UploadPath, FileUpload1.FileName);
                FileUpload1.SaveAs(savePath);
                FileBusiness fb = new FileBusiness();
                fb.Import(FileUpload1.FileName); 
                GridView1.DataBind();
                Repeater1.DataBind();

            }
        }
        protected void OnChange(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception.GetBaseException();
                ErrorMessage.Set(ex.Message);
                e.ExceptionHandled = true;
            }
            GridView1.DataBind();
            Repeater1.DataBind();
        }
        }
}