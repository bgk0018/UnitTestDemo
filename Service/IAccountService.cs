using Service.Requests;
using Service.Responses;

namespace Service
{
    public interface IAccountService
    {
        void Withdraw(AccountWithdrawRequest request);

        void Deposit(AccountDepositRequest request);

        void Transfer(AccountTransferRequest request);

        AccountCreateResponse Create(AccountCreateRequest request);

        AccountGetResponse Get(AccountGetRequest request);
    }
}