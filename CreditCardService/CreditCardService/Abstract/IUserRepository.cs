using System;
using System.Linq.Expressions;
using CreditCardService.Entities;
namespace CreditCardService.Abstract
{
    public interface IUserRepository
    {
        User Get(Expression<Func<User, bool>> predicate);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
