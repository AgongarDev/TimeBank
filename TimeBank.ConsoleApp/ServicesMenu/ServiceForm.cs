using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.ServicesMenu
{
    class ServiceForm
    {
        private readonly static AdminManagement _adminMgm = AdminManagement.GetInstance();
        public static void NewService()
        {
            Service service = new Service();
            string insertData;

            Console.WriteLine("SERVICE FORM");
            Console.WriteLine("------------");

            Console.WriteLine("NAME : ");
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                service.Name = insertData;
            }

            Console.WriteLine("DESCRIPTION : ");
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                service.Description = insertData;
            }

            List<Category> cats = _adminMgm.GetCategories();
            foreach (Category c in cats)
            {
                Console.WriteLine(c.ToString());
            }
            Console.WriteLine("CATEGORY : ");
            insertData = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(insertData))
            {
                service.Description = insertData;
            }

            User userProv = null;
            do
            {
                Console.WriteLine("USER PROVIDER : ");
                insertData = Console.ReadLine();
                long n = CommonLib.ValidateNumEntrance(insertData) ? (long.Parse(insertData)) : 1L;
                userProv = UserManagement.GetInstance().GetUser(n);
                if (userProv != null)
                {
                    service.ProviderID = n;
                    service.Provider = userProv;
                }

            } while (userProv == null);

            Token token = null;
            bool ok = false;
            Console.WriteLine("PRICE : ");
            do
            {
                List<Token> tokens = _adminMgm.GetTokens();
                foreach (Token t in tokens)
                {
                    Console.WriteLine(t.ToString());
                }

                Console.WriteLine("INSERT TOKEN ID : ");
                insertData = Console.ReadLine();
                int n = CommonLib.ValidateNumEntrance(insertData) ? (int.Parse(insertData)) : 1;
                token = _adminMgm.GetTokenById(n);
                if (token != null)
                {
                    Console.WriteLine("INSERT QUANTITY OF TOKENS " + token.Name);
                    insertData = Console.ReadLine();
                    int q = CommonLib.ValidateNumEntrance(insertData) ? (int.Parse(insertData)) : 1;
                    
                    if (service.Price == null)
                    {
                        service.Price = new List<Token>();
                    }

                    for (int i = 0; i <= q; i++)
                    {
                        service.Price.Add(token);
                    }
                }
                Console.WriteLine("WANT TO ADD MORE TOKENS TO THE SERVICE ? (Y/N)");
                insertData = Console.ReadLine();
                if (insertData.ToUpper() == "Y")
                {
                    ok = false;
                }
                else ok = true;

            } while (!ok);

            InsertOrUpdate(userProv, service);
        }
        private static void InsertOrUpdate(User user, Service s)
        {
            try
            {
                _adminMgm.InsertOrUpdate(s);
                _adminMgm.InsertOrUpdate(user);
                Console.WriteLine("Service " + s.Name + " of " + s.Provider.Name + " Updated");
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}