using System;

namespace Service.Requests
{
    public class AccountGetRequest
    {
        public Guid Id { get; private set; }

        public AccountGetRequest(Guid id)
        {
            Id = id;
        }
    }
}