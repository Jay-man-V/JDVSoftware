﻿//-----------------------------------------------------------------------
// <copyright file="DateTimeServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Services.Application;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// The DateTime Service Tests
    /// </summary>
    [TestFixture]
    public class DateTimeServiceTests : UnitTestBase
    {
        private IDateTimeService _dateTimeService;

        protected override void StartTest()
        {
            base.StartTest();

            _dateTimeService = new DateTimeService();
        }

        [TestCase]
        public void Test_Scenario_NewValidityPeriod()
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime t = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59);
            DateTime expected = DateTime.SpecifyKind(t.AddMonths(6), DateTimeKind.Utc);

            TimeSpan endTime = new TimeSpan(23, 59, 59);
            DateTime actual = _dateTimeService.MakeUtcDateTime(_dateTimeService.SystemDateTimeNow.AddMonths(6), endTime);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_MakeUtc()
        {
            DateTime expected = new DateTime(2019, 1, 1, 20, 0, 0, DateTimeKind.Utc);

            DateTime aDate = new DateTime(2019, 1, 1, 20, 0, 0);

            DateTime actual = _dateTimeService.MakeUtcDateTime(aDate);

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [TestCase]
        public void Test_MakeUtc_TimeSpan()
        {
            DateTime expected = new DateTime(2019, 1, 1, 20, 0, 0, DateTimeKind.Utc);

            DateTime aDate = new DateTime(2019, 1, 1);
            TimeSpan endTime = new TimeSpan(20, 0, 0);

            DateTime actual = _dateTimeService.MakeUtcDateTime(aDate, endTime);

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [TestCase]
        public void Test_MakeUtc_Time()
        {
            DateTime expected = new DateTime(2019, 1, 1, 20, 0, 0, DateTimeKind.Utc);

            DateTime aDate = new DateTime(2019, 1, 1);

            DateTime actual = _dateTimeService.MakeUtcDateTime(aDate, 20, 0, 0);

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [TestCase]
        public void Test_StartOfWeek()
        {
            DayOfWeek value = DayOfWeek.Monday;
            DayOfWeek actualValue = _dateTimeService.StartOfWeek;

            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_SystemDateTimeNow()
        {
            DateTime value = DateTime.UtcNow;
            DateTime actualValue = _dateTimeService.SystemDateTimeNow;

            Assert.That(actualValue.Date, Is.EqualTo(value.Date));
            Assert.That(actualValue.TimeOfDay.Hours, Is.EqualTo(value.TimeOfDay.Hours));
            Assert.That(actualValue.TimeOfDay.Minutes, Is.EqualTo(value.TimeOfDay.Minutes));
            Assert.That(actualValue.TimeOfDay.Seconds, Is.EqualTo(value.TimeOfDay.Seconds));
            Assert.That(actualValue.TimeOfDay.Milliseconds, Is.EqualTo(value.TimeOfDay.Milliseconds));
        }

        [TestCase]
        public void Test_SystemDateTimeNowWithoutMilliseconds()
        {
            DateTime value = DateTime.UtcNow;
            DateTime actualValue = _dateTimeService.SystemDateTimeNowWithoutMilliseconds;

            Assert.That(actualValue.Date, Is.EqualTo(value.Date));
            Assert.That(actualValue.TimeOfDay.Hours, Is.EqualTo(value.TimeOfDay.Hours));
            Assert.That(actualValue.TimeOfDay.Minutes, Is.EqualTo(value.TimeOfDay.Minutes));
            Assert.That(actualValue.TimeOfDay.Seconds, Is.EqualTo(value.TimeOfDay.Seconds));
        }

        [TestCase]
        public void Test_StartOfMonth()
        {
            int year = _dateTimeService.SystemDateTimeNow.Year;
            int month = _dateTimeService.SystemDateTimeNow.Month;

            DateTime value = new DateTime(year, month, 1);
            DateTime actualValue = _dateTimeService.StartOfMonth;

            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_EndOfMonth()
        {
            int year = _dateTimeService.SystemDateTimeNow.Year;
            int month = _dateTimeService.SystemDateTimeNow.Month;

            DateTime value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            DateTime actualValue = _dateTimeService.EndOfMonth;

            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_StartOfLastMonth()
        {
            int year = _dateTimeService.SystemDateTimeNow.Year;
            int month = _dateTimeService.SystemDateTimeNow.Month;

            DateTime value = new DateTime(year, month, 1).AddMonths(-1);
            DateTime actualValue = _dateTimeService.StartOfLastMonth;

            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_EndOfLastMonth()
        {
            int year = _dateTimeService.SystemDateTimeNow.Year;
            int month = _dateTimeService.SystemDateTimeNow.Month - 1;

            if (month == 0)
            {
                year--;
                month = 12;
            }

            DateTime value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            DateTime actualValue = _dateTimeService.EndOfLastMonth;

            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_GetStartOfMonth_DateTime()
        {
            DateTime targetDate = new DateTime(2020, 6, 27);
            DateTime actualValue = _dateTimeService.GetStartOfMonth(targetDate);

            DateTime value = new DateTime(2020, 6, 1);
            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_GetStartOfMonth_YearMonth()
        {
            DateTime actualValue = _dateTimeService.GetStartOfMonth(2020, 6);

            DateTime value = new DateTime(2020, 6, 1);
            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_GetEndOfMonth_DateTime()
        {
            DateTime targetDate = new DateTime(2020, 6, 27);
            DateTime actualValue = _dateTimeService.GetEndOfMonth(targetDate);

            DateTime value = new DateTime(2020, 6, 30);
            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_GetEndOfMonth_YearMonth()
        {
            DateTime actualValue = _dateTimeService.GetEndOfMonth(2020, 6);

            DateTime value = new DateTime(2020, 6, 30);
            Assert.That(actualValue, Is.EqualTo(value));
        }

        [TestCase]
        public void Test_GetPreviousQuarter()
        {
            DateTimeService dateTimeService = new DateTimeService
            {
                SystemDateTimeNow = new DateTime(2024, 05, 10)
            };
            DateTime startOfQuarter = new DateTime(2024, 01, 01);
            DateTime endOfQuarter = new DateTime(2024, 03, 31);

            DateTime actualStartOfQuarter = dateTimeService.GetStartOfPreviousQuarter();
            DateTime actualEndOfQuarter = dateTimeService.GetEndOfPreviousQuarter();

            Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter));
            Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter));
        }

        [TestCase]
        public void Test_GetPreviousQuarter_Loop()
        {
            DateTime startDate = new DateTime(2023, 01, 01);
            DateTime endDate = new DateTime(2025, 12, 31);

            for (DateTime dateLoop = startDate; dateLoop <= endDate; dateLoop = dateLoop.AddDays(1))
            {
                DateTimeService dateTimeService = new DateTimeService
                {
                    SystemDateTimeNow = new DateTime(dateLoop.Year, dateLoop.Month, dateLoop.Day)
                };

                DateTime startOfQuarter = DateTime.MinValue;
                DateTime endOfQuarter = DateTime.MaxValue;

                if (dateLoop.Month.IsBetween(1, 3))
                {
                    startOfQuarter = new DateTime(dateLoop.Year - 1, 10, 01);
                    endOfQuarter = new DateTime(dateLoop.Year - 1, 12, 31);
                }
                if (dateLoop.Month.IsBetween(4, 6))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 01, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 03, 31);
                }
                if (dateLoop.Month.IsBetween(7, 9))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 04, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 06, 30);
                }
                if (dateLoop.Month.IsBetween(10, 12))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 07, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 09, 30);
                }

                DateTime actualStartOfQuarter = dateTimeService.GetStartOfPreviousQuarter();
                DateTime actualEndOfQuarter = dateTimeService.GetEndOfPreviousQuarter();

                Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
                Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
            }
        }

        [TestCase]
        public void Test_GetCurrentQuarter_SingleValue()
        {
            DateTimeService dateTimeService = new DateTimeService
            {
                SystemDateTimeNow = new DateTime(2024, 05, 10)
            };
            DateTime startOfQuarter = new DateTime(2024, 04, 01);
            DateTime endOfQuarter = new DateTime(2024, 06, 30);

            DateTime actualStartOfQuarter = dateTimeService.GetStartOfCurrentQuarter();
            DateTime actualEndOfQuarter = dateTimeService.GetEndOfCurrentQuarter();

            Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter));
            Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter));
        }

        [TestCase]
        public void Test_GetCurrentQuarter_Loop()
        {
            DateTime startDate = new DateTime(2023, 01, 01);
            DateTime endDate = new DateTime(2025, 12, 31);

            for (DateTime dateLoop = startDate; dateLoop <= endDate; dateLoop = dateLoop.AddDays(1))
            {
                DateTimeService dateTimeService = new DateTimeService
                {
                    SystemDateTimeNow = new DateTime(dateLoop.Year, dateLoop.Month, dateLoop.Day)
                };

                DateTime startOfQuarter = DateTime.MinValue;
                DateTime endOfQuarter = DateTime.MaxValue;

                if (dateLoop.Month.IsBetween(1, 3))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 01, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 03, 31);
                }
                if (dateLoop.Month.IsBetween(4, 6))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 04, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 06, 30);
                }
                if (dateLoop.Month.IsBetween(7, 9))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 07, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 09, 30);
                }
                if (dateLoop.Month.IsBetween(10, 12))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 10, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 12, 31);
                }

                DateTime actualStartOfQuarter = dateTimeService.GetStartOfCurrentQuarter();
                DateTime actualEndOfQuarter = dateTimeService.GetEndOfCurrentQuarter();

                Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
                Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
            }
        }

        [TestCase]
        public void Test_GetNextQuarter()
        {
            DateTimeService dateTimeService = new DateTimeService
            {
                SystemDateTimeNow = new DateTime(2024, 05, 10)
            };
            DateTime startOfQuarter = new DateTime(2024, 07, 01);
            DateTime endOfQuarter = new DateTime(2024, 09, 30);

            DateTime actualStartOfQuarter = dateTimeService.GetStartOfNextQuarter();
            DateTime actualEndOfQuarter = dateTimeService.GetEndOfNextQuarter();

            Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter));
            Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter));
        }

        [TestCase]
        public void Test_GetNextQuarter_Loop()
        {
            DateTime startDate = new DateTime(2023, 01, 01);
            DateTime endDate = new DateTime(2025, 12, 31);

            for (DateTime dateLoop = startDate; dateLoop <= endDate; dateLoop = dateLoop.AddDays(1))
            {
                DateTimeService dateTimeService = new DateTimeService
                {
                    SystemDateTimeNow = new DateTime(dateLoop.Year, dateLoop.Month, dateLoop.Day)
                };

                DateTime startOfQuarter = DateTime.MinValue;
                DateTime endOfQuarter = DateTime.MaxValue;

                if (dateLoop.Month.IsBetween(1, 3))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 04, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 06, 30);
                }
                if (dateLoop.Month.IsBetween(4, 6))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 07, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 09, 30);
                }
                if (dateLoop.Month.IsBetween(7, 9))
                {
                    startOfQuarter = new DateTime(dateLoop.Year, 10, 01);
                    endOfQuarter = new DateTime(dateLoop.Year, 12, 31);
                }
                if (dateLoop.Month.IsBetween(10, 12))
                {
                    startOfQuarter = new DateTime(dateLoop.Year + 1, 01, 01);
                    endOfQuarter = new DateTime(dateLoop.Year + 1, 03, 31);
                }

                DateTime actualStartOfQuarter = dateTimeService.GetStartOfNextQuarter();
                DateTime actualEndOfQuarter = dateTimeService.GetEndOfNextQuarter();

                Assert.That(actualStartOfQuarter, Is.EqualTo(startOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
                Assert.That(actualEndOfQuarter, Is.EqualTo(endOfQuarter), dateLoop.ToString("yyyy-MMM-dd"));
            }
        }
    }
}
