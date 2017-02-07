using Domain.ValueObjects;
using System;

namespace Domain.Accounts
{
    public class Account
    {
        public AccountNumber Number { get; private set; }

        private decimal balance;

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

        public Account(AccountNumber accountNumber, decimal balance, Currency currencyType)
        {
            Number = accountNumber;
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