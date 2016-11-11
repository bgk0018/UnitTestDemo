using System;

namespace Domain.ValueObjects
{
    public class Money
    {
        private decimal amount;

        public decimal Amount
        {
            get
            {
                return amount;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                amount = value;
            }
        }

        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}