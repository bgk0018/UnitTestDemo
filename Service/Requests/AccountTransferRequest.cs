using System;
using Domain.ValueObjects;

namespace Service.Requests
{
    public class AccountTransferRequest
    {
        public AccountNumber WithdrawAccountNumber { get; private set; }

        public AccountNumber DepositAccountNumber { get; private set; }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public AccountTransferRequest(AccountNumber withdrawAccountNumber, AccountNumber depositAccountNumber, decimal amount, Currency currency)
        {
            WithdrawAccountNumber = withdrawAccountNumber;
            DepositAccountNumber = depositAccountNumber;
            Amount = amount;
            Currency = currency;
        }
    }
}