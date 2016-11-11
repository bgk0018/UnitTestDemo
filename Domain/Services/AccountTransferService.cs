using Domain.Accounts;
using Domain.ValueObjects;

namespace Service.Services
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