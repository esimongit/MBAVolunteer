using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace  NQN.Core
{
       
    [Serializable]
    public abstract class RootObject
    {
        public  DateTime SQLMinDate = DateTime.Parse("1/1/1753");
        public  DateTime SQLMaxDate = DateTime.Parse("1/1/9999");

        protected string _tablename;
        public string TableName
        {
            get { return _tablename; }
        }
        protected string _primarykey;
        public string PrimaryKey
        {
            get { return _primarykey; }
        }
        public virtual int PKVal()
        {
            return 0;
        }

        public string vardump(bool html = false)
        {
            string output = String.Empty;
            PropertyInfo[] srcinfo = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo prop in srcinfo)
            {
                try
                {
                    output += prop.Name + ": ";
                    output += prop.GetValue(this, null).ToString();
                    output += html ? "<br/>" : "\n";
                }
                catch { }
            }
            return output;
        }
    }
    public class KeyedObjectList<TEntity> : Dictionary<int, TEntity> where TEntity : RootObject
    {
        
    }
    [Serializable]
    public class ObjectList<TEntity> : List<TEntity> where TEntity : RootObject
    {

        public DataTable RenderAsTable()
        {
            if (Count == 0) return new DataTable();
            RootObject obj = this[0];
            DataTable dt = GetDataTable(obj);
            

            foreach ( RootObject root in this)
            {
                AddDataRow(dt, root);
            }
            return dt; 
        }
        protected DataTable GetDataTable(RootObject obj)
        {

            DataTable dt = new DataTable();

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();
            string pk = obj.PrimaryKey;
            foreach (PropertyInfo prop in props)
            {

                if (prop.Name == "TableName" || prop.Name == "PrimaryKey") continue;
                if (prop.Name == pk)
                {
                    DataColumn col = new DataColumn(prop.Name, prop.PropertyType);
                    dt.Columns.Add(col);
                    dt.PrimaryKey = new DataColumn[1] { col };
                }
                else
                {
                    dt.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
                }

            }
            return dt;

        }
        protected void AddDataRow(DataTable dt, RootObject obj)
        {
            DataRow row = dt.NewRow();
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "TableName" || prop.Name == "PrimaryKey") continue;
                row[prop.Name] = prop.GetValue(  obj, null);
            }
            dt.Rows.Add(row);

        }
    }
}
