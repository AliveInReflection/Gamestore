using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class AccountRepository : IAccountRepository
    {
        private IStorable database;

        public AccountRepository()
        {
            this.database = new FakeDatabase();
        }

        public Account Get(Expression<Func<Account, bool>> predicate)
        {
            return database.Accounts.First(predicate.Compile());
        }

        public void Create(Account account)
        {
            if (database.Accounts.Any())
            {
                account.AccountId = database.Accounts.Max(m => m.AccountId) + 1;
            }
            else
            {
                account.AccountId = 1;
            }

            database.Accounts.Add(account);
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