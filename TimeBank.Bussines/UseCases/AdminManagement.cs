using System;
using System.Collections.Generic;
using System.Linq;
using TimeBank.Bussines.Repositories;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Enums;
using TimeBank.Core.Models;

namespace TimeBank.Bussines.UseCases
{
    public class AdminManagement
    {
        public Actions Orden { get; set; }

        private static AdminManagement INSTANCE;
        private static Repository _repo;

        public AdminManagement() { _repo = new Repository(); }
        
        public static AdminManagement GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new AdminManagement();
            }
            return INSTANCE;
        }
        
        public List<Token> GetTokens()
        {
            return _repo.GetTokensList();
        }

        public List<Service> GetAllServices()
        {
            return _repo.GetAllServices();
        }

        public bool RemoveToken(Token token)
        {
            if (RemovableToken(token))
            {
                _repo.RemoveToken(token);
                return true;
            }
            return false;
        }

        private bool RemovableToken(Token token)
        {
            if (GetAllServices().Find(s => s.Price.Find(t => t == token) != null) != null) { return false; }
            if (UserManagement.GetInstance().GetUsers().Find(u => u.Wallet.Credit.Find(t => t == token) != null) != null) { return false; }
            if (UserManagement.GetInstance().GetUsers().Find(u => u.Wallet.Debit.Find(t => t == token) !=null) != null) { return false; }
            return true;
        }

        public Token GetTokenById(int tokenId)
        {
            return _repo.GetTokenById(tokenId);
        }

        public List<Service> GetAllServices(Category cat)
        {
            return _repo.GetAllServices(cat);
        }

        public bool InsertOrUpdate(Category cat)
        {
            try
            {
                _repo.InsertOrUpdate(cat);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertOrUpdate(Token token)
        {
            try
            {
                _repo.InsertOrUpdate(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category GetCategory(int catId)
        {
            return _repo.GetCategory(catId);
        }

        public Service GetService(string name)
        {
            Service service = _repo.GetService(name);
            if (service == null)
            {
                throw new ArgumentException("Unnable to find Service " + name);
            }
            return service;
        }
        
        public Service GetService(long serviceId)
        {
            Service service = _repo.GetService(serviceId);
            if (service == null)
            {
                throw new ArgumentException("Unnable to find Service with id " + serviceId);
            }
            return service;
        }

        public IEnumerable<PaymentType> GetPayMethods()
        {
            return Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>();
        }

        public void NewPayment(User user, Service service, PaymentType p)
        {
            int diff = GetUserEnoughFounds(user.Wallet, service.Price);
            if (diff < 0)
            {
                if (!CheckUserEnoughDebit(diff, user.Wallet))
                {
                    throw new ArgumentException("Not Enough founds");
                }
            }

            Payment payment = new Payment
            {
                TBankUserID = user.UserId,
                User = user,
                ValidationId = service.Validation.ID,
                Validation = service.Validation,
                PaymentType = p
            };

            int price = CommonLib.TokenListToHours(service.Price);

            User provider = _repo.GetUsers().SingleOrDefault(p => p.UserId == service.Provider.UserId);
            provider.Wallet.Credit.AddRange(service.Price);

            while (price > 0)
            {
                try
                {
                    Token tPaid = user.Wallet.Credit.Find(t => t.Hours <= price);
                    price -= tPaid.Hours;
                    user.Wallet.Credit.Remove(tPaid);
                    Console.WriteLine("Paid token " + tPaid.Name + " <" + tPaid.Hours + ">");
                }
                catch (Exception)
                {
                    try
                    {
                        Token tPaid = user.Wallet.Credit.Find(t => t.Hours <= price);
                        price -= tPaid.Hours;
                        user.Wallet.Credit.Remove(tPaid);
                        Console.WriteLine("Owed token " + tPaid.Name + " <" + tPaid.Hours + ">");
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Error trying to pay service : " + e.Message);
                    }
                }
            }

            _repo.InsertOrUpdate(provider);
            _repo.InsertOrUpdate(user);
            _repo.InsertOrUpdate(service);
            _repo.InsertOrUpdate(payment);
        }

        private int GetUserEnoughFounds(Wallet wallet, List<Token> price)
        {
            int walletHours = CommonLib.TokenListToHours(wallet.Credit);
            int priceHours = CommonLib.TokenListToHours(price);

            return walletHours - priceHours;
        }

        private bool CheckUserEnoughDebit(int diff, Wallet wallet)
        {
            int debitHours = CommonLib.TokenListToHours(wallet.Debit);
            return debitHours + diff > wallet.MaxDebit;
        }

        public Category GetCategory(string name)
        {
            return _repo.GetCategory(name);
        }

        public List<Service> GetAllServices(User user)
        {
            return _repo.GetAllServices(user);
        }

        public Incidence GetIncidence(int id)
        {
            return _repo.GetIncidence(id);
        }

        public List<Validation> GetPendingValidations(User user)
        {
            List<Validation> validations = _repo.GetPendingValidations(user);

            if (validations == null)
            {
                throw new ArgumentException("Not Services Pending to Validate");
            }

            return validations;
        }

        public void GenerateIncidence(long valId, DateTime date, string desc)
        {
            Validation val = _repo.GetPendingValidations(valId);
            Incidence incidence = new Incidence
            {
                IssueDate = date,
                Description = desc,
                Service = val.Service,
                ServiceID = val.ServiceID,
                Solved = false,
                Comments = new List<Comment>()
            };

            val.Incidences.Add(incidence);

            _repo.InsertOrUpdate(incidence);
            _repo.InsertOrUpdate(val);
        }

        public void NewValidation(User user, Service service)
        {
            Validation val = new Validation();
            val.Validated = false;
            val.Service = service;
            val.ServiceID = service.ServiceID;
            val.User = user;
            val.TBankUserID = user.UserId;

            user.Validations.Add(val);
            service.Validation = val;

            _repo.InsertOrUpdate(val);
            _repo.InsertOrUpdate(user);
            _repo.InsertOrUpdate(service);
        }

        public void InsertOrUpdate(User user)
        {
            _repo.InsertOrUpdate(user);
        }

        public List<Category> GetCategories()
        {
            return _repo.GetCategories();
        }

        public void ValidateService(long v, DateTime date)
        {
            try
            {
                Validation val = _repo.GetPendingValidations(v);
                val.Validated = true;
                val.ValidatedOn = date;
                _repo.InsertOrUpdate(val);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertOrUpdate(Service service)
        {
            try
            {
                if (!SafeUpdate(service))
                {
                    throw new ArgumentException("Service data not valid");
                }
                _repo.InsertOrUpdate(service);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Unnable to update database : " + e.Message);
            }
        }

        private bool SafeUpdate(Service service)
        {
            if (_repo.GetCategory(service.CategoryID) == null 
                || _repo.GetUser(service.ProviderID) == null)
            {
                return false;
            }
            service.Available = true;
            return true;
        }

        public void RemoveService(Service service)
        {
            if (RemovableService(service))
            {
                _repo.RemoveService(service);
            }
            else 
            {
                throw new ArgumentException("SERVER LOCK BY USING");
            }
        }

        private bool RemovableService(Service service)
        {
            if (_repo.GetPendingValidations(service) != null) { return false; }
            return true;
        }
    }
}