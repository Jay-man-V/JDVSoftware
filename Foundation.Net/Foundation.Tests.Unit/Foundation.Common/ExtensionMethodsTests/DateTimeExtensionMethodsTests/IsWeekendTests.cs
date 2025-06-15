﻿//-----------------------------------------------------------------------
// <copyright file="IsWeekendTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExtensionMethodsTests.DateTimeExtensionMethodsTests
{
    /// <summary>
    /// The DateTime Extension tests
    /// </summary>
    [TestFixture]
    public class IsWeekendTests : UnitTestBase
    {
        [TestCase]
        public void TestIsWeekend_True()
        {
            DateTime startValue = new DateTime(2019, 12, 27, 0, 0, 0);

            Boolean actualResult = startValue.IsWeekend();

            Assert.That(actualResult, Is.EqualTo(false));
        }

        [TestCase]
        public void TestIsWeekend_False()
        {
            DateTime startValue = new DateTime(2019, 12, 28, 0, 0, 0);

            Boolean actualResult = startValue.IsWeekend();

            Assert.That(actualResult, Is.EqualTo(true));
        }
    }
}
