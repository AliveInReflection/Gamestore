using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CreditCardService.Entities;

namespace CreditCardService.Abstract
{
    public interface ITransferRepository
    {
        Transfer Get(Expression<Func<Transfer, bool>> predicate);
        void Create(Transfer transfer);
        void Update(Transfer transfer);
        void Delete(int id);
    }
}