using System;
using Domain.Accounts;
using Domain.Tests.Framework.AutoData;
using Xunit;
using Domain.Tests.Framework.Categories;

namespace Domain.Tests.Accounts
{
    public class AccountFactoryTests
    {
        [UnitTest]
        public class TheCreateMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(Currency currency, AccountFactory sut)
            {
                var account = sut.Create(currency);

                Assert.True(account.Balance == 0 && account.CurrencyType == currency);
            }
        }
    }
}