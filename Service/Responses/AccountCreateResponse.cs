using System;
using Domain.ValueObjects;

namespace Service.Responses
{
    public class AccountCreateResponse
    {
        public AccountNumber Number { get; private set; }

        public AccountCreateResponse(AccountNumber number)
        {
            Number = number;
        }
    }
}