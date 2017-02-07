using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ValueObjects;

namespace Domain.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private Dictionary<AccountNumber, Account> persistence;

        public AccountRepository()
        {
            persistence = new Dictionary<AccountNumber, Account>();
        }

        public Account Get(AccountNumber number)
        {
            var persisted = persistence.First(p => p.Key == number).Value;

            return new Account(persisted.Number, persisted.Balance, persisted.CurrencyType);
        }

        public void Save(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            if (persistence.Any(p => p.Key == account.Number))
            {
                persistence[account.Number] = account;
            }
            else
            {
                persistence.Add(account.Number, account);
            }
        }
    }
}