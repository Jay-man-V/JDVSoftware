﻿//-----------------------------------------------------------------------
// <copyright file="IsBetweenNotNullableTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExtensionMethodsTests.ByteExtensionMethodsTests
{
    /// <summary>
    /// The Byte Extension Methods tests class
    /// </summary>
    [TestFixture]
    public class IsBetweenNotNullableTests : UnitTestBase
    {
        [TestCase]
        public void TestIsBetween_True()
        {
            Byte targetValue = 75;
            Byte startValue = 50;
            Byte endValue = 100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_False()
        {
            Byte targetValue = 10;
            Byte startValue = 50;
            Byte endValue = 100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(false));
        }

        [TestCase]
        public void TestIsBetween_SameValue()
        {
            Byte targetValue = 100;
            Byte startValue = 100;
            Byte endValue = 100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_StartSameValue()
        {
            Byte targetValue = 50;
            Byte startValue = 50;
            Byte endValue = 100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_EndSameValue()
        {
            Byte targetValue = 100;
            Byte startValue = 50;
            Byte endValue = 100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }
    }
}
