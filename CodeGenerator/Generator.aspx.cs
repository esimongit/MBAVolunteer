using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
 

namespace CodeGenerator
{
    public partial class Generator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.DataSource = TableInfo.UserTables();
                DropDownList1.DataBind();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileGenerator cg = new FileGenerator(DropDownList1.SelectedValue);
            cg.Generate(ObjectClassTextBox.Text, DMClassTextBox.Text,
                FolderTextBox.Text);

            Label1.Text = cg.Display();
           
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectClassTextBox.Text = DropDownList1.SelectedValue + "Object";
            DMClassTextBox.Text = DropDownList1.SelectedValue + "DM";
            FolderTextBox.Text = "E:\\MBAVolunteer\\DB";
        }

      
    }
}
