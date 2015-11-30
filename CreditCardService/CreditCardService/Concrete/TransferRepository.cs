using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class TransferRepository : IRepository<Transfer>
    {
        public Transfer Get(Expression<Func<Transfer, bool>> predicate)
        {
            return FakeDatabase.Transfers.First(predicate.Compile());
        }

        public void Create(Transfer transfer)
        {
            if (FakeDatabase.Transfers.Any())
            {
                transfer.TransferId = FakeDatabase.Transfers.Max(m => m.TransferId) + 1;
            }
            else
            {
                transfer.TransferId = 1;
            }
            FakeDatabase.Transfers.Add(transfer);
        }

        public void Update(Transfer transfer)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}