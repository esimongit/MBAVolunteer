using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NQN.Core
{
     public  class ConnectionFactory
    {
              
        public static SqlConnection getNew()
        {
            NQNConnection CC = new NQNConnection("NQNDB");
            return CC.getConnection();
        }
         // Connect to another database.  Used in MemberVisit
         public static SqlConnection getNew(string server)
         {
             NQNConnection CC = new NQNConnection(server);
             return CC.getConnection();
         }
    }
   
    public class NQNConnection
    {
        SqlConnection conn = null;
 
        public NQNConnection(string server)
        {
            
            if (conn == null)
            {
                conn = new SqlConnection(getConnectionString(server));      
            }
        }

        public SqlConnection getConnection() {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

     
        public static string getConnectionString(string CString)
        {
            return ConfigurationManager.ConnectionStrings[CString].ToString();            
        }
        
    }
}
