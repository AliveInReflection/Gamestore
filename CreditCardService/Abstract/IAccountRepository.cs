using CreditCardService.Entities;
using System;
using System.Linq.Expressions;

namespace CreditCardService.Abstract
{
    public interface IAccountRepository
    {
        Account Get(Expression<Func<Account,bool>> predicate);
        void Create(Account account);
        void Update(Account account);
        void Delete(int id);
    }
}