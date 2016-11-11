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