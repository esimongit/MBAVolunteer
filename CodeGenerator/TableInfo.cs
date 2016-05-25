using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using NQN.Core;

namespace CodeGenerator
{
    public class TableInfo
    {
        public string TableName;
        public bool HasPK = false;
        public string PrimaryKey = String.Empty;
        public List<TableCols> Cols = new List<TableCols>();

        public TableInfo(string tname)
        {
            TableName = tname;
            GetPK();
            GetCols();
        }

        // return a list of all user tables
        public static List<string> UserTables()
        {
            List<string> tables = new List<string>();
            string qry = @"SELECT name from sysobjects where type = 'U' order by 1";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);

                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add((string)reader.GetString(0));
                    }
                }
            }
            return tables;
        }

        protected void GetPK() 
        {
            string qry = @"select c.name from syscolumns c 
            join sys.index_columns ic on object_id = c.id and ic.column_id = c.colid
            join sys.indexes i on i.object_id = c.id and i.is_primary_key = 1
            and i.index_id= ic.index_id
            where c.id =object_id(@TableName)";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("TableName", TableName));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PrimaryKey = (string)reader.GetString(0);
                        HasPK = true;
                    }
                    if (reader.Read())
                    {
                        PrimaryKey = String.Empty;
                        HasPK = false;
                    }
                }
            }
        }
        protected void GetCols()
        {
            string qry = @"SELECT  cast(xtype as int), name from syscolumns
                where id= object_id (@TableName) order by colid";
            using (SqlConnection conn = ConnectionFactory.getNew())
            {
                SqlCommand myc = new SqlCommand(qry, conn);
                myc.Parameters.Add(new SqlParameter("TableName", TableName));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableCols tc = new TableCols((int)reader.GetInt32(0),
                             (string)reader.GetString(1));
                        if (HasPK && tc.ColName == PrimaryKey)
                        {
                            tc.IsPK = true;
                        }
                        Cols.Add(tc);
                    }
                }
            }
        }
    }
    public class TableCols
    {
        public int TypeID;
        public string ColName;
        public bool IsPK = false;
        public TableCols(int id, string name)
        {
            TypeID = id;
            ColName = name;
        }
    }
}
