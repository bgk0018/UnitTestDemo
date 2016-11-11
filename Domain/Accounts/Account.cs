using Domain.ValueObjects;
using System;

namespace Domain.Accounts
{
    public class Account
    {
        private decimal balance;

        public Guid Id { get; private set; }

        public decimal Balance
        {
            get
            {
                return balance;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                balance = value;
            }
        }

        public Currency CurrencyType { get; private set; }

        public Account(Guid id, decimal balance, Currency currencyType)
        {
            Id = id;
            Balance = balance;
            CurrencyType = currencyType;
        }

        public void Withdraw(Money money)
        {
            if (money.Currency != CurrencyType)
                throw new ArgumentOutOfRangeException("money");

            Balance -= money.Amount;
        }

        public void Deposit(Money money)
        {
            if (money.Currency != CurrencyType)
                throw new ArgumentOutOfRangeException("money");

            Balance += money.Amount;
        }
    }
}