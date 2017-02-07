using System;
using Domain.Tests.Framework.AutoData;
using Domain.Tests.Framework.Categories;
using Domain.ValueObjects;
using Xunit;

namespace Domain.Tests.ValueObjects
{
    //Below shows anonymized data using Autofixture + xUnit Attribute
    //Parameters passed into tests are generated via Fixture class through the AutoMoqDataAttribute
    //These are explicitly unit tests, this shows a ValueObject set of unit tests
    //Refer to ServiceTests for Mocking usage
    public class MoneyTests
    {
        [UnitTest]
        public class TheConstructorMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(decimal amount, Currency currency)
            {
                var sut = new Money(amount, currency);

                Assert.True(sut.Amount == amount && sut.Currency == currency);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Negative_Amount(Currency currency)
            {
                decimal amount = -30;

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var sut = new Money(amount, currency);
                });
            }
        }
    }
}