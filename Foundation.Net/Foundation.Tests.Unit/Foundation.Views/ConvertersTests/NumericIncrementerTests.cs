//-----------------------------------------------------------------------
// <copyright file="NumericIncrementerTests.cs" company="JDV Software Ltd">
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
    /// The unit test NumericIncrementerTests class
    /// </summary>
    [TestFixture]
    public class NumericIncrementerTests : UnitTestBase
    {
        private IValueConverter GetConverter()
        {
            return new NumericIncrementer();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Null()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            const Object value = null;
            Object expectedValue = String.Empty;

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
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = String.Empty;
            Object expectedValue = String.Empty;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Spaces()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "   ";
            Object expectedValue = String.Empty;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Valid_1()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "1";
            Object expectedValue = "002";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Valid_2()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "10";
            Object expectedValue = "011";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Valid_3()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(Int32);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "123";
            String expectedValue = "124";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Valid_Int32()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Int32 value = 10;
            Object expectedValue = 9;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Valid_String()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            String value = "012";
            Object expectedValue = 11;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Null()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            const Object value = null;
            Object expectedValue = 0;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
