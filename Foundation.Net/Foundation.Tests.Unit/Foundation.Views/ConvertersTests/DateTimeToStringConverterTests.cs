//-----------------------------------------------------------------------
// <copyright file="DateTimeToStringConverterTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;
using Foundation.Views;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Views.ConvertersTests
{
    /// <summary>
    /// The unit test DateTimeToStringConverterTests class
    /// </summary>
    [TestFixture]
    public class DateTimeToStringConverterTests : UnitTestBase
    {
        private readonly DateTime _targetDateTime = new DateTime(2021, 1, 10, 20, 56, 25);
        private IValueConverter GetConverter()
        {
            return new DateTimeToStringConverter();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase(null, "Select Date")]
        public void TestConvert(Object value, String expectedValue)
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_NoneDateValue()
        {
            IDateTimeService dateTimeService = CoreInstance.Container.Get<IDateTimeService>();

            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "None date value";
            Object expectedValue = dateTimeService.SystemDateTimeNow.ToString(Formats.DotNet.DateTimeSeconds);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Value_NullParameter()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = _targetDateTime;
            Object expectedValue = _targetDateTime.ToString("dd-MMM-yyyy HH:mm:ss");

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Value_Parameter()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            Object parameter = "dd-MMM-yyyy";
            const CultureInfo culture = null;

            Object value = _targetDateTime;
            Object expectedValue = _targetDateTime.ToString((String)parameter);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Null()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(DateTime);
            const Object parameter = null;
            const CultureInfo culture = null;

            const Object value = null;
            const Object expectedValue = null;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_InvalidDate()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(DateTime);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = "Invalid Date";
            const Object expectedValue = null;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Date()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(DateTime);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = _targetDateTime.ToString(Formats.DotNet.DateOnly);
            Object expectedValue = _targetDateTime.Date;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_DateTime()
        {
            IValueConverter converter = GetConverter();
            Type targetType = typeof(DateTime);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object value = _targetDateTime.ToString(Formats.DotNet.DateTimeSeconds);
            Object expectedValue = _targetDateTime;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
