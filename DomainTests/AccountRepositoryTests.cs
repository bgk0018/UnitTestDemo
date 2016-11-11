using Domain.Accounts;
using Domain.ValueObjects;
using DomainTests.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestDemo.Tests
{
    public class AccountRepositoryTests
    {
        public class TheSaveMethod
        {
            [Theory(DisplayName = "Succeed_With_Add"), AutoMoqData]
            public void Succeed_With_Add(Account account,
                                                    AccountRepository sut)
            {
                sut.Save(account);
            }

            [Theory(DisplayName = "Succeed_With_Update"), AutoMoqData]
            public void Succeed_With_Update(decimal amount,
                                            Account account,
                                            AccountRepository sut)
            {
                sut.Save(account);
                var persistedAccount = sut.Get(account.Id);
                Money money = new Money(amount, account.CurrencyType);

                persistedAccount.Deposit(money);

                sut.Save(account);
            }

            [Theory(DisplayName = "Fail_On_Null_Account"), AutoMoqData]
            public void Fail_On_Null_Account(AccountRepository sut)
            {
                Account nullAccount = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    sut.Save(nullAccount);
                });
            }
        }

        public class TheGetMethod
        {
            [Theory(DisplayName = "Succeed_On_Valid_Input"), AutoMoqData]
            public void Succeed_On_Valid_Input(List<Account> accounts, AccountRepository sut)
            {
                var retrieve = accounts.FirstOrDefault();

                foreach (var account in accounts)
                {
                    sut.Save(account);
                }

                var result = sut.Get(retrieve.Id);

                Assert.True(retrieve.Id == result.Id);
            }

            [Theory(DisplayName = "Fail_On_Missing"), AutoMoqData]
            public void Fail_On_Missing(List<Account> accounts, AccountRepository sut)
            {
                var retrieve = accounts.FirstOrDefault();

                foreach (var account in accounts)
                {
                    sut.Save(account);
                }

                Assert.Throws<InvalidOperationException>(() =>
                {
                    var result = sut.Get(Guid.NewGuid());
                });
            }
        }
    }
}
