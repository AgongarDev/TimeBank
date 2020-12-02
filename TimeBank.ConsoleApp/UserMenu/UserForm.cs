using System;
using System.Collections.Generic;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;
using TimeBank.ConsoleApp.UserMenu;

namespace TimeBank.ConsoleApp.UserMenu
{
    public class UserForm
    {
        private const int MAXDEBIT = 80;
        private const int MINCREDIT = 10;

        public static void NewUser(User user)
        {
            string insertData;

            Console.WriteLine("USER FORM");
            Console.WriteLine("---------");
            
            Console.WriteLine("NAME : " + user.Name);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                user.Name = insertData;
            }
            
            Console.WriteLine("LAST NAME : " + user.LastName);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                user.LastName = insertData;
            }
            
            Console.WriteLine("PASSWORD : ");
            insertData = Console.ReadLine();
            user.Password = insertData;
            
            Console.WriteLine("IS ADMIN : (Y/N) " + user.Admin);
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                if (insertData.ToUpper() == "Y")
                {
                    user.Admin = true;
                }
                else
                {
                    user.Admin = false;
                }
            }

            Console.WriteLine("MAX DEBIT HOURS :");
            insertData = Console.ReadLine();
            Wallet wallet = new Wallet();
            if (CommonLib.ValidateNumEntrance(insertData))
            {
                wallet.MaxDebit = int.Parse(insertData);
            }
            else
            {
                wallet.MaxDebit = MAXDEBIT;
            }
            wallet.MinCredit = MINCREDIT;
            wallet.Debit = new List<Token>();
            wallet.Credit = new List<Token>();
            user.Wallet = wallet;

            if (user.Address == null)
            {
                user.Address = new Address();
            }
            Console.WriteLine(user.Address.ToString());
            Console.WriteLine("CHANGE ADDRESS? (Y/N) ");
            string c = Console.ReadLine();
            if (c.ToUpper() == "Y")
            {
                user.Address = AddressForm.NewAddress(user.Address);
            }

            user.InDate = System.DateTime.Now;
            InsertOrUpdate(user);
        }
        private static void InsertOrUpdate(User user)
        {
            UserManagement _userMgm = UserManagement.GetInstance();
            if (_userMgm.InsertOrUpdate(user))
            {
                Console.WriteLine("TBankUser " + user.UserId + " : " + user.Name + " Updated");
            }
        }
    }
}