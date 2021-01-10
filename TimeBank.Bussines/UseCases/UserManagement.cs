using System;
using System.Collections.Generic;
using System.Text;
using TimeBank.Bussines.Repositories;
using TimeBank.Core.Models;

namespace TimeBank.Bussines.UseCases
{
    public class UserManagement
    {
        private static UserManagement INSTANCE;
        
        private Repository _repo;

        public User CurrentUser { get; set; }

        private UserManagement()
        {
            _repo = new Repository();
        }

        public static UserManagement GetInstance()
        {
            if (INSTANCE == null)
            {
                 INSTANCE = new UserManagement();
            }
            return INSTANCE;
        }
        public bool InsertOrUpdate(User user)
        {
            if (user.Validations == null)
            {
                user.Validations = new List<Validation>();
            }
            _repo.InsertOrUpdate(user);
            return true;
        }
        public User GetUserAccess(object id)
        {
            if (id.GetType().Equals(typeof(String)))
            {
                return GetUser((string)id);
            } 
            else
            {
                return GetUser((long)id);
            }
        }

        public User GetUser(string userName)
        {
            return _repo.GetUser(userName);
        }

        public User GetUser(long userID)
        {
           return _repo.GetUser(userID);
        }

        public List<User> GetUsers()
        {
            return _repo.GetUsers();
        }

        public bool RemoveUser(User user)
        {
            try
            {
                _repo.RemoveUser(user);
            }
            catch
            {
                Console.WriteLine("Unnable to remove User");
                return false;
            }
            return true;
        }
    }
}
