//-----------------------------------------------------------------------
// <copyright file="ServerProcessTimerTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.ComponentsTests
{
    /// <summary>
    /// Summary description for ServerProcessTimerTests
    /// </summary>
    [TestFixture]
    public class ServerProcessTimerTests : UnitTestBase
    {
        private ICalendarProcess CalendarProcess { get; set; }

        protected override void StartTest()
        {
            base.StartTest();

            CalendarProcess = Substitute.For<ICalendarProcess>();
        }

        [TestCase]
        public void Test_CreateObject()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
            dateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            dateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 23, 11, 54, 300));

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = dateTimeService.SystemDateTimeNow.Date;

            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(RunTimeEnvironmentSettings.StandardCountryCode, currentDate + startTime).Returns(currentDate + startTime);
            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(RunTimeEnvironmentSettings.StandardCountryCode, currentDate + endTime).Returns(currentDate + endTime);

            IScheduledJobRepository dataAccess = Substitute.For<IScheduledJobRepository>();
            IStatusRepository statusDataAccess = Substitute.For<IStatusRepository>();
            IUserProfileRepository userProfileDataAccess = Substitute.For<IUserProfileRepository>();
            IEventLogProcess eventLogProcess = Substitute.For<IEventLogProcess>();
            IScheduleIntervalProcess scheduleIntervalProcess = Substitute.For<IScheduleIntervalProcess>();
            ICalendarProcess calendarProcess = Substitute.For<ICalendarProcess>();

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, ScheduleInterval.Seconds, 30);
            IScheduledJobProcess process = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, dataAccess, statusDataAccess, userProfileDataAccess, eventLogProcess, scheduleIntervalProcess, calendarProcess);
            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = dateTimeService;
            SchedulerSupport.EventLogProcess = eventLogProcess;

            process.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            process.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            scheduledJob.ScheduledTask = process.CreateScheduledTask(scheduledJob);

            ServerProcessTimer serverProcessTimer = new ServerProcessTimer(scheduledJob);

            Assert.That(serverProcessTimer.Name, Is.EqualTo(scheduledJob.Name));
            Assert.That(serverProcessTimer.ScheduledJob, Is.EqualTo(scheduledJob));
        }
    }
}
