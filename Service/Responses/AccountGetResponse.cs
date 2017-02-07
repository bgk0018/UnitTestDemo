using System;
using Domain.ValueObjects;

namespace Service.Responses
{
    public class AccountGetResponse
    {
        public AccountNumber Number { get; private set; }

        public Currency Currency { get; private set; }

        public decimal Balance { get; private set; }

        public AccountGetResponse(AccountNumber number, Currency currency, decimal balance)
        {
            Number = number;
            Currency = currency;
            Balance = balance;
        }
    }
}