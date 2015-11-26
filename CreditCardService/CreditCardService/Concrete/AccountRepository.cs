using System;
using System.Linq;
using System.Linq.Expressions;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class AccountRepository : IAccountRepository
    {
        public Account Get(Expression<Func<Account, bool>> predicate)
        {
            return FakeDatabase.Accounts.First(predicate.Compile());
        }

        public void Create(Account account)
        {
            if (FakeDatabase.Accounts.Any())
            {
                account.AccountId = FakeDatabase.Accounts.Max(m => m.AccountId) + 1;
            }
            else
            {
                account.AccountId = 1;
            }

            FakeDatabase.Accounts.Add(account);
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}