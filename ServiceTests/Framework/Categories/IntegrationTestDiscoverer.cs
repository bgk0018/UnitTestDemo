using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Service.Tests.Framework.Categories
{
    internal class IntegrationTestDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>("Category", "Integration");
        }
    }
}