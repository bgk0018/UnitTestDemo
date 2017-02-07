using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace Service.Tests.Framework.AutoData
{
    internal class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture()
                .Customize(new TestingConventions()))
        {
        }
    }
}