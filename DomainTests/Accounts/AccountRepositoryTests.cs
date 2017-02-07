using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Accounts;
using Domain.Tests.Framework.AutoData;
using Domain.ValueObjects;
using Xunit;
using Domain.Tests.Framework.Categories;

namespace Domain.Tests.Accounts
{
    public class AccountRepositoryTests
    {
        [UnitTest]
        public class TheSaveMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Add(Account account,
                                                    AccountRepository sut)
            {
                sut.Save(account);
            }

            [Theory, AutoMoqData]
            public void Succeed_With_Update(decimal amount,
                                            Account account,
                                            AccountRepository sut)
            {
                sut.Save(account);
                var persistedAccount = sut.Get(account.Number);
                Money money = new Money(amount, account.CurrencyType);

                persistedAccount.Deposit(money);

                sut.Save(account);
            }

            [Theory, AutoMoqData]
            public void Fail_On_Null_Account(AccountRepository sut)
            {
                Account nullAccount = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    sut.Save(nullAccount);
                });
            }
        }

        [UnitTest]
        public class TheGetMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_On_Valid_Input(List<Account> accounts, AccountRepository sut)
            {
                var retrieve = accounts.FirstOrDefault();

                foreach (var account in accounts)
                {
                    sut.Save(account);
                }

                var result = sut.Get(retrieve.Number);

                Assert.True(retrieve.Number == result.Number);
            }

            //This test could potentially fail since it is possible that
            //the number we get from autofixture thats been randomly generated somehow
            //is also generated in the account list
            //if we wanted this test to be more robust, we'd probably first check
            //and see if our missing number is in the generated list and augment it until
            //its not in the list
            [Theory, AutoMoqData]
            public void Fail_On_Missing(AccountNumber missingNumber, List<Account> accounts, AccountRepository sut)
            {
                var retrieve = accounts.FirstOrDefault();

                foreach (var account in accounts)
                {
                    sut.Save(account);
                }

                Assert.Throws<InvalidOperationException>(() =>
                {
                    var result = sut.Get(missingNumber);
                });
            }
        }
    }
}