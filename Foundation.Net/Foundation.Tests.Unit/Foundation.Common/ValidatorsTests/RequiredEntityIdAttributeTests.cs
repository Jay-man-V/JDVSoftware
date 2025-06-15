//-----------------------------------------------------------------------
// <copyright file="RequiredEntityIdAttributeTests.cs" company="JDV Software Ltd">
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
    /// Unit Tests for the Required Entity Id Attribute class
    /// </summary>
    [TestFixture]
    public class RequiredEntityIdAttributeTests : UnitTestBase
    {
        [TestCase]
        public void Test_Attribute()
        {
            String entityName = "TestEntity";
            RequiredEntityIdAttribute attr = new RequiredEntityIdAttribute
            {
                EntityName = entityName
            };

            Assert.That(attr.EntityName, Is.EqualTo(entityName));

            String errorMessage = $"{entityName} Id must be provided";
            String actualErrorMessage = attr.ErrorMessage;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));

            Assert.That(attr.IsValid(new EntityId(1)));
            Assert.That(attr.IsValid(new EntityId(0)), Is.EqualTo(false));
            Assert.That(attr.IsValid(new AppId(1)), Is.EqualTo(false));
            Assert.That(attr.IsValid(new AppId(0)), Is.EqualTo(false));

        }
    }
}
