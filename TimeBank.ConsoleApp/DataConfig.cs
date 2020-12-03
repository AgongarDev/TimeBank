using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Core.DataAccess;
using System.Xml;
using TimeBank.Bussines.Utilities;
using System.IO;

namespace TimeBank.ConsoleApp
{
    public class DataConfig
    {
        private static DataConfig INSTANCE;
        private DataConfig()
        {
            AccesDataBase();
        }

        public static DataConfig GetDataAccess()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new DataConfig();
            }
            return INSTANCE;
        }
        private void AccesDataBase()
        {
            ConnectionData connectionData = LoadData();

            if (connectionData == null || String.IsNullOrWhiteSpace(connectionData.ConnectionString))
            {
                connectionData = NewConnection();
            }
            Database.InitDatabase(connectionData);

            while (!Database.IsServerConnected())
            {
                Console.WriteLine("Database Not Connected...\n");
                connectionData = NewConnection();
                Database.InitDatabase(connectionData);
            }
        }

        public ConnectionData NewConnection()
        {
            ConnectionData conn = new ConnectionData();

            Console.WriteLine("Insert Connection Data :\n");
            string input = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(input))
            {
                return conn;
            }

            Console.WriteLine("Confirm data? (Y/N)");
            string ok = Console.ReadLine().ToUpper();
            if (ok != "Y" || String.IsNullOrWhiteSpace(ok))
            {
                return conn;
            }

            conn.ConnectionString = input;
            Console.WriteLine("Keep session open? (Y/N)");
            Console.WriteLine("<< If not, next time you will need to provide a connection string >>");
            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    SaveData(new ConnectionData());
                    break;
                default:
                    SaveData(conn);
                    break;
            }
            return conn;
        }

        private static void SaveData(ConnectionData connectionData)
        {
            try
            {
                CommonLib.ToXml(connectionData, "dbconfig", "TimeBankDBConfig");
                Console.WriteLine("Session Data Updated");
            }
            catch (Exception)
            {
                Console.WriteLine("Unnable to save session data...");
            }
        }

        private static ConnectionData LoadData()
        {
            try
            {
                return CommonLib.FromXml<ConnectionData>("dbconfig", "TimeBankDBConfig");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
