using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web;
using System.Text;

namespace CodeGenerator
{
    public class DMClass
    {
        TableInfo TIF;
        string ClassName;
        string ObjectName;
        Types ColTypes;
        string ExternalLink = String.Empty;
        public void Generate(TableInfo tif, string FileName)
        {
            object externalDB = ConfigurationManager.AppSettings["ExternalDB"];
            if (externalDB != null)
                ExternalLink = externalDB.ToString();

            ColTypes = new Types();
            TIF = tif;
            ClassName = tif.TableName + "DM";
            ObjectName = tif.TableName + "Object";
            StringBuilder sb = new StringBuilder();
            sb.Append(Header);
            BuildFile(sb);
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(sb.ToString());
            }
        }
        void BuildFile(StringBuilder sb)
        {
            sb.AppendLine(String.Format("\n\tpublic class {0} : DBAccess<{1}>", 
                ClassName, ObjectName));
            sb.AppendLine("\t{");
            if (ExternalLink != String.Empty)
            {
                sb.AppendLine(String.Format("\t\tpublic {0}()", ClassName));
                sb.AppendLine("\t\t{");
                sb.AppendLine(String.Format("\t\t\tDB=\"{0}\";", ExternalLink));
                sb.AppendLine("\t\t}\n");
                ExternalLink = "DB";
            }
            BuildUpdate(sb);
            BuildSave(sb);
            BuildDelete(sb);
            BuildLoadFrom(sb);
            BuildSelectAll(sb);
            BuildGetLast(sb);
            sb.AppendLine("\t}");
            sb.AppendLine("}");
        }
        // Insert statement assumes PK is identity
        void BuildSave(StringBuilder sb)
        {
            sb.AppendLine(String.Format("\t\tpublic void Save({0} obj)", ObjectName));
            sb.AppendLine("\t\t{");
             sb.AppendLine(String.Format("\t\t\t string qry = @\"INSERT INTO {0} (",TIF.TableName));
            string sep = "";
            foreach (TableCols tc in TIF.Cols)
            {   
                if (tc.IsPK) continue;
                sb.AppendLine(String.Format("\t\t\t\t{0}[{1}]",sep,tc.ColName));
                sep = ",";
            }
            sb.AppendLine("\t\t\t\t)");
            sb.AppendLine("\t\t\tVALUES(");
            sep = "";
            foreach (TableCols tc in TIF.Cols)
            {   
                if (tc.IsPK) continue;
                sb.AppendLine(String.Format("\t\t\t\t{0}@{1}",sep,tc.ColName));
                sep = ",";
            }
             sb.AppendLine("\t\t\t\t)\";");
            sb.AppendLine(String.Format("\t\t\t using (SqlConnection conn = ConnectionFactory.getNew({0}))", ExternalLink));
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tSqlCommand myc = new SqlCommand(qry, conn);");
            foreach (TableCols tc in TIF.Cols)
            { 
                if (tc.IsPK) continue;
                sb.AppendLine(String.Format("\t\t\t\tmyc.Parameters.Add(new SqlParameter(\"{0}\",obj.{0}));",
                    tc.ColName));             
                
            }
            sb.AppendLine("\t\t\t\tmyc.ExecuteNonQuery();");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
        }
        // Update statement assumes PK is identity
        void BuildUpdate(StringBuilder sb)
        {
            sb.AppendLine(String.Format("\t\tpublic void Update({0} obj)", ObjectName));
            sb.AppendLine("\t\t{");
            sb.AppendLine(String.Format("\t\t\t string qry = @\"UPDATE  {0} SET ", TIF.TableName));
            string sep = "";
          
            foreach (TableCols tc in TIF.Cols)
            {
                if (tc.IsPK) continue;
                sb.AppendLine(String.Format("\t\t\t\t{0}{1}=@{1}", sep, tc.ColName));
                sep = ",";
            }
            sb.AppendLine(String.Format("\t\t\t\t WHERE {0} = @{0}\";", TIF.PrimaryKey));
            sb.AppendLine(String.Format("\t\t\t using (SqlConnection conn = ConnectionFactory.getNew({0}))", ExternalLink));
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tSqlCommand myc = new SqlCommand(qry, conn);");
            foreach (TableCols tc in TIF.Cols)
            {
                sb.AppendLine(String.Format("\t\t\t\tmyc.Parameters.Add(new SqlParameter(\"{0}\",obj.{0}));",
                    tc.ColName));

            }
            sb.AppendLine("\t\t\t\tmyc.ExecuteNonQuery();");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
        }
        void BuildDelete(StringBuilder sb)
        {
            sb.AppendLine("\t\tpublic void Delete(int pkey)");
            sb.AppendLine("\t\t{");
            if (TIF.HasPK) 
            {
                sb.AppendLine(String.Format("\t\t\tstring qry = @\"DELETE FROM {0} WHERE [{1}] = @{1}\";",
                    TIF.TableName, TIF.PrimaryKey, TIF.PrimaryKey));
            } 
            else 
            {
                sb.AppendLine("\t\t\tTODO:  No Primary Key");
            }
            sb.AppendLine(String.Format("\t\t\t using (SqlConnection conn = ConnectionFactory.getNew({0}))", ExternalLink));
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tSqlCommand myc = new SqlCommand(qry, conn);");
            sb.AppendLine(String.Format("\t\t\t\tmyc.Parameters.Add(new SqlParameter(\"{0}\",pkey));", 
                TIF.PrimaryKey));
            sb.AppendLine("\t\t\t\t myc.ExecuteNonQuery();");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
        }

        void BuildLoadFrom(StringBuilder sb)
        {
            sb.AppendLine(String.Format("\t\tprotected override {0} LoadFrom(SqlDataReader reader)",
                ObjectName));
            sb.AppendLine("\t\t{");

            sb.AppendLine("\t\t\tif (reader == null) return null;");
            sb.AppendLine("\t\t\tif (!reader.Read()) return null;");
            sb.AppendLine(String.Format("\t\t\t{0} obj = new {0}();", ObjectName));
            foreach (TableCols tc in TIF.Cols)
            {
                SysType stype = ColTypes.lst[tc.TypeID];
                sb.AppendLine(String.Format("\t\t\tobj.{0} = {1}(reader, \"{0}\",{2});",
                    tc.ColName, stype.Getter, stype.DefaultVal));
            }
            sb.AppendLine("\t\t\treturn obj;");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
        }
        void BuildGetLast(StringBuilder sb)
        {
            sb.AppendLine("\t\tpublic int GetLast()");
            sb.AppendLine("\t\t{");
            sb.AppendLine(String.Format("\t\t\tstring qry = \"SELECT IDENT_CURRENT('{0}')\";", TIF.TableName));
            sb.AppendLine(String.Format("\t\t\t using (SqlConnection conn = ConnectionFactory.getNew({0}))", ExternalLink));
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tSqlCommand myc = new SqlCommand(qry, conn);");
            sb.AppendLine("\t\t\t\treturn  Convert.ToInt32( myc.ExecuteScalar());");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
        }
        void BuildSelectAll(StringBuilder sb)
        {
             sb.AppendLine("\t\tprotected override string ReadAllCommand()");
             sb.AppendLine("\t\t{");
             sb.AppendLine("\t\t\treturn @\"");
             sb.AppendLine("\t\t\tSELECT");
            string sep = "";
            foreach (TableCols tc in TIF.Cols)
            {
                sb.AppendLine(String.Format("\t\t\t\t{0}[{1}]", sep, tc.ColName));
                sep = ",";
            }
            sb.AppendLine(String.Format("\t\t\t\tFROM {0} \";", TIF.TableName));
            sb.AppendLine("\t\t}");
        }
        static string Header = @"
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NQN.Core;

namespace  NQN.DB
{";
    }
}
