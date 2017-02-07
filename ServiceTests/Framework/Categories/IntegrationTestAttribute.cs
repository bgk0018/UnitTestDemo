using System;
using Xunit.Sdk;

namespace Service.Tests.Framework.Categories
{
    /// <summary>
    ///     Designates the category of test being performed as an Integration Test
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    [TraitDiscoverer("Service.Tests.Framework.Categories.IntegrationTestDiscoverer", "Service.Tests")]
    internal class IntegrationTestAttribute : Attribute, ITraitAttribute
    {
    }
}