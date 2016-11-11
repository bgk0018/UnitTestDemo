using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Requests
{
    public class AccountCreateRequest
    {
        public Currency CurrencyType { get; private set; }

        public AccountCreateRequest(Currency currency)
        {
            CurrencyType = currency;
        }
    }
}
