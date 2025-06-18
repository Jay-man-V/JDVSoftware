//-----------------------------------------------------------------------
// <copyright file="ScheduleNextRunTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.ScheduledProcessBaseTests
{
    /// <summary>
    /// Summary description for SchedulerTests
    /// </summary>
    [TestFixture]
    public class ScheduleNextRunTests : UnitTestBase
    {
        private LogId _logId;

        private ICalendarProcess CalendarProcess { get; set; }

        protected override void StartTest()
        {
            base.StartTest();

            CalendarProcess = Substitute.For<ICalendarProcess>();

            EventLogProcess = Substitute.For<IEventLogProcess>();
            EventLogProcess.StartTask(Arg.Any<AppId>(), Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>()).Returns(new LogId(123));
            EventLogProcess.CreateLogEntry(Arg.Any<LogId>(), Arg.Any<AppId>(), Arg.Any<LogSeverity>(), Arg.Any<Exception>()).Returns(new LogId(456));
            _logId = new LogId(123);

            Repository = Substitute.For<IScheduledJobRepository>();
            StatusRepository = Substitute.For<IStatusRepository>();
            UserProfileRepository = Substitute.For<IUserProfileRepository>();
            ScheduleIntervalProcess = Substitute.For<IScheduleIntervalProcess>();
        }

        protected IScheduledJobRepository Repository { get; set; }
        protected IEventLogProcess EventLogProcess { get; set; }
        protected IScheduleIntervalProcess ScheduleIntervalProcess { get; set; }

        [TestCase(ScheduleInterval.NotSet, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 0, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Seconds, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Minutes, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Hours, 3, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Days, 3, "2022-11-30 09:00:00.000")]
        [TestCase(ScheduleInterval.Weeks, 3, "2022-12-18 09:00:00.000")]
        [TestCase(ScheduleInterval.Months, 3, "2023-02-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Years, 3, "2025-11-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Other, 100, "2022-11-28 09:00:00.000")]
        public void Test_ScheduleNextRun(ScheduleInterval scheduleInterval, Int32 interval, String expectedNextRunDate)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(expectedNextRunDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            LogId parentLogId = new LogId(0);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 12, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 12, 11, 54, 300));

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            DateTime nextDate = currentDate.AddDays(1);

            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + startTime).Returns(nextDate + startTime);
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + endTime).Returns(nextDate + endTime);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, true, currentDate, startTime, endTime, scheduleInterval, interval);
            DateTime checkDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            DateTime returnDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), checkDate).Returns(returnDate);

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, Repository, StatusRepository, UserProfileRepository, EventLogProcess, ScheduleIntervalProcess, CalendarProcess);

            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(parentLogId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            Assert.That(actualNextRun, Is.EqualTo(expectedNextRunDateTime));
        }

        [TestCase(ScheduleInterval.NotSet, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 0, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Seconds, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Minutes, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Hours, 3, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Days, 3, "2022-11-30 09:00:00.000")]
        [TestCase(ScheduleInterval.Weeks, 3, "2022-12-18 09:00:00.000")]
        [TestCase(ScheduleInterval.Months, 3, "2023-02-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Years, 3, "2025-11-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Other, 100, "2022-11-28 09:00:00.000")]
        public void Test_RunBeforeStartTime(ScheduleInterval scheduleInterval, Int32 interval, String expectedNextRunDate)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(expectedNextRunDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            LogId parentLogId = new LogId(0);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 04, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 12, 04, 54, 300));

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            DateTime nextDate = currentDate.AddDays(1);

            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + startTime).Returns(nextDate + startTime);
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + endTime).Returns(nextDate + endTime);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, true, currentDate, startTime, endTime, scheduleInterval, interval);
            DateTime checkDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            DateTime returnDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), checkDate).Returns(returnDate);

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, Repository, StatusRepository, UserProfileRepository, EventLogProcess, ScheduleIntervalProcess, CalendarProcess);

            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(parentLogId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            Assert.That(actualNextRun, Is.EqualTo(expectedNextRunDateTime));
        }

        [TestCase(ScheduleInterval.NotSet, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 0, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Milliseconds, 100, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Seconds, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Minutes, 30, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Hours, 3, "2022-11-28 09:00:00.000")]
        [TestCase(ScheduleInterval.Days, 3, "2022-11-30 09:00:00.000")]
        [TestCase(ScheduleInterval.Weeks, 3, "2022-12-18 09:00:00.000")]
        [TestCase(ScheduleInterval.Months, 3, "2023-02-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Years, 3, "2025-11-27 09:00:00.000")]
        [TestCase(ScheduleInterval.Other, 100, "2022-11-28 09:00:00.000")]
        public void Test_RunAfterEndTime(ScheduleInterval scheduleInterval, Int32 interval, String expectedNextRunDate)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(expectedNextRunDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            LogId parentLogId = new LogId(0);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 12, 23, 54, 300));

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            DateTime nextDate = currentDate.AddDays(1);

            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + startTime).Returns(nextDate + startTime);
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), currentDate + endTime).Returns(nextDate + endTime);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, true, currentDate, startTime, endTime, scheduleInterval, interval);
            DateTime checkDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            DateTime returnDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), checkDate).Returns(returnDate);

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, Repository, StatusRepository, UserProfileRepository, EventLogProcess, ScheduleIntervalProcess, CalendarProcess);

            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(parentLogId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            Assert.That(actualNextRun, Is.EqualTo(expectedNextRunDateTime));
        }

        [TestCase(ScheduleInterval.Seconds, 60)]
        public void Test_ScheduleNextRun_WithError(ScheduleInterval scheduleInterval, Int32 interval)
        {
            LogId parentLogId = new LogId(0);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 12, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 12, 11, 54, 300));

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, true, currentDate, startTime, endTime, scheduleInterval, interval);
            DateTime checkDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            DateTime returnDate = DateTimeService.SystemDateTimeNowWithoutMilliseconds.Add(scheduledJob.ScheduleInterval, scheduledJob.Interval, scheduledJob.StartTime).Date;
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), checkDate).Returns(returnDate);

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, Repository, StatusRepository, UserProfileRepository, EventLogProcess, ScheduleIntervalProcess, CalendarProcess);

            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            scheduledJobProcess.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            Repository.GetAllActive().Returns(new List<IScheduledJob> { scheduledJob });
            scheduledJobProcess.InitialiseJobs(Arg.Any<LogId>());
            scheduledJobProcess.StartJobs(Arg.Any<LogId>());

            Thread.Sleep(new TimeSpan(0, 1, 0));

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            Assert.That(actualNextRun, Is.EqualTo(DateTimeService.SystemDateTimeNowWithoutMilliseconds.AddSeconds(interval)));
        }
    }
}
