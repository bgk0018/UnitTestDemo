using System;

namespace Service.Responses
{
    public class AccountCreateResponse
    {
        public Guid Id { get; private set; }

        public AccountCreateResponse(Guid id)
        {
            Id = id;
        }
    }
}