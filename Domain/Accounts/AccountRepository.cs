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
            return persistence.First(p=>p.Key == id).Value;
        }

        public void Save(Account account)
        {
            persistence.Add(account.Id, account);
        }
    }
}
