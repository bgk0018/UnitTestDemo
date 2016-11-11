using Domain;
using Domain.Accounts;
using DomainTests.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestDemo.Tests
{
    public class AccountFactoryTests
    {
        public class TheCreateMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input(Currency currency, AccountFactory sut)
            {
                var account = sut.Create(currency);

                Assert.True(account.Balance == 0 && account.CurrencyType == currency && account.Id != Guid.Empty);
            }
        }
    }
}
