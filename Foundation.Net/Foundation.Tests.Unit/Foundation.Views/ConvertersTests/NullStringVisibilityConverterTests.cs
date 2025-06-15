//-----------------------------------------------------------------------
// <copyright file="NullStringVisibilityConverterTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using NUnit.Framework;

using Foundation.Views;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Views.ConvertersTests
{
    /// <summary>
    /// The unit test NullStringVisibilityConverterTests class
    /// </summary>
    [TestFixture]
    public class NullStringVisibilityConverterTests : UnitTestBase
    {
        private IValueConverter GetConverter()
        {
            return new NullStringVisibilityConverter();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase(null, Visibility.Collapsed)]
        [TestCase("   ", Visibility.Collapsed)]
        [TestCase("Some value/random text", Visibility.Visible)]
        public void TestConvert(Object value, Visibility expectedValue)
        {
            IValueConverter converter = GetConverter();
            const Type targetType = null;
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Empty()
        {
            IValueConverter converter = GetConverter();
            const Type targetType = null;
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = String.Empty;
            Object expectedValue = Visibility.Collapsed;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the ConvertBack method.
        /// </summary>
        [TestCase(Visibility.Collapsed, null)]
        [TestCase(Visibility.Hidden, null)]
        [TestCase(Visibility.Visible, null)]
        public void TestConvertBack(Visibility value, String expectedValue)
        {
            IValueConverter converter = GetConverter();
            const Type targetType = null;
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
