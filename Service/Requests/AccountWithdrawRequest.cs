using System;
using Domain.ValueObjects;

namespace Service.Requests
{
    public class AccountWithdrawRequest
    {
        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public AccountNumber Number { get; private set; }

        public AccountWithdrawRequest(AccountNumber number, decimal amount, Currency currency)
        {
            Number = number;
            Amount = amount;
            Currency = currency;
        }
    }
}