using Domain;
using Domain.Accounts;
using Domain.ValueObjects;
using DomainTests.AutoMoq;
using System;
using System.Linq;
using Xunit;

namespace DomainTests
{
    //Below shows anonymized data using Autofixture + xUnit Attribute
    //Parameters passed into tests are generated via Fixture class through the AutoMoqDataAttribute
    //These are explicitly unit tests, this shows a Domain Entity set of unit tests
    //Refer to ServiceTests for Mocking usage
    public class AccountTests
    {
        public class TheConstructorMethod
        {
            //Autofixture defaults to generating positive numbers by default, thus why we can just take the anonymized amount
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input(Guid id, decimal amount, Currency currency)
            {
                Account sut = new Account(id, amount, currency);

                Assert.True(sut.Balance == amount && sut.Id == id && sut.CurrencyType == currency);
            }

            [Theory(DisplayName = "Fail_With_Negative_Amount"), AutoMoqData]
            public void Fail_With_Negative_Amount(Guid id, Currency currency)
            {
                decimal amount = -30;

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    Account sut = new Account(id, amount, currency);
                });
            }
        }

        public class TheWithdrawMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input(Account sut)
            {
                Money money = new Money(sut.Balance, sut.CurrencyType);

                sut.Withdraw(money);

                Assert.True(sut.Balance == 0);
            }

            [Theory(DisplayName = "Fail_With_Amount_Exceeds_Balance"), AutoMoqData]
            public void Fail_With_Amount_Exceeds_Balance(Account sut)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    Money money = new Money(sut.Balance + 1, sut.CurrencyType);

                    sut.Withdraw(money);
                });
            }

            [Theory(DisplayName = "Fail_With_Incorrect_Currency"), AutoMoqData]
            public void Fail_With_Incorrect_Currency(Account sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var differentCurrency = Enum.GetValues(typeof(Currency))
                                        .Cast<Currency>()
                                        .Where(p => p != sut.CurrencyType)
                                        .FirstOrDefault();

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    Money money = new Money(sut.Balance, differentCurrency);

                    sut.Withdraw(money);
                });
            }
        }

        public class TheDepositMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input(Account sut, decimal amount)
            {
                var initialBalance = sut.Balance;

                Money money = new Money(amount, sut.CurrencyType);

                sut.Deposit(money);

                Assert.True(sut.Balance == initialBalance + amount);
            }

            [Theory(DisplayName = "Fail_With_Incorrect_Currency"), AutoMoqData]
            public void Fail_With_Incorrect_Currency(Account sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var differentCurrency = Enum.GetValues(typeof(Currency))
                                        .Cast<Currency>()
                                        .Where(p => p != sut.CurrencyType)
                                        .FirstOrDefault();

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    Money money = new Money(sut.Balance, differentCurrency);

                    sut.Deposit(money);
                });
            }
        }
    }
}
