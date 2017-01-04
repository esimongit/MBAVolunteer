using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Bus;

namespace VolManager
{
    public partial class GuideImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ImportButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string UploadPath = "C:\\Windows\\temp";
                string savePath = Path.Combine(UploadPath, FileUpload1.FileName);
                FileUpload1.SaveAs(savePath);

                BatchBusiness bb = new BatchBusiness();
                InfoMessage.Set(bb.Import(savePath));
                Repeater1.DataBind();

            }
        }
        protected void DoMerge(object sender, EventArgs e)
        {
            BatchBusiness bb = new BatchBusiness();
            InfoMessage.Set(String.Format("{0} Records imported successfully.", bb.Merge()));
            Repeater1.DataBind();
        }
        protected void DoDelete(object sender, EventArgs e)
        {
            BatchBusiness bb = new BatchBusiness();
            InfoMessage.Set(String.Format("{0} Records Deleted successfully.", bb.Delete()));
            Repeater1.DataBind();
        }
        protected void DoInactive(object sender, EventArgs e)
        {
            BatchBusiness bb = new BatchBusiness();
            InfoMessage.Set(String.Format("{0} Records made inactive.", bb.Inactive()));
            Repeater1.DataBind();
        }
    }
}