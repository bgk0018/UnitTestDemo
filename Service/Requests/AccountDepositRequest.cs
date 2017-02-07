using System;
using Domain.ValueObjects;

namespace Service.Requests
{
    public class AccountDepositRequest
    {
        public AccountNumber Number { get; private set; }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public AccountDepositRequest(AccountNumber number, decimal amount, Currency currency)
        {
            Number = number;
            Amount = amount;
            Currency = currency;
        }
    }
}