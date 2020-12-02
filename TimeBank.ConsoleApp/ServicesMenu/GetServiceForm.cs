using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.ConsoleApp.UserMenu;
using TimeBank.Core.Enums;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.ServicesMenu
{
    class GetServiceForm
    {
        private static AdminManagement _adminMgm = AdminManagement.GetInstance();
        public static void GetService()
        {
            Service service = new Service();
            string insertData;

            Console.WriteLine("GET A SERVICE TESTER");
            Console.WriteLine("--------------------");

            User user = UserActions.GetUser();
            if (user == null)
            {
                user = new User();
                UserForm.NewUser(user);
            }

            Category cat = ChooseCategory();

            ListServices(cat);

            Console.WriteLine("CHOOSE SERVICE : ");
            insertData = Console.ReadLine();
            if (CommonLib.ValidateNumEntrance(insertData))
            {
                try
                {
                    service = _adminMgm.GetService(long.Parse(insertData));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    service = _adminMgm.GetService(insertData);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            if (service != null)
            {
                _adminMgm.NewValidation(user, service);
                Console.WriteLine("Service adquired");
            }
        }



        public static void ListServices(Category cat)
        {
            List<Service> services;
            if (cat == null)
            {
                services = _adminMgm.GetAllServices();
            }
            else
            {
                services = _adminMgm.GetAllServices(cat);
            }
            foreach (Service s in services)
            {
                Console.WriteLine(s.ServiceID + " : " + s.Name);
            }

        }

        public static Category ChooseCategory()
        {
            string insertData;
            ServicesActions.ListCategories();
            Category cat = null;
            bool ok = false;
            do
            {
                Console.WriteLine("CHOOSE CATEGORY : ");
                insertData = Console.ReadLine();
                if (CommonLib.ValidateNumEntrance(insertData))
                {
                    cat = _adminMgm.GetCategory(int.Parse(insertData));
                }
                else
                {
                    cat = _adminMgm.GetCategory(insertData);
                }
                if (cat != null)
                {
                    return cat;
                }
                Console.WriteLine("GET SERVICES FROM ALL CATEGORIES ? (Y/N)");
                insertData = Console.ReadLine();
                if (insertData.ToUpper() != "N")
                {
                    ok = true;
                }
            } while (!ok);
            return null;
        }
    }
}
