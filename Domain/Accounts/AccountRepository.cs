using System.Linq;
using System;
using System.Collections.Generic;

namespace Domain.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        Dictionary<Guid, Account> persistence;

        public AccountRepository()
        {
            persistence = new Dictionary<Guid, Account>();
        }

        public Account Get(Guid id)
        {
            var persisted = persistence.First(p => p.Key == id).Value;

            return new Account(persisted.Id, persisted.Balance, persisted.CurrencyType);
        }

        public void Save(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            if(persistence.Any(p=>p.Key == account.Id))
            {
                persistence[account.Id] = account;
            }
            else
            {
                persistence.Add(account.Id, account);
            }
        }
    }
}
