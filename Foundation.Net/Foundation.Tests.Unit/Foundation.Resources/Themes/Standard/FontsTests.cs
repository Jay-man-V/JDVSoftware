//-----------------------------------------------------------------------
// <copyright file="FontsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Resources.Themes.Standard
{
    /// <summary>
    /// Unit Tests for the Fonts class
    /// </summary>
    [TestFixture]
    public class FontsTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            Type thisType = this.GetType();
            MethodInfo[] testMethods = thisType.GetMethods();
            Int32 testMethodCount = testMethods.Count(m => m.Name.StartsWith("Test_"));

            // This test exists to ensure all the Properties are tested/checked in the next test
            Type theType = typeof(Fonts);
            PropertyInfo[] propertyInfos = theType.GetProperties();

            Assert.That(propertyInfos.Length, Is.EqualTo(testMethodCount - 1));

            Int32 index = 0;
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(Fonts.DefaultApplicationFontFamily)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(Fonts.DefaultFixedFontFamily)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DefaultApplicationFontFamily()
        {
            Assert.That(Fonts.DefaultApplicationFontFamily.Name, Is.EqualTo("Segoe UI"));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DefaultFixedFontFamily()
        {
            Assert.That(Fonts.DefaultFixedFontFamily.Name, Is.EqualTo("Lucida Console"));
        }
    }
}
