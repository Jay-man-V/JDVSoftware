//-----------------------------------------------------------------------
// <copyright file="ScheduleNextRunTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.System.Support;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.System.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for SchedulerTests
    /// </summary>
    [TestFixture]
    public class ScheduleNextRunTests : SystemTestBase
    {
        protected IScheduledJobRepository DataAccess { get; set; }
        protected IScheduleIntervalProcess ScheduledIntervalProcess { get; set; }
        private ICalendarProcess CalendarProcess { get; set; }

        protected override void StartTest()
        {
            ProcessName = LocationUtils.GetClassName();
            TaskName = LocationUtils.GetFunctionName();

            base.StartTest();

            CalendarProcess = Core.Core.Instance.Container.Get<ICalendarProcess>();
            DataAccess = Core.Core.Instance.Container.Get<IScheduledJobRepository>();
            //DataAccess = Substitute.For<IScheduledJobRepository>();
            StatusDataAccess = Core.Core.Instance.Container.Get<IStatusRepository>();
            UserProfileDataAccess = Core.Core.Instance.Container.Get<IUserProfileRepository>();
            ScheduledIntervalProcess = Core.Core.Instance.Container.Get<IScheduleIntervalProcess>();
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
        public void Test_ScheduleNextRun(ScheduleInterval scheduleInterval, Int32 interval, String nextRunDateString)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(nextRunDateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 12, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 12, 11, 54, 300));

            LogId logId = EventLogProcess.CreateLogEntry(ParentLogId, Arg.Any<AppId>(), BatchName, LocationUtils.GetFunctionName(), scheduleInterval.ToString(), LogSeverity.Trace, $"{scheduleInterval}. {interval}. {nextRunDateString}.");

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, EventLogProcess, ScheduledIntervalProcess, CalendarProcess);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(logId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            EventLogProcess.EndTask(logId, LogSeverity.Trace, $"Finished. {actualNextRun:yyyy-MM-dd hh:mm:ss.fff} ");

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
        public void Test_RunBeforeStartTime(ScheduleInterval scheduleInterval, Int32 interval, String nextRunDateString)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(nextRunDateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 4, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 4, 11, 54, 300));

            LogId logId = EventLogProcess.CreateLogEntry(ParentLogId, Arg.Any<AppId>(), BatchName, LocationUtils.GetFunctionName(), scheduleInterval.ToString(), LogSeverity.Trace, $"{scheduleInterval}. {interval}. {nextRunDateString}.");

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, EventLogProcess, ScheduledIntervalProcess, CalendarProcess);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(logId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            EventLogProcess.EndTask(logId, LogSeverity.Trace, $"Finished. {actualNextRun:yyyy-MM-dd hh:mm:ss.fff} ");

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
        public void Test_RunAfterEndTime(ScheduleInterval scheduleInterval, Int32 interval, String nextRunDateString)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(nextRunDateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 23, 11, 54, 300));

            LogId logId = EventLogProcess.CreateLogEntry(ParentLogId, Arg.Any<AppId>(), BatchName, LocationUtils.GetFunctionName(), scheduleInterval.ToString(), LogSeverity.Trace, $"{scheduleInterval}. {interval}. {nextRunDateString}.");

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, EventLogProcess, ScheduledIntervalProcess, CalendarProcess);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(logId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            EventLogProcess.EndTask(logId, LogSeverity.Trace, $"Finished. {actualNextRun:yyyy-MM-dd hh:mm:ss.fff} ");

            Assert.That(actualNextRun, Is.EqualTo(expectedNextRunDateTime));
        }

        [TestCase(ScheduleInterval.Seconds, 30, "2022-11-27 23:12:24.000")]
        public void Test_ScheduleNextRun_WithError(ScheduleInterval scheduleInterval, Int32 interval, String nextRunDateString)
        {
            DateTime expectedNextRunDateTime = DateTime.ParseExact(nextRunDateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 23, 11, 54, 300));

            LogId logId = EventLogProcess.CreateLogEntry(ParentLogId, Arg.Any<AppId>(), BatchName, LocationUtils.GetFunctionName(), scheduleInterval.ToString(), LogSeverity.Trace, $"{scheduleInterval}. {interval}. {nextRunDateString}.");

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJobProcess scheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, EventLogProcess, ScheduledIntervalProcess, CalendarProcess);

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            scheduledJob.ScheduledTask = scheduledJobProcess.CreateScheduledTask(scheduledJob);

            scheduledJobProcess.RunJob(logId, scheduledJob);

            DateTime actualNextRun = scheduledJob.NextRunDateTime;

            EventLogProcess.EndTask(logId, LogSeverity.Trace, $"Finished. {actualNextRun:yyyy-MM-dd hh:mm:ss.fff} ");

            Assert.That(actualNextRun, Is.EqualTo(expectedNextRunDateTime));
        }
    }
}
