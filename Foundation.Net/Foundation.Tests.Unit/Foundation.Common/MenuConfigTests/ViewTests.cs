//-----------------------------------------------------------------------
// <copyright file="ViewTests.cs" company="JDV Software Ltd">
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
    /// The View Tests class
    /// </summary>
    [TestFixture]
    public class ViewTests : UnitTestBase
    {
        /// <summary>
        /// This test exists to force developers to come to this class if there are any changes to the <see cref="ViewScreen"/>
        /// </summary>
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetInstancePropertyInfosForType(typeof(ViewScreen));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewScreen.AssemblyName)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ViewScreen.AssemblyType)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Constructor()
        {
            ViewScreen obj = new ViewScreen();

            Assert.That(obj.AssemblyName, Is.EqualTo(String.Empty));
            Assert.That(obj.AssemblyType, Is.EqualTo(String.Empty));
        }

        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void Test_Clone()
        {
            ViewScreen obj = new ViewScreen
            {
                AssemblyName = "AssemblyName",
                AssemblyType = "AssemblyType",
            };

            ViewScreen clone = obj.Clone() as ViewScreen;

            Assert.That(obj, Is.Not.EqualTo(clone));

            Assert.That(obj.AssemblyName, Is.EqualTo(clone.AssemblyName));
            Assert.That(obj.AssemblyType, Is.EqualTo(clone.AssemblyType));
        }
    }
}
