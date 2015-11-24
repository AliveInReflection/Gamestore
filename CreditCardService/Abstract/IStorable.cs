using CreditCardService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Abstract
{
    public interface IStorable
    {
        ICollection<User> Users { get; }
        ICollection<Account> Accounts { get; }
        ICollection<Transfer> Transfers { get; }
    }
}