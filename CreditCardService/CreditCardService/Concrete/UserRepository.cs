using System;
using System.Linq;
using System.Linq.Expressions;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class UserRepository : IRepository<User>
    {
        public User Get(Expression<Func<User, bool>> predicate)
        {
            return FakeDatabase.Users.First(predicate.Compile());
        }

        public void Create(User user)
        {
            if (FakeDatabase.Users.Any())
            {
                user.UserId = FakeDatabase.Users.Max(m => m.UserId) + 1;
            }
            else
            {
                user.UserId = 1;
            }

            FakeDatabase.Users.Add(user);
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