using System;
using System.Data;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NQN.Core;


namespace CodeGenerator
{
    public class FileGenerator
    {
        TableInfo tif;
        string ObjFileName;
        string DMFileName;
        public FileGenerator(string TableName)
        {
            tif = new TableInfo(TableName);
        }

        public void Generate(string objClass, string dmClass,
            string outputFolder)
        {
            ObjFileName = outputFolder + "\\" + objClass + ".cs";
            DMFileName = outputFolder + "\\" + dmClass + ".cs";
            ObjectClass oc = new ObjectClass();
            oc.Generate( tif, ObjFileName);
            DMClass dc = new DMClass();
            dc.Generate(tif, DMFileName);

        }
       
        public string Display() {
            if (!File.Exists(ObjFileName))
            {
                return String.Format("{0} does not exist.", ObjFileName);
            }
            StringBuilder sb= new StringBuilder(ObjFileName);
            sb.Append(":<br/><br/>");
            string input;
            using (StreamReader sr = File.OpenText(ObjFileName))
            {
                while (( input = sr.ReadLine()) != null)
                {
                    sb.Append(input.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;") + "<br/>");
                }
                
                sr.Close();
            }
             if (!File.Exists(DMFileName))
            {
                return String.Format("{0} does not exist.", DMFileName);
            }
            sb.Append(String.Format("<br/><br/>{0}:<br/><br/>", DMFileName));
            using (StreamReader sr = File.OpenText(DMFileName))
            {
                while (( input = sr.ReadLine()) != null)
                {
                    sb.Append(input.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;") + "<br/>");
                }
                
                sr.Close();
            }
            return sb.ToString();
        }

    }
}
