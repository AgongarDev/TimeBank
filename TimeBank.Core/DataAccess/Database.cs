using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.DataAccess
{
    public class Database
    {
        private static string m_ConnectionString;

        public static void InitDatabase(ConnectionData connectionData)
        {
            if (!String.IsNullOrEmpty(m_ConnectionString))
            {
                return;
            }
            m_ConnectionString = connectionData.ConnectionString;
        }

        public static string GetDatabaseConnection()
        {
            return m_ConnectionString;
        }

        public static bool IsServerConnected()
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
