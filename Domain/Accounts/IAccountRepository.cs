using System;
using Domain.ValueObjects;

namespace Domain.Accounts
{
    public interface IAccountRepository
    {
        void Save(Account account);

        Account Get(AccountNumber id);
    }
}