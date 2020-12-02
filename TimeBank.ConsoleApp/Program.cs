using System;
using System.Collections.Generic;
using TimeBank.Bussines.UseCases;
using TimeBank.Core.Models;
using TimeBank.ConsoleApp.UserMenu;
using TimeBank.ConsoleApp.TokenMenu;
using TimeBank.ConsoleApp.ServicesMenu;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TimeBank.Core.DataAccess;

namespace TimeBank.ConsoleApp
{
    class Program
    {
        private static DataConfig _dataConfig;
        private static AdminManagement _adminMgm;

        static void Main(string[] args)
        {
            Console.WriteLine("CONSOLE TESTER TIME BANK APPLICATION\r");
            Console.WriteLine("------------------------------------\n");

            _dataConfig = DataConfig.GetDataAccess();
            ShowHomeMenu();
        }

        public static void ShowHomeMenu()
        {
            string[] validOptions = new string[] { "U", "T", "S" };
            List<string> Options = new List<string>();
            Options.AddRange(validOptions);
            string choice;

            Console.WriteLine("HOME MENU");
            Console.WriteLine("--------------------");

            do
            {
                Console.WriteLine("SELECT AN OPTION: \n");
                Console.WriteLine("U - USER ACTIONS"); //ok
                Console.WriteLine("T - TOKEN ACTIONS"); //ok
                Console.WriteLine("S - SERVICES ACTIONS");
                Console.WriteLine("C - CONFIG DATA ACCESS"); //ok

                choice = Console.ReadLine().ToUpper();
            }
            while (!Options.Contains(choice));

            switch (choice)
            {
                case "U":
                    UserActions.ShowUserMenu();
                    ShowHomeMenu();
                    break;
                case "T":
                    new TokenActions().ShowTokenMenu();
                    ShowHomeMenu();
                    break;
                case "S":
                    new ServicesActions().ShowServicesMenu();
                    ShowHomeMenu();
                    break;
                case "C":
                    _dataConfig.NewConnection();
                    ShowHomeMenu();
                    break;
            }
        }
    }
}
