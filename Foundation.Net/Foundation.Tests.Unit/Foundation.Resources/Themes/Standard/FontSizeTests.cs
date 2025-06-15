//-----------------------------------------------------------------------
// <copyright file="FontSizeTests.cs" company="JDV Software Ltd">
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
    /// Unit Tests for the Font Size class
    /// </summary>
    [TestFixture]
    public class FontSizeTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            Type thisType = this.GetType();
            MethodInfo[] testMethods = thisType.GetMethods();
            Int32 testMethodCount = testMethods.Count(m => m.Name.StartsWith("Test_"));

            // This test exists to ensure all the Properties are tested/checked in the next test
            Type theType = typeof(FontSize);
            PropertyInfo[] propertyInfos = theType.GetProperties();

            Assert.That(propertyInfos.Length, Is.EqualTo(testMethodCount - 1));

            Int32 index = 0;
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.HeaderFontSize)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.ScreenNameFontSize)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.ScreenInstructionsFontSize)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.DefaultFontSize)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.SmallHeaderFontSize)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(FontSize.SmallContentFontSize)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_HeaderFontSize()
        {
            Assert.That(FontSize.HeaderFontSize, Is.EqualTo(20));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ScreenNameFontSize()
        {
            Assert.That(FontSize.ScreenNameFontSize, Is.EqualTo(18));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ScreenInstructionsFontSize()
        {
            Assert.That(FontSize.ScreenInstructionsFontSize, Is.EqualTo(14));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_DefaultFontSize()
        {
            Assert.That(FontSize.DefaultFontSize, Is.EqualTo(15));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_SmallHeaderFontSize()
        {
            Assert.That(FontSize.SmallHeaderFontSize, Is.EqualTo(12));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_SmallContentFontSize()
        {
            Assert.That(FontSize.SmallContentFontSize, Is.EqualTo(10));
        }
    }
}
