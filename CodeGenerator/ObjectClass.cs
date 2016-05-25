using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web;
using System.Text;


namespace CodeGenerator
{
    public class ObjectClass
    {
        TableInfo TIF;
        string ClassName;
        Types ColTypes;
        public void Generate(TableInfo tif, string FileName)
        {
            ColTypes = new Types();
            TIF = tif;
            ClassName= tif.TableName + "Object";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Header);
            BuildFile (sb);
            using (StreamWriter sw = new StreamWriter(FileName)) 
            {
                sw.Write(sb.ToString());
            }
        }
        void BuildFile(StringBuilder sb) {
            sb.AppendLine(String.Format("\tpublic class {0} : RootObject", ClassName));
            sb.AppendLine("\t{");
            sb.AppendLine("#region AutoAttributes");
            foreach (TableCols tc in TIF.Cols) {
                BuildField(sb, tc);
            }
            sb.AppendLine("#endregion");
            sb.AppendLine();
            // Constructor
            sb.AppendLine(String.Format("\t\tpublic {0}()", ClassName));
            sb.AppendLine("\t\t{");
            sb.AppendLine(String.Format("\t\t\t_tablename = \"{0}\";",TIF.TableName));
            sb.AppendLine(String.Format("\t\t\t_primarykey = \"{0}\";",TIF.PrimaryKey));
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
        }
        void BuildField (StringBuilder sb, TableCols tc) 
        {
            SysType stype = ColTypes.lst[tc.TypeID];
            sb.AppendLine(String.Format("\t\tprivate {0} _{1} = {2};", stype.CSName,
                tc.ColName.ToLower(), stype.DefaultVal));
            sb.AppendLine(String.Format("\t\tpublic {0} {1} ", stype.CSName,
                tc.ColName));
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tget");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine(String.Format("\t\t\t\treturn _{0};", tc.ColName.ToLower()));
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\tset");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine(String.Format("\t\t\t\t _{0} = value;", tc.ColName.ToLower()));          
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
        }

       
         private static string Header =
        @"using System;
using System.Data;
using System.Collections.Generic;
using NQN.Core;

namespace NQN.DB 
{";
    
    }
}
