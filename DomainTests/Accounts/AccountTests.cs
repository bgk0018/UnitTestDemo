using System;
using System.Linq;
using Domain.Accounts;
using Domain.Tests.Framework.AutoData;
using Domain.Tests.Framework.Categories;
using Domain.ValueObjects;
using Xunit;

namespace Domain.Tests.Accounts
{
    //Below shows anonymized data using Autofixture + xUnit Attribute
    //Parameters passed into tests are generated via Fixture class through the AutoMoqDataAttribute
    //These are explicitly unit tests, this shows a Domain Entity set of unit tests
    //Refer to ServiceTests for Mocking usage
    public class AccountTests
    {
        [UnitTest]
        public class TheConstructorMethod
        {
            //Autofixture defaults to generating positive numbers by default, thus why we can just take the anonymized amount
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(AccountNumber number, decimal amount, Currency currency)
            {
                var sut = new Account(number, amount, currency);

                Assert.True(sut.Balance == amount && sut.Number == number && sut.CurrencyType == currency);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Negative_Amount(AccountNumber number, Currency currency)
            {
                decimal amount = -30;

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var sut = new Account(number, amount, currency);
                });
            }
        }

        [UnitTest]
        public class TheWithdrawMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(Account sut)
            {
                var money = new Money(sut.Balance, sut.CurrencyType);

                sut.Withdraw(money);

                Assert.True(sut.Balance == 0);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Amount_Exceeds_Balance(Account sut)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var money = new Money(sut.Balance + 1, sut.CurrencyType);

                    sut.Withdraw(money);
                });
            }

            [Theory, AutoMoqData]
            public void Fail_With_Incorrect_Currency(Account sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var differentCurrency = Enum
                                        .GetValues(typeof(Currency))
                                        .Cast<Currency>()
                                        .FirstOrDefault(p => p != sut.CurrencyType);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var money = new Money(sut.Balance, differentCurrency);

                    sut.Withdraw(money);
                });
            }
        }

        [UnitTest]
        public class TheDepositMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(Account sut, decimal amount)
            {
                var initialBalance = sut.Balance;

                var money = new Money(amount, sut.CurrencyType);

                sut.Deposit(money);

                Assert.True(sut.Balance == initialBalance + amount);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Incorrect_Currency(Account sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var differentCurrency = Enum
                                        .GetValues(typeof(Currency))
                                        .Cast<Currency>()
                                        .FirstOrDefault(p => p != sut.CurrencyType);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var money = new Money(sut.Balance, differentCurrency);

                    sut.Deposit(money);
                });
            }
        }
    }
}