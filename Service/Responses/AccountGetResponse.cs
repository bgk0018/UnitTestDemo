using System;

namespace Service.Responses
{
    public class AccountGetResponse
    {
        public Guid Id { get; private set; }

        public Currency Currency { get; private set; }

        public decimal Balance { get; private set; }

        public AccountGetResponse(Guid id, Currency currency, decimal balance)
        {
            Id = id;
            Currency = currency;
            Balance = balance;
        }
    }
}