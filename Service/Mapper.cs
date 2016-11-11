using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class Mapper
    {
        //This method definitely belongs elsewhere
        internal Domain.Currency Get(Currency currency)
        {
            switch (currency)
            {
                case Currency.USD:
                    return Domain.Currency.USD;
                case Currency.AUS:
                    return Domain.Currency.AUS;
                case Currency.CAN:
                    return Domain.Currency.CAN;
                default:
                    throw new NotImplementedException("Could not find currency " + currency);
            }
        }

        internal Currency Get(Domain.Currency currency)
        {
            switch (currency)
            {
                case Domain.Currency.USD:
                    return Currency.USD;
                case Domain.Currency.AUS:
                    return Currency.AUS;
                case Domain.Currency.CAN:
                    return Currency.CAN;
                default:
                    throw new NotImplementedException("Could not find currency " + currency);
            }
        }

        internal Money Get(Currency currency, decimal amount)
        {
            return new Money(amount, Get(currency));
        }
    }
}
