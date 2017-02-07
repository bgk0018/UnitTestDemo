using Ploeh.AutoFixture;
using Domain.Tests.Framework.Customizations;
using Ploeh.AutoFixture.AutoMoq;

namespace Domain.Tests.Framework.AutoData
{
    internal class TestingConventions : CompositeCustomization
    {
        internal TestingConventions()
            : base(new AccountNumberCustomization(),
                       new AutoMoqCustomization())
        {

        }
    }
}
