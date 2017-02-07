using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Service.Tests.Framework.Customizations;

namespace Service.Tests.Framework.AutoData
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
