using Service.Requests;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
