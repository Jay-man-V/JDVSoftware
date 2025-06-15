//-----------------------------------------------------------------------
// <copyright file="DateTimeToStringMultiConverterTests.cs" company="JDV Software Ltd">
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
    /// The unit test DateTimeToStringMultiConverterTests class
    /// </summary>
    [TestFixture]
    public class DateTimeToStringMultiConverterTests : UnitTestBase
    {
        readonly DateTime _targetDateTime = new DateTime(2021, 1, 10, 20, 56, 25);
        private IMultiValueConverter GetConverter()
        {
            return new DateTimeToStringMultiConverter();
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Null()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            const Object[] value = null;
            Object expectedValue = "Select Date";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_EmptyArray()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { };
            Object expectedValue = "Select Date";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_OneElement()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { _targetDateTime };
            Object expectedValue = "Select Date";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_ThreeElement()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { _targetDateTime, String.Empty, String.Empty };
            Object expectedValue = "Select Date";

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Valid()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { _targetDateTime, Formats.DotNet.DateTimeSeconds };
            Object expectedValue = _targetDateTime.ToString(Formats.DotNet.DateTimeSeconds);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_DateEmptyFormat()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { _targetDateTime, String.Empty };
            Object expectedValue = _targetDateTime.ToString(Formats.DotNet.DateTimeSeconds);

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

            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { "None date value", Formats.DotNet.DateTimeSeconds };
            Object expectedValue = dateTimeService.SystemDateTimeNow.ToString(Formats.DotNet.DateTimeSeconds);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Value_NoneDateEmptyFormat()
        {
            IDateTimeService dateTimeService = CoreInstance.Container.Get<IDateTimeService>();

            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(String);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { "None date value", String.Empty };
            Object expectedValue = dateTimeService.SystemDateTimeNow.ToString(Formats.DotNet.DateTimeSeconds);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Null()
        {
            IMultiValueConverter converter = GetConverter();
            Type[] targetType = { typeof(DateTime) };
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { };
            const Object expectedValue = null;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }
    }
}
