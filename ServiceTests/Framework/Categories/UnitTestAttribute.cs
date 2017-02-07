using System;
using Xunit.Sdk;

namespace Service.Tests.Framework.Categories
{
    /// <summary>
    ///     Designates the category of test being performed as a Unit Test
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    [TraitDiscoverer("Service.Tests.Framework.Categories.UnitTestDiscoverer", "Service.Tests")]
    internal class UnitTestAttribute : Attribute, ITraitAttribute
    {
    }
}