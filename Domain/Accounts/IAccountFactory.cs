namespace Domain.Accounts
{
    public interface IAccountFactory
    {
        Account Create(Currency currency);
    }
}
