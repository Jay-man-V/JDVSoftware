//-----------------------------------------------------------------------
// <copyright file="CalendarProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.System.Support;

namespace Foundation.Tests.System.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for Calendar Process Tests
    /// </summary>
    [TestFixture]
    public class CalendarProcessTests : SystemTestBase
    {
        private ICalendarProcess CalendarProcess { get; set; }

        protected override void StartTest()
        {
            base.StartTest();

            CalendarProcess = Core.Core.Instance.Container.Get<ICalendarProcess>();
        }

        [TestCase("GB", "2025-12-24", false)]
        [TestCase("GB", "2025-12-25", true)]
        public void Test_IsHoliday(String countryCode, String dateString, Boolean expected)
        {
            DateTime testDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Boolean actual = CalendarProcess.IsHoliday(countryCode, testDate);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
