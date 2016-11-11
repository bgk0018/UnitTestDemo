using System;

namespace Domain.Accounts
{
    public interface IAccountRepository
    {
        void Save(Account account);

        Account Get(Guid id);
    }
}