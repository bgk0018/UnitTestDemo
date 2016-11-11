using System;

namespace Service.Requests
{
    public class AccountTransferRequest
    {
        public Guid WithdrawAccountId { get; private set; }

        public Guid DepositAccountId { get; private set; }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public AccountTransferRequest(Guid withdrawAccountId, Guid depositAccountId, decimal amount, Currency currency)
        {
            WithdrawAccountId = withdrawAccountId;
            DepositAccountId = depositAccountId;
            Amount = amount;
            Currency = currency;
        }
    }
}