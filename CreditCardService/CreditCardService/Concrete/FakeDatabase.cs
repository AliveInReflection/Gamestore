using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditCardService.Abstract;
using CreditCardService.Entities;
using CreditCardService.Static;

namespace CreditCardService.Concrete
{
    public static class FakeDatabase
    {
        private static ICollection<Account> accounts;
        private static ICollection<User> users;
        private static ICollection<Transfer> transfers;

        static FakeDatabase()
        {
            accounts = new List<Account>
            {
                new Account(){AccountId = 1, Owner = "GameStore", Amount = 100m},
                new Account(){AccountId = 2, Owner = "Garrus Vaccarian", Amount = 300m},
                new Account(){AccountId = 3, Owner = "Sarah Kerrigan", Amount = 100}
            };

            users = new List<User>()
            {
                new User()
                {
                    UserId = 1,
                    AccountId = 2,
                    Name = "Garrus",
                    Surname = "Vaccarian",
                    Email = "Yurii_Honchar@epam.com",
                    PhoneNumber = "+380981232233",
                    CardType = CardType.Visa,
                    CardNumber = "0000-0000-0000-0000",
                    ExperirationMonth = 10,
                    ExperirationYear = 2018,
                    SecureCode = 123,                    
                },
                new User()
                {
                    UserId = 2,
                    AccountId = 3,
                    Name = "Sarah",
                    Surname = "Kerrigan",
                    Email = "Yurii_Honchar@epam.com",
                    PhoneNumber = "+380981232233",
                    CardType = CardType.MasterCard,
                    CardNumber = "1111-1111-1111-1111",
                    ExperirationMonth = 10,
                    ExperirationYear = 2018,
                    SecureCode = 234,                    
                }
            };

            transfers = new List<Transfer>();
        }


        public static ICollection<User> Users
        {
            get { return users; }
        }

        public static ICollection<Account> Accounts
        {
            get { return accounts; }
        }

        public static ICollection<Transfer> Transfers
        {
            get { return transfers; }
        }
    }
}