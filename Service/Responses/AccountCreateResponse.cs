using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
