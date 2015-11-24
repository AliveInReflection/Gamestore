using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CreditCardService.Abstract;
using CreditCardService.Entities;

namespace CreditCardService.Concrete
{
    public class TransferService : ITransferRepository
    {
        private IStorable database;

        public TransferService()
        {
            database = new FakeDatabase();
        }

        public Transfer Get(Expression<Func<Transfer, bool>> predicate)
        {
            return database.Transfers.First(predicate.Compile());
        }

        public void Create(Transfer transfer)
        {
            database.Transfers.Add(transfer);
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