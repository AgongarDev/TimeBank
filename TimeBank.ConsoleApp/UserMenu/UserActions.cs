using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.UserMenu
{
    public class UserActions
    {
        private static readonly UserManagement _userMgm = UserManagement.GetInstance();

        public static void ShowUserMenu()
        {
            string[] validOptions = new string[] { "I", "E", "R", "L", "P", "B" };
            List<string> Options = new List<string>();
            Options.AddRange(validOptions);
            string choice;

            Console.WriteLine("USER ACTIONS TESTER");
            Console.WriteLine("---------------------");

            do
            {
                Console.WriteLine("SELECT AN OPTION: \n");
                Console.WriteLine("I - INSERT USER");
                Console.WriteLine("E - EDIT USER");
                Console.WriteLine("R - REMOVE USER");
                Console.WriteLine("L - LIST USERS");
                Console.WriteLine("P - PRINT USERS TO XML");
                Console.WriteLine("B - BACK");

                choice = Console.ReadLine().ToUpper();
            }
            while (!Options.Contains(choice));

            User auxUser;
            switch (choice)
            {
                case "I":
                    UserForm.NewUser(new User());
                    ShowUserMenu();
                    break;
                case "E":
                    auxUser = GetUser();
                    UserForm.NewUser(auxUser);
                    ShowUserMenu();
                    break;
                case "R":
                    auxUser = GetUser();
                    RemoveUser(auxUser);
                    ShowUserMenu();
                    break;
                case "L":
                    ListUsers();
                    ShowUserMenu();
                    break;
                case "P":
                    PrintUsers();
                    ShowUserMenu();
                    break;
                case "B":
                    Program.ShowHomeMenu();
                    break;
            }
        }

        private static void ListUsers()
        {
            List<User> users = _userMgm.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("NOT FOUND USERS");
            }
            else
            {
                foreach (User user in users)
                {
                    Console.WriteLine(user.ToString());
                }
            }
        }

        private static void PrintUsers()
        {
            List<User> users = _userMgm.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("NOT FOUND USERS");
            }
            else
            {
                try
                {
                    CommonLib.ToXml(users, "xml", "Users");
                }
                catch (Exception)
                {
                    Console.WriteLine("Unnable to generate xml file");
                }
            }
        }

        private static void RemoveUser(User auxUser)
        {
            if (_userMgm.RemoveUser(auxUser))
            {
                Console.WriteLine("USER REMOVED");
            }
            else
            {
                Console.WriteLine("UNNABLE TO REMOVE USER : " + auxUser.UserId + "." + auxUser.Name);
            }
        }

        public static User GetUser()
        {
            User user = null;
            string o;
            Console.WriteLine("GET USER TEST");
            Console.WriteLine("-----------------");
            do
            {
                Console.WriteLine("INSERT EXISTING USER ID OR BLANK TO EXIT : ");
                o = Console.ReadLine().ToUpper();
                if (String.IsNullOrWhiteSpace(o))
                {
                    ShowUserMenu();
                    return null;
                }
                if (CommonLib.ValidateNumEntrance(o))
                {
                    user = _userMgm.GetUser(long.Parse(o));
                }
            } while (user == null);

            return user;
        }

    }
}
