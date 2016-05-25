using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace NQN.Core
{
    
    public abstract class DBAccess<T> : IDisposable where T : RootObject
    {
        protected string DB = "NQNDB";
         
        ~DBAccess()
        {
            Dispose();
        }
        #region IDisposable Support


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

       

        #endregion
        public virtual T FetchRecord(string pkname, int pkval)
        {
            if (pkval == 0 )
                throw new ArgumentException("Fetch called with null PK", "pkval");

            T obj = null;

            string sql  = ReadAllCommand() + String.Format(" WHERE {0} = @pkval",pkname);
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(sql, conn);
                myc.Parameters.Add(new SqlParameter("pkval", pkval));
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    obj = LoadFrom(reader);

                }
            }

            return obj;
        }
        // Add a "WHERE ..." qualification to the FetchAll
        public virtual ObjectList<T> Fetch(string qual)
        {

            ObjectList<T> dList = new ObjectList<T>();

            string sql = ReadAllCommand() + " " + qual;
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(sql, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    T obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        dList.Add(obj);
                        obj = LoadFrom(reader);
                    }

                }
            }

            return dList;
        }
        // Substitue COUNT(*) from the Select List and add the qual
        // JUst need to pass a non-null instance of the object
        public int FetchCount(T obj, string qual)
        {
            int cnt = 0;
            
            string sql = String.Format("SELECT CNT= COUNT(*) FROM {0} {1}",
                obj.TableName, qual);
          
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand(sql, conn);
                try
                {
                    cnt = (int)myc.ExecuteScalar();
                }
                catch { }
               
            }

            return cnt ;
        }
        public virtual ObjectList<T> FetchAll()
        {
           
            ObjectList<T> dList = new ObjectList<T>();

            string sql = ReadAllCommand();
            using (SqlConnection conn = ConnectionFactory.getNew(DB))
            {
                SqlCommand myc = new SqlCommand( sql, conn);
                using (SqlDataReader reader = myc.ExecuteReader())
                {
                    T obj = LoadFrom(reader);
                    while (obj != null)
                    {
                        dList.Add(obj);
                        obj = LoadFrom(reader);
                    }

                }
            }

            return dList;
        }
        protected virtual T LoadFrom(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
        protected virtual string ReadAllCommand()
        {
            throw new Exception("No ReadAll Command has been provided ");
        }

        

      /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public  bool GetNullableBoolean(SqlDataReader reader, string columnName, Boolean defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (bool)reader.GetBoolean(reader.GetOrdinal(columnName));
        }
       
       
        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  Byte? GetNullableByte(SqlDataReader reader, string columnName, Byte defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (byte)reader.GetByte(reader.GetOrdinal(columnName));
        }
      
       
        /// <summary>
        /// Gets the nullable binary.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  Byte[] GetNullableBinary(SqlDataReader reader, string columnName, Byte[] defaultValue)
        {
            if (reader == null)
                return defaultValue;
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return defaultValue;
            long length = reader.GetBytes(reader.GetOrdinal(columnName), 0, null, 0, 0);
            if (length == 0)
                return new Byte[0];
            Byte[] buffer = new Byte[length];
            reader.GetBytes(reader.GetOrdinal(columnName), 0, buffer, 0, (int)length);
            return buffer;
        }       
      
        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  char GetNullableChar(SqlDataReader reader, string columnName, Char defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (char)reader.GetChar(reader.GetOrdinal(columnName));
        }
        
       
        /// <summary>
        /// Gets the nullable char array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  Char[] GetNullableCharArray(SqlDataReader reader, string columnName, Char[] defaultValue)
        {
            if (reader == null)
                return defaultValue;
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return defaultValue;
            long length = reader.GetChars(reader.GetOrdinal(columnName), 0, null, 0, 0);
            if (length == 0)
                return new Char[0];
            Char[] buffer = new Char[length];
            reader.GetChars(reader.GetOrdinal(columnName), 0, buffer, 0, (int)length);          
            return buffer;
        }


        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  DateTime GetNullableDateTime(SqlDataReader reader, string columnName, DateTime defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (DateTime)reader.GetDateTime(reader.GetOrdinal(columnName));
        }
        /// <summary>
        /// Gets the nullable time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public TimeSpan GetNullableTimeSpan(SqlDataReader reader, string columnName, TimeSpan defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (TimeSpan)reader.GetTimeSpan(reader.GetOrdinal(columnName));
        }
        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  decimal GetNullableDecimal(SqlDataReader reader, string columnName, Decimal defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (decimal)reader.GetDecimal(reader.GetOrdinal(columnName));
        }
       
     
        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  double GetNullableDouble(SqlDataReader reader, string columnName, Double defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (double)reader.GetDouble(reader.GetOrdinal(columnName));
        }
       
        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  Guid GetNullableGuid(SqlDataReader reader,string columnName,Guid defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue :(Guid)reader.GetGuid(reader.GetOrdinal(columnName));
        }
     
    

        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  short GetNullableInt16(SqlDataReader reader, string columnName, Int16 defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue :(short) reader.GetInt16(reader.GetOrdinal(columnName));
        }



        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  int GetNullableInt32(SqlDataReader reader, string columnName, Int32 defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (int)reader.GetInt32(reader.GetOrdinal(columnName));
        }

        public Int64 GetNullableInt64(SqlDataReader reader, string columnName, Int64 defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (int)reader.GetInt64(reader.GetOrdinal(columnName));
        }


        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public  string GetNullableString(SqlDataReader reader, string columnName, String defaultValue)
        {
            return (reader == null || reader.IsDBNull(reader.GetOrdinal(columnName))) ? defaultValue : (string)reader.GetString(reader.GetOrdinal(columnName));
        }
        public  string SearchString(string TextField)
        {
            if (TextField == null || TextField == String.Empty) return String.Empty;
            string pattern = HttpUtility.UrlDecode(TextField.Trim());
            pattern = pattern.Replace("\"", "");
            pattern = pattern.Replace("'", "''");
            //Restore this if we start using fulltext search
            // string plower = pattern.ToLower();
            //if ((pattern.Contains(" ") || pattern.Contains("*") || pattern.Contains(",")) && !plower.Contains(" and ") && !plower.Contains(" or ") && !plower.Contains(" near "))
            //{
            //    pattern = '"' + pattern + '"';
            //}
            return pattern;
        }
         
    }
}
