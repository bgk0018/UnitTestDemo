using Domain;
using Domain.ValueObjects;
using DomainTests.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainTests
{
    //Below shows anonymized data using Autofixture + xUnit Attribute
    //Parameters passed into tests are generated via Fixture class through the AutoMoqDataAttribute
    //These are explicitly unit tests, this shows a ValueObject set of unit tests
    //Refer to ServiceTests for Mocking usage
    public class MoneyTests
    {
        public class TheConstructorMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input(decimal amount, Currency currency)
            {
                Money sut = new Money(amount, currency);

                Assert.True(sut.Amount == amount && sut.Currency == currency);
            }

            [Theory(DisplayName = "Fail_With_Negative_Amount"), AutoMoqData]
            public void Fail_With_Negative_Amount(Currency currency)
            {
                decimal amount = -30;

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    Money sut = new Money(amount, currency);
                });
            }
        }
    }
}
