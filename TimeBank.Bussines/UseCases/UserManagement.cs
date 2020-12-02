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

        private UserManagement()
        {

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
            return false;
        }

        public User GetUser(long userID)
        {
            throw new NotImplementedException();
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
