using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Requests
{
    public class AccountDepositRequest
    {
        public Guid Id { get; private set; }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public AccountDepositRequest(Guid id, decimal amount, Currency currency)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
        }
    }
}
