﻿//-----------------------------------------------------------------------
// <copyright file="IsBetweenNullableTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExtensionMethodsTests.Int32ExtensionMethodsTests
{
    /// <summary>
    /// The Int32 Extension Methods tests class
    /// </summary>
    [TestFixture]
    public class IsBetweenNullableTests : UnitTestBase
    {
        [TestCase]
        public void TestIsBetween_Null()
        {
            Int32? targetValue = null;
            Int32 startValue = 2000;
            Int32 endValue = 2100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(false));
        }

        [TestCase]
        public void TestIsBetween_True()
        {
            Int32? targetValue = 2010;
            Int32 startValue = 2000;
            Int32 endValue = 2100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_False()
        {
            Int32? targetValue = 2010;
            Int32 startValue = 2100;
            Int32 endValue = 2200;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(false));
        }

        [TestCase]
        public void TestIsBetween_SameValue()
        {
            Int32? targetValue = 2010;
            Int32 startValue = 2010;
            Int32 endValue = 2010;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_StartSameValue()
        {
            Int32? targetValue = 2010;
            Int32 startValue = 2010;
            Int32 endValue = 2100;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }

        [TestCase]
        public void TestIsBetween_EndSameValue()
        {
            Int32? targetValue = 2010;
            Int32 startValue = 2000;
            Int32 endValue = 2010;

            Boolean actualResult = targetValue.IsBetween(startValue, endValue);

            Assert.That(actualResult, Is.EqualTo(true));
        }
    }
}
