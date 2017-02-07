using System;
using Domain.Tests.Framework.Categories;
using Domain.ValueObjects;
using Xunit;
using Domain.Tests.Framework.AutoData;

namespace Domain.Tests.ValueObjects
{

    //I'm sort of cheating here since I'm having autofixture give me a valid accountNumber
    //thus implicitly testing the constructor.
    //I'm not sure the best way to go about
    //dealing with invariants at a primitive level
    //We could used SpecimenBuilders but that feels very grey and
    //I would be afraid that doing things like depending on the name
    //and type to return a particular string could cause leaking into
    //other unrelated tests
    public class AccountNumberTests
    {
        [UnitTest]
        public class TheConstructorMethod
        {
            [Theory, AutoMoqData]
            public void Succeeds_With_Valid_Input(AccountNumber seedNumber)
            {
                var sut = new AccountNumber(seedNumber.Value);

                Assert.True(sut == seedNumber);
            }

            [Fact]
            public void Throws_With_Empty_String()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var sut = new AccountNumber(string.Empty);
                });
            }

            //There should be a boat load more unit tests here
            
            //At least one for each of the invariants on the AccountNumber value

            /*
             * 
             if (number == null)
                throw new ArgumentNullException("number", "AccountNumber cannot be null");

            if(number.Length != 10)
                throw new ArgumentOutOfRangeException("number", number, "Must be exactly 10 characters");

            if(!number.Substring(0, 8).All(char.IsDigit))
                throw new ArgumentException("All characters before the hyphen need to be digits");

            if(number[8] != '-')
                throw new ArgumentException("Position 9 in the AccountNumber needs to be a hyphen");

            if (!char.IsDigit(number[9]))
                throw new ArgumentException("Character following hyphen needs to be a digit");

            if (Convert.ToInt32(number[9].ToString()) > 6)
                throw new ArgumentException("Position 10 in the AccountNumber needs to be 5 or less");
             
             */
        }
    }
}
