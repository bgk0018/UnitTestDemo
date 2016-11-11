using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
