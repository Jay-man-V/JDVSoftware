//-----------------------------------------------------------------------
// <copyright file="RequiredApplicationIdAttributeTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Resources
{
    /// <summary>
    /// Unit Tests for the Required App Id Attribute class
    /// </summary>
    [TestFixture]
    public class RequiredApplicationIdAttributeTests : UnitTestBase
    {
        [TestCase]
        public void Test_Attribute()
        {
            RequiredAppIdAttribute attr = new RequiredAppIdAttribute();

            String errorMessage = $"Application Id must be provided";
            String actualErrorMessage = attr.ErrorMessage;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));

            Assert.That(attr.IsValid(new AppId(1)));
            Assert.That(attr.IsValid(new AppId(0)), Is.EqualTo(false));
            Assert.That(attr.IsValid(new EntityId(1)), Is.EqualTo(false));
            Assert.That(attr.IsValid(new EntityId(0)), Is.EqualTo(false));
        }
    }
}
