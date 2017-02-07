using System;
using Domain.ValueObjects;

namespace Domain.Accounts
{
    public class AccountFactory : IAccountFactory
    {
        //Don't overthink this part, assume there is a reasonable way in which we're generating
        //The accountNumber, maybe from a database plus other information
        public Account Create(Currency currency)
        {
            var random = new Random();

            AccountNumber number = random.Next(0, 99999999).ToString().PadLeft(8) + "-" + random.Next(0, 5);

            return new Account(number, 0, currency);
        }
    }
}