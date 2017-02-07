using System;
using Domain.ValueObjects;

namespace Service.Requests
{
    public class AccountGetRequest
    {
        public AccountNumber Number { get; private set; }

        public AccountGetRequest(AccountNumber number)
        {
            Number = number;
        }
    }
}