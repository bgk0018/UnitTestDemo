using Domain;
using System;

namespace Domain.Accounts
{
    public class AccountFactory : IAccountFactory
    {
        public Account Create(Currency currency)
        {
            Guid id = Guid.NewGuid();
            return new Account(id, 0, currency);
        }
    }
}
