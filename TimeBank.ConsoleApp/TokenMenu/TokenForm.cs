using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.TokenMenu
{
    class TokenForm
    {
        public static void NewToken()
        {
            Token token = new Token();
            string insertData;

            Console.WriteLine("TOKEN FORM");
            Console.WriteLine("---------");

            Console.WriteLine("NAME : ");
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                token.Name = insertData;
            }

            Console.WriteLine("HOURS VALUE : ");
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                int n = CommonLib.ValidateNumEntrance(insertData) ? int.Parse(insertData) : 1;
                token.Hours = n;
            }
            
            InsertOrUpdate(token);
        }
        private static void InsertOrUpdate(Token token)
        {
            AdminManagement _adminMgm = AdminManagement.GetInstance();
            if (_adminMgm.InsertOrUpdate(token))
            {
                Console.WriteLine("Token " + token.ID + " : " + token.Name + " Insert");
            }
        }
    }
}
