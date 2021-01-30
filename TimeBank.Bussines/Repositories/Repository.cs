using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Core.DataAccess;
using TimeBank.Core.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeBank.Bussines.Repositories
{
    public class Repository
    {
        #region Services
        internal Service GetService(string serviceName)
        {
            using var db = new TimeBankContext();
            return db.Services.FirstOrDefault(s => s.Name == serviceName);
        }
        internal Service GetService(long serviceID)
        {
            using var db = new TimeBankContext();
            return db.Services.FirstOrDefault(p => p.ServiceID == serviceID);
        }
        internal List<Service> GetAllServices(Category cat)
        {
            using var db = new TimeBankContext();
            return (from ser in db.Services where ser.Category == cat select ser).ToList();
        }
        internal List<Service> GetAllServices(User user)
        {
            using var db = new TimeBankContext();
            return (from ser in db.Services where ser.Provider == user select ser).ToList();
        }
        internal List<Service> GetAllServices()
        {
            using var db = new TimeBankContext();
            return (from ser in db.Services select ser).ToList();
        }
        internal async void InsertOrUpdate(Service service)
        {
            await using var db = new TimeBankContext();
            var query = (from s in db.Services where s.ServiceID == service.ServiceID select s).FirstOrDefault();
            if (query == null)
            {
                db.Services.Add(service);
            }
            else
            {
                query.Name = service.Name;
                query.Price = service.Price;
                query.Provider = service.Provider;
                query.ProviderID = service.ProviderID;
                query.ServiceID = service.ServiceID;
                query.Validation = service.Validation;
            }
            await db.SaveChangesAsync();
        }
        internal async void RemoveService(Service service)
        {
            await using var db = new TimeBankContext();
            db.Services.Remove(service);
        }
        #endregion

        #region Validations
        internal async void InsertOrUpdate(Validation val)
        {
            await using var db = new TimeBankContext();
            var query = (from v in db.Validations where v.ID == val.ID select v).FirstOrDefault();
            if (query == null)
            {
                db.Validations.Add(val);
            }
            else
            {
                query.Incidences = val.Incidences ?? new List<Incidence>();
                query.Paid = val.Paid;
                query.Service = val.Service;
                query.ServiceID = val.ServiceID;
                query.ServiceUsedOn = val.ServiceUsedOn;
                query.TBankUserID = val.TBankUserID;
                query.Validated = val.Validated;
                query.ValidatedOn = val.ValidatedOn;
            }
            await db.SaveChangesAsync();
        }
        internal List<Validation> GetPendingValidations(User user)
        {
            using var db = new TimeBankContext();
            return (from val in db.Validations where val.User == user select val).ToList();
        }

        internal List<Validation> GetPendingValidations(Service service)
        {
            using var db = new TimeBankContext();
            return (from val in db.Validations where val.Service == service select val).ToList();
        }
        internal Validation GetPendingValidations(long id)
        {
            using var db = new TimeBankContext();
            return (from val in db.Validations where val.ID == id select val).FirstOrDefault();
        }
        #endregion

        #region Users
        internal async void InsertOrUpdate(User user)
        {
            await using (var db = new TimeBankContext())
            {

                var query = (from u in db.Users where u.UserId == user.UserId select u).FirstOrDefault();
                if (query == null)
                {
                    db.Users.Add(user);
                }
                else
                {
                    query.Address = user.Address ?? new Address();
                    query.AddressId = user.AddressId;
                    query.Admin = user.Admin;
                    query.Comments = user.Comments ?? new List<Comment>();
                    query.InDate = user.InDate;
                    query.LastName = user.LastName;
                    query.Name = user.Name;
                    query.OutDate = user.OutDate;
                    query.Password = user.Password;
                    query.Payments = user.Payments ?? new List<Payment>();
                    query.ProvideServices = user.ProvideServices;
                    query.Rating = user.Rating;
                    query.UserId = user.UserId;
                    query.Validations = user.Validations ?? new List<Validation>();
                    query.Wallet = user.Wallet ?? new Wallet();

                }
                db.Database.OpenConnection();
                try
                {
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                    await db.SaveChangesAsync();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        internal async void RemoveUser(User user)
        {
            await using var db = new TimeBankContext();
            db.Users.Remove(user);
            db.SaveChanges();
        }
        internal List<User> GetUsers()
        {
            using var db = new TimeBankContext();
            return (from u in db.Users select u).ToList();
        }
        internal User GetUser(long id)
        {
            using var db = new TimeBankContext();
            try
            {
                return (from u in db.Users where u.UserId == id select u).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        internal User GetUser(string name)
        {
            using var db = new TimeBankContext();
            try
            {
                return (from u in db.Users where u.Name == name select u).First();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Tokens
        internal async void InsertOrUpdate(Token token)
        {
            await using var db = new TimeBankContext();
            var query = (from t in db.Tokens where t.ID == token.ID select t).FirstOrDefault();
            if (query == null)
            {
                db.Tokens.Add(token);
            }
            else
            {
                query.Name = token.Name;
                query.Hours = token.Hours;
            }
            await db.SaveChangesAsync();
        }
        internal Token GetTokenById(int tokenId)
        {
            using var db = new TimeBankContext();
            return db.Tokens.FirstOrDefault(t => t.ID == tokenId);
        }
        internal List<Token> GetTokensList()
        {
            using var db = new TimeBankContext();
            return (from token in db.Tokens select token).OrderByDescending(x => x.Hours).ToList();
        }
        internal async void RemoveToken(Token token)
        {
            await using var db = new TimeBankContext();
            db.Tokens.Remove(token);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Payments
        internal async void InsertOrUpdate(Payment payment)
        {
            await using var db = new TimeBankContext();
            var query = (from p in db.Payments where p.ID == payment.ID select p).FirstOrDefault();
            if (query == null)
            {
                db.Payments.Add(payment);
            }
            else
            {
                query.PaymentType = payment.PaymentType;
                query.User = payment.User;
                query.TBankUserID = payment.TBankUserID;
                query.Validation = payment.Validation;
                query.ValidationId = payment.ValidationId;
            }
            await db.SaveChangesAsync();
        }
        #endregion

        #region Categories
        internal Category GetCategory(int catId)
        {
            using var db = new TimeBankContext();
            return (from c in db.Categories where c.ID == catId select c).FirstOrDefault();
        }
        internal Category GetCategory(string name)
        {
            using var db = new TimeBankContext();
            return (from c in db.Categories where c.Name == name select c).FirstOrDefault();
        }
        internal List<Category> GetCategories()
        {
            using var db = new TimeBankContext();
            return db.Categories.ToList();
        }
        internal async void InsertOrUpdate(Category cat)
        {
            await using var db = new TimeBankContext();
            var query = (from c in db.Categories where c.ID == cat.ID select c).FirstOrDefault();
            
            if (query == null)
            {
                db.Categories.Add(cat);
            }
            else
            {
                query.Name = cat.Name;
            }
            await db.SaveChangesAsync();
        }
        internal async void RemoveCategory(Category cat)
        {
            using var db = new TimeBankContext();
            db.Categories.Remove(cat);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Incidences
        internal async void InsertOrUpdate(Incidence incidence)
        {
            await using var db = new TimeBankContext();
            var query = (from i in db.Incidences where i.ID == incidence.ID select i).FirstOrDefault();
            if (query == null)
            {
                db.Incidences.Add(incidence);
            }
            else
            {
                query.Service = incidence.Service;
                query.ServiceID = incidence.ServiceID;
                query.Solved = incidence.Solved;
                query.Description = incidence.Description;
                query.Comments = incidence.Comments ?? new List<Comment>();
            }
            await db.SaveChangesAsync();
        }
        internal Incidence GetIncidence(int id)
        {
            using var db = new TimeBankContext();
            return db.Incidences.FirstOrDefault(i => i.ID == id);
        }
        #endregion
    }
}
