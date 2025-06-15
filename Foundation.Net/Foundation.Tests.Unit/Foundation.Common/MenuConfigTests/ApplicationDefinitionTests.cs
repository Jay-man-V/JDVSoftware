//-----------------------------------------------------------------------
// <copyright file="ApplicationDefinitionTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.MenuConfigTests
{
    /// <summary>
    /// The Application Definition Tests class
    /// </summary>
    [TestFixture]
    public class ApplicationDefinitionTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ApplicationDefinition"/>
        /// </summary>
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetInstancePropertyInfosForType(typeof(ApplicationDefinition));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationDefinition.Name)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationDefinition.ViewMenuItems)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Constructor()
        {
            ApplicationDefinition obj = new ApplicationDefinition();

            Assert.That(obj.Name, Is.EqualTo(String.Empty));
            Assert.That(obj.ViewMenuItems, Is.Not.EqualTo(null));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Clone()
        {
            ApplicationDefinition obj = new ApplicationDefinition
            {
                Name = "Name",
            };

            ApplicationDefinition clone = obj.Clone() as ApplicationDefinition;

            Assert.That(obj, Is.Not.EqualTo(clone));

            Assert.That(obj.Name, Is.EqualTo(clone.Name));
            Assert.That(obj.ViewMenuItems, Is.Not.SameAs(clone.ViewMenuItems));
        }
    }
}
