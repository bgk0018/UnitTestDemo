﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
    }
}
