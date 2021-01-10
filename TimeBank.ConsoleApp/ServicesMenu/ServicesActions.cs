using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.ConsoleApp.UserMenu;
using TimeBank.Core.Enums;
using TimeBank.Core.Models;

namespace TimeBank.ConsoleApp.ServicesMenu
{
    class ServicesActions
    {
        private readonly AdminManagement _adminMgm = AdminManagement.GetInstance();

        public void ShowServicesMenu()
        {
            string[] validOptions = new string[] { "P", "R", "G", "V", "I", "S", "L", "*", "B" };
            List<string> Options = new List<string>();
            Options.AddRange(validOptions);
            string choice;

            Console.WriteLine("SERVICES ACTIONS TESTER");
            Console.WriteLine("-----------------------");

            do
            {
                Console.WriteLine("SELECT AN OPTION: \n");
                Console.WriteLine("P - PUBLISH SERVICE"); //ok
                Console.WriteLine("T - CREATE NEW CATEGORY (TO DO)"); //not implemented
                Console.WriteLine("R - REMOVE SERVICE"); //ok
                Console.WriteLine("G - GET SERVICE"); //ok
                Console.WriteLine("V - VALIDATE SERVICE"); //ok
                Console.WriteLine("I - GENERATE INCIDENCE"); //ok
                Console.WriteLine("S - SOLVE INCIDENCE"); //ok
                Console.WriteLine("L - LIST SERVICES BY USER"); //ok
                Console.WriteLine("* - LIST ALL SERVICES"); //ok
                Console.WriteLine("C - LIST ALL CATEGORIES"); //ok
                Console.WriteLine("B - BACK");

                choice = Console.ReadLine().ToUpper();
            }
            while (!Options.Contains(choice));

            Service aux = null;
            switch (choice)
            {
                case "P":
                    ServiceForm.NewService();
                    ShowServicesMenu();
                    break;
                case "R":
                    aux = GetService();
                    if (aux != null)
                    {
                        RemoveService(aux);
                    }
                    ShowServicesMenu();
                    break;
                case "G":
                    GetServiceForm.GetService();
                    ShowServicesMenu();
                    break;
                case "V":
                    ValidateService();
                    ShowServicesMenu();
                    break;
                case "I":
                    GenerateIncidence();
                    ShowServicesMenu();
                    break;
                case "S":
                    SolveIncidence();
                    ShowServicesMenu();
                    break;
                case "L":
                    PrintOfferedServices();
                    ShowServicesMenu();
                    break;
                case "*":
                    Category cat = GetServiceForm.ChooseCategory();
                    GetServiceForm.ListServices(cat); 
                    ShowServicesMenu();
                    break;
                case "C":
                    ListCategories();
                    ShowServicesMenu();
                    break;
                default:
                    ShowServicesMenu();
                    break;
            }
        }

        internal static void ListCategories()
        {
            List<Category> cats = AdminManagement.GetInstance().GetCategories();
            foreach (Category c in cats)
            {
                Console.WriteLine(c.ID + " : " + c.Name);
            }
        }

        private void PrintOfferedServices()
        {
            User user = UserActions.GetUser();
            List<Service> services = _adminMgm.GetAllServices(user);
            foreach (Service s in services)
            {
                Console.WriteLine(s.ToString());
            }
        }

        private void SolveIncidence()
        {
            Console.WriteLine("INCIDENCES SOLVER TESTER");
            Console.WriteLine("------------------------");
            Console.WriteLine("INSERT INCIDENCE ID");
            string id = Console.ReadLine();
            if (CommonLib.ValidateNumEntrance(id))
            {
                bool finished = false;
                Incidence incidence = _adminMgm.GetIncidence(int.Parse(id));
                do
                {
                    Console.WriteLine("ADD COMMENT ");
                    incidence.Comments.Add(new Comment
                    {
                        CommentDate = System.DateTime.Now,
                        Text = Console.ReadLine(),
                        IncidenceID = incidence.ID,
                        Incidence = incidence
                    });

                } while (!finished);

                bool solved = false;
                Console.WriteLine("SOLVED? (Y/N)");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    solved = true;
                }

                incidence.Solved = solved;
            }
        }

        private void GenerateIncidence()
        {
            try
            {
                User user = UserActions.GetUser();
                string valId;
                Console.WriteLine("GET PENDING VALIDATION");
                Console.WriteLine("----------------------");

                ListPendingValidations(user);
                Console.WriteLine("INSERT PENDING TO VALIDATE SERVICE ID OR BLANK TO EXIT : ");
                valId = Console.ReadLine().ToUpper();
                if (String.IsNullOrWhiteSpace(valId))
                {
                    ShowServicesMenu();
                    return;
                }
                if (CommonLib.ValidateNumEntrance(valId))
                {
                    IncidenceDetails(long.Parse(valId));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void IncidenceDetails(long valId)
        {
            Console.WriteLine("INSERT DATE OF INCIDENCE");
            string d = Console.ReadLine();
            DateTime date = DateTime.Parse(d);

            Console.WriteLine("INSERT INCIDENCE DESCRIPTION");
            string desc = Console.ReadLine();
            _adminMgm.GenerateIncidence(valId, date, desc);
        }

        private void ValidateService()
        {
            try
            {
                GetValidation();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetValidation()
        {
            User user = UserActions.GetUser();
            string o;
            Console.WriteLine("GET PENDING VALIDATION");
            Console.WriteLine("----------------------");

            ListPendingValidations(user);

            Console.WriteLine("INSERT ID OF SERVICE PENDING TO VALIDATE OR BLANK TO EXIT : ");
            o = Console.ReadLine().ToUpper();
            if (String.IsNullOrWhiteSpace(o))
            {
                ShowServicesMenu();
                return;
            }
            if (CommonLib.ValidateNumEntrance(o))
            {
                try
                {
                    Console.WriteLine("DATE OF USING SERVICE :");
                    string d = Console.ReadLine();
                    DateTime date = DateTime.Parse(d);
                    long serviceId = long.Parse(o);
                    Service service = _adminMgm.GetService(serviceId);
                    
                    PayService(user, service);
                    _adminMgm.ValidateService(serviceId, date);

                }
                catch (Exception e)
                {
                    Console.WriteLine("SERVICE NOT VALIDATED : " + e.Message);
                }
            }
        }
        public bool PayService(User user, Service service)
        {
            try
            {
                IEnumerable<PaymentType> payMethods = _adminMgm.GetPayMethods();
                foreach (PaymentType t in payMethods)
                {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine("CHOOSE PAY METHOD");
                string p = Console.ReadLine();

                _adminMgm.NewPayment(user, service, (PaymentType)int.Parse(p));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("PAYMENT PROCESS NOT FINISHED : " + e.Message);
                throw e;
            }
        }

        private void ListPendingValidations(User user)
        {
            List<Validation> pendings = _adminMgm.GetPendingValidations(user);
            foreach (Validation v in pendings)
            {
                Console.WriteLine(v.ServiceID + " : " + v.Service.Name + " >> " + v.Service.ProviderID + " : " + v.Service.Provider.Name);
            }
        }

        private void RemoveService(Service aux)
        {
            try
            {
                _adminMgm.RemoveService(aux);
                Console.WriteLine("TOKEN REMOVED");
            }
            catch (Exception e)
            {
                Console.WriteLine("IMPOSSIBLE REMOVE TOKEN : " + e.Message);
            }
        }

        private Service GetService()
        {
            Service service = null;
            string o;
            Console.WriteLine("GET SERVICE");
            Console.WriteLine("-------------------");
            do
            {
                Console.WriteLine("INSERT EXISTING SERVICE ID OR BLANK TO EXIT : ");
                o = Console.ReadLine().ToUpper();
                if (String.IsNullOrWhiteSpace(o))
                {
                    ShowServicesMenu();
                    return null;
                }
                if (CommonLib.ValidateNumEntrance(o))
                {
                    service = _adminMgm.GetService(long.Parse(o));
                }

            } while (service == null);

            return service;
        }
    }
}
