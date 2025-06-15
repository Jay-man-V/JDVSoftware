//-----------------------------------------------------------------------
// <copyright file="InvertBoolConverterTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

using NUnit.Framework;

using Foundation.Views;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Views.ConvertersTests
{
    /// <summary>
    /// The unit test InvertBoolConverterTests class
    /// </summary>
    [TestFixture]
    public class InvertBoolConverterTests : UnitTestBase
    {
        private IValueConverter GetConverter()
        {
            return new InvertBooleanConverter();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase(null, false)]
        [TestCase("None Boolean value", false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase("true", false)]
        [TestCase("false", true)]
        public void TestConvert(Object inputValue, Boolean expectedValue)
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Boolean);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = converter.Convert(inputValue, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the ConvertBack method.
        /// </summary>
        [TestCase(null, false)]
        [TestCase("None Boolean value", false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase("true", false)]
        [TestCase("false", true)]
        public void TestConvertBack(Object inputValue, Boolean expectedValue)
        {
            IValueConverter convertBack = GetConverter();
            Type targetType = typeof(Boolean);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = convertBack.ConvertBack(inputValue, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
