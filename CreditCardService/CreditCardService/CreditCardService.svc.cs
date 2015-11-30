using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CreditCardService.Abstract;
using CreditCardService.Concrete;
using CreditCardService.Entities;
using CreditCardService.Models;
using CreditCardService.Static;

namespace CreditCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CreditCardService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CreditCardService.svc or CreditCardService.svc.cs at the Solution Explorer and start debugging.
    public class CreditCardService : ICreditCardService
    {
        private IRepository<User> userRepository;
        private IRepository<Account> accountRepository;
        private IRepository<Transfer> transferRepository;

        public CreditCardService()
        {
            userRepository = new UserRepository();
            accountRepository = new AccountRepository();
            transferRepository = new TransferRepository();
        }

        public PaymentStatus PayVisa(PaymentInfo info)
        {
            return Pay(info, CardType.Visa);
        }

        public PaymentStatus PayMasterCard(PaymentInfo info)
        {
            return Pay(info, CardType.MasterCard);
        }

        public ConfirmationStatus Confirm(string cardNumber, string confirmationCode)
        {
            try
            {
                var transfer = FindTransfer(cardNumber);

                if (!VerifyTransfer(transfer, confirmationCode))
                {
                    if (transfer.FailedConfirmationCount >= 3)
                    {
                        return ConfirmationStatus.Aborted;
                    }
                    return ConfirmationStatus.Failed;
                }

                CompleteTransfer(transfer);

                return ConfirmationStatus.Successful;
            }
            catch (Exception)
            {
                return ConfirmationStatus.Failed;
            }
        }

        private PaymentStatus Pay(PaymentInfo info, CardType type)
        {
            User user;
            Account payer;
            Account receiver;
            try
            {
                user = userRepository.Get(m => info.FullName.Contains(m.Name) &&
                                               info.FullName.Contains(m.Surname) &&
                                               type == m.CardType &&
                                               info.CardNumber.Equals(m.CardNumber) &&
                                               info.SecureCode.Equals(m.SecureCode) &&
                                               info.ExpirationMonth.Equals(m.ExperirationMonth) &&
                                               info.ExpirationYear.Equals(m.ExperirationYear));
            }
            catch (InvalidOperationException)
            {
                return PaymentStatus.NotExistedCard;
            }

            try
            {
                payer = accountRepository.Get(m => m.AccountId.Equals(user.AccountId));
                receiver = accountRepository.Get(m => m.Owner.Equals(info.Receiver));

                if (payer.Amount < info.Amount)
                {
                    return PaymentStatus.NotEnoughMoney;
                }

                var transfer = new Transfer()
                {
                    PayerId = payer.AccountId,
                    ReceiverId = receiver.AccountId,
                    InitTime = DateTime.UtcNow,
                    Amount = info.Amount,
                    VerificationCode = CodeWordGenerator.Generate(10)
                };


                SendMessage(new EmailService(user.Email),
                            string.Format("Your confirmation code for operation is: {0}", transfer.VerificationCode));


                transferRepository.Create(transfer);
                return PaymentStatus.ConfirmationRequired;

            }
            catch (Exception)
            {
                return PaymentStatus.Failed;
            }
        }

        private Transfer FindTransfer(string cardNumber)
        {
            var user = userRepository.Get(m => m.CardNumber.Equals(cardNumber));
            var account = accountRepository.Get(m => m.AccountId.Equals(user.AccountId));

            return transferRepository.Get(m => !m.Confirmed && m.PayerId.Equals(account.AccountId));
        }

        private bool VerifyTransfer(Transfer transfer, string verificationCode)
        {
            transfer.FailedConfirmationCount += 1;

            return transfer.VerificationCode == verificationCode;
        }

        private void CompleteTransfer(Transfer transfer)
        {
            transfer.Confirmed = true;

            var payer = accountRepository.Get(m => m.AccountId.Equals(transfer.PayerId));
            var receiver = accountRepository.Get(m => m.AccountId.Equals(transfer.ReceiverId));

            payer.Amount -= transfer.Amount;
            receiver.Amount += transfer.Amount;

            transfer.CompleteTime = DateTime.UtcNow;
        }

        private void SendMessage(IMessageService service, string message)
        {
            service.Send(message);
        }
    }
}
