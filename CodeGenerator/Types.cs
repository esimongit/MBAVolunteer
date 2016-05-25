using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using NQN.Core;

namespace CodeGenerator
{
    public class Types
    {
        public Dictionary<int,SysType> lst = new Dictionary<int,SysType>();
        public Types()
        {
            // Decodes syscolumns.xtype
            lst.Add(34, new SysType(34, "image", "byte[]", "GetNullableBinary", "new Byte[0]"));
            lst.Add(35, new SysType(35, "text", "string", "GetNullableString", "String.Empty"));
            lst.Add(36, new SysType(36, "uniqueidentifier", "Guid", "GetNullableGuid", "new Guid()"));
            lst.Add(40, new SysType(40, "date", "DateTime", "GetNullableDateTime", "new DateTime()"));
            lst.Add(41, new SysType(41, "time", "DateTime", "GetNullableDateTime", "DateTime.Now"));
            lst.Add(52, new SysType(52, "smallint", "short", "GetNullableInt16", "0"));
            lst.Add(56, new SysType(56, "int", "int", "GetNullableInt32", "0"));
            lst.Add(58, new SysType(58, "smalldatetime", "DateTime", "GetNullableDateTime", "new DateTime()"));
            lst.Add(60, new SysType(60, "money", "decimal", "GetNullableDecimal", "0"));
            lst.Add(61, new SysType(61, "timestamp", "DateTime", "GetNullableDateTime", "new DateTime()"));
            lst.Add(62, new SysType(62, "float", "decimal", "GetNullableDecimal", "0"));
            lst.Add(63, new SysType(63, "numeric", "decimal", "GetNullableDecimal", "0"));
            lst.Add(104, new SysType(104, "bit", "bool", "GetNullableBoolean", "false"));
            lst.Add(106, new SysType(106, "decimal", "decimal", "GetNullableDecimal", "0"));
            lst.Add(111, new SysType(61, "datetime", "DateTime", "GetNullableDateTime", "new DateTime()"));
            lst.Add(122, new SysType(122, "smallmoney", "decimal", "GetNullableDecimal", "0"));
            lst.Add(165, new SysType(165, "varbinary", "byte[]", "GetNullableBinary", "new Byte[0]"));
            lst.Add(167, new SysType(167, "varchar", "string", "GetNullableString", "String.Empty"));
            lst.Add(175, new SysType(175, "char", "string", "GetNullableString", "String.Empty"));
            lst.Add(231, new SysType(231, "varchar", "string", "GetNullableString", "String.Empty"));
           
        }

    }
    public class SysType {
        public int id;
        public string DBName;
        public string CSName;
        public string Getter;
        public string DefaultVal;

        public SysType(int ID, string DB, string CS, string GT, string DEF) {
            id = ID;
            DBName = DB;
            CSName = CS;
            Getter = GT;
            DefaultVal = DEF;
        }
    }
}
