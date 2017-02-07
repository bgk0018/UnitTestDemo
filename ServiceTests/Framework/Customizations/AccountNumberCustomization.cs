using System;
using Domain.ValueObjects;
using Ploeh.AutoFixture;

namespace Service.Tests.Framework.Customizations
{
    internal class AccountNumberCustomization : ICustomization
    {
        private readonly Random rand;

        public AccountNumberCustomization()
        {
            rand = new Random();
        }

        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var accountNumber = rand.Next(0, 99999999).ToString().PadLeft(8, '0');

                accountNumber = accountNumber + "-" + rand.Next(0, 5);

                return new AccountNumber(accountNumber);
            });
        }
    }
}
