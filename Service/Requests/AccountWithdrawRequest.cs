using System;

namespace Service.Requests
{
    public class AccountWithdrawRequest
    {
        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public Guid Id { get; private set; }

        public AccountWithdrawRequest(Guid id, decimal amount, Currency currency)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
        }
    }
}