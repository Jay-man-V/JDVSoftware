//-----------------------------------------------------------------------
// <copyright file="BooleanToVisibleConverterTests.cs" company="JDV Software Ltd">
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
    /// The unit test BooleanToVisibleConverterTests class
    /// </summary>
    [TestFixture]
    public class BooleanToVisibleConverterTests : UnitTestBase
    {
        private IValueConverter GetConverter()
        {
            return new BooleanToVisibilityConverter();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase(null, Visibility.Collapsed)]
        [TestCase("Non-Boolean value", Visibility.Collapsed)]
        [TestCase(true, Visibility.Visible)]
        [TestCase(false, Visibility.Collapsed)]
        [TestCase("true", Visibility.Visible)]
        [TestCase("false", Visibility.Collapsed)]
        public void TestConvert(Object value, Visibility expectedValue)
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Visibility);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the ConvertBack method.
        /// </summary>
        [TestCase(Visibility.Collapsed, false)]
        [TestCase(Visibility.Visible, true)]
        [TestCase(Visibility.Hidden, false)]
        public void TestConvertBack(Object value, Boolean expectedValue)
        {
            IValueConverter convertBack = GetConverter();
            Type targetType = typeof(Boolean);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = convertBack.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
