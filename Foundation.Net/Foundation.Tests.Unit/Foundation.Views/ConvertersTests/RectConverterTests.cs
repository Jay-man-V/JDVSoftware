//-----------------------------------------------------------------------
// <copyright file="RectConverterTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using NUnit.Framework;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using FCONV = Foundation.Views;

namespace Foundation.Tests.Unit.Foundation.Views.ConvertersTests
{
    /// <summary>
    /// The unit test RectConverterTests class
    /// </summary>
    [TestFixture]
    public class RectConverterTests : UnitTestBase
    {
        private IMultiValueConverter GetConverter()
        {
            return new FCONV.RectConverter();
        }

        private void CompareRectangles(Rect r1, Rect r2)
        {
            Assert.That(r2.Top, Is.EqualTo(r1.Top));
            Assert.That(r2.Bottom, Is.EqualTo(r1.Bottom));
            Assert.That(r2.Left, Is.EqualTo(r1.Left));
            Assert.That(r2.Right, Is.EqualTo(r1.Right));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Null()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            const Object[] value = null;
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_EmptyArray()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { };
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_OneElement()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { 1 };
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_ThreeElement()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { 1, 2, 3 };
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_Valid()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { 5, 10 };
            Object expectedValue = new Rect(0, 0, 5, 10);

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_EmptyValues()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { String.Empty, String.Empty };
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvert_NoneNumericValue()
        {
            IMultiValueConverter converter = GetConverter();
            Type targetType = typeof(Rect);
            const Object parameter = null;
            const CultureInfo culture = null;

            Object[] value = { "A", "B" };
            Object expectedValue = new Rect();

            Object actualResult = converter.Convert(value, targetType, parameter, culture);

            CompareRectangles((Rect)expectedValue, (Rect)actualResult);
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Null()
        {
            IMultiValueConverter converter = GetConverter();
            Type[] targetType = { typeof(Double) };
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
        public void TestConvertBack_NonRect()
        {
            IMultiValueConverter converter = GetConverter();
            Type[] targetType = { typeof(Double) };
            const Object parameter = null;
            const CultureInfo culture = null;
            
            RandomObject value = new RandomObject();
            const Object expectedValue = null;

            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Tests the Convert method.
        /// </summary>
        [TestCase]
        public void TestConvertBack_Valid()
        {
            IMultiValueConverter converter = GetConverter();
            Type[] targetType = { typeof(Double) };
            const Object parameter = null;
            const CultureInfo culture = null;

            Rect value = new Rect (0, 0, 10, 15);
            Object[] expectedValue = { value.Width, value.Height };

            Object[] actualResult = converter.ConvertBack(value, targetType, parameter, culture);

            Assert.That(actualResult, Is.EquivalentTo(expectedValue));
        }
    }
}
