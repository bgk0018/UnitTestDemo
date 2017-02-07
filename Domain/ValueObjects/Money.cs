using System;

namespace Domain.ValueObjects
{
    public struct Money
    {
        private readonly decimal amount;
        private readonly Currency currency;

        public decimal Amount { get { return amount; } }

        public Currency Currency { get { return currency; } }

        public Money(decimal amount, Currency currency)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount");

            this.amount = amount;
            this.currency = currency;
        }
    }
}