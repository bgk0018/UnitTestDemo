﻿using Domain.Accounts;
using Domain.ValueObjects;
using Service.Requests;
using Service.Responses;
using Service.Services;
using System;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repo;
        private readonly IAccountFactory factory;
        private readonly Mapper mapper;

        public AccountService(IAccountRepository repo, IAccountFactory factory)
        {
            if (repo == null)
                throw new ArgumentNullException("repo");

            if (factory == null)
                throw new ArgumentNullException("factory");

            this.repo = repo;
            this.factory = factory;
            mapper = new Mapper();
        }

        public AccountCreateResponse Create(AccountCreateRequest request)
        {
            Domain.Currency domainCurrency = mapper.Get(request.CurrencyType);

            var account = factory.Create(domainCurrency);

            repo.Save(account);

            return new AccountCreateResponse(account.Id);
        }

        public void Deposit(AccountDepositRequest request)
        {
            var account = repo.Get(request.Id);

            var currency = mapper.Get(request.Currency);
            Money money = new Money(request.Amount, currency);

            account.Deposit(money);

            repo.Save(account);
        }

        public AccountGetResponse Get(AccountGetRequest request)
        {
            var account = repo.Get(request.Id);

            var currency = mapper.Get(account.CurrencyType);

            return new AccountGetResponse(account.Id, currency, account.Balance);
        }

        public void Transfer(AccountTransferRequest request)
        {
            var transferService = new AccountTransferService();

            var to = repo.Get(request.DepositAccountId);
            var from = repo.Get(request.WithdrawAccountId);

            var currency = mapper.Get(request.Currency);
            Money money = new Money(request.Amount, currency);

            //wraps up functionality that exists as a function of a relationship
            transferService.Transfer(money, to, from);

            repo.Save(to);
            repo.Save(from);
        }

        public void Withdraw(AccountWithdrawRequest request)
        {
            var account = repo.Get(request.Id);

            var currency = mapper.Get(request.Currency);
            Money money = new Money(request.Amount, currency);

            account.Withdraw(money);

            repo.Save(account);
        }
    }
}