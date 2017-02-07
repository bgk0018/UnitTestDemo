using Domain.Accounts;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class AccountTransferService
    {
        public void Transfer(Money money, Account to, Account from)
        {
            from.Withdraw(money);

            to.Deposit(money);
        }
    }
}