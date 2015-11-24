using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class UserRepository : IUserRepository
    {
        private IStorable database;

        public UserRepository()
        {
            database = new FakeDatabase();
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return database.Users.First(predicate.Compile());
        }

        public void Create(User user)
        {
            database.Users.Add(user);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}