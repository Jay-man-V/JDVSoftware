//-----------------------------------------------------------------------
// <copyright file="RunTaskTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.ScheduledProcessBaseTests
{
    /// <summary>
    /// Summary description for SchedulerTests
    /// </summary>
    [TestFixture]
    public class RunTaskTests : UnitTestBase
    {
        private ICalendarProcess CalendarProcess { get; set; }
        private IEventLogProcess EventLogProcess { get; set; }
        private IScheduledJobProcess ScheduledJobProcess { get; set; }

        protected override void StartTest()
        {
            base.StartTest();

            CalendarProcess = Substitute.For<ICalendarProcess>();

            DateTimeService = Substitute.For<IDateTimeService>();
            DateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            DateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 23, 11, 54, 300));

            EventLogProcess = Substitute.For<IEventLogProcess>();
            EventLogProcess.StartTask(Arg.Any<AppId>(), Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>()).Returns(new LogId(123));

            IScheduledJobRepository dataAccess = Substitute.For<IScheduledJobRepository>();
            IScheduleIntervalProcess scheduleIntervalProcess = Substitute.For<IScheduleIntervalProcess>();
            ScheduledJobProcess = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService, dataAccess, StatusRepository, UserProfileRepository, EventLogProcess, scheduleIntervalProcess, CalendarProcess);

            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;
            ScheduledJobProcess.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            ScheduledJobProcess.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(Arg.Any<String>(), Arg.Any<DateTime>()).Returns(DateTimeService.SystemDateTimeNowWithoutMilliseconds);
        }

        [TestCase]
        public void Test_RunTask_Enabled_True()
        {
            String batchName = "Unit Testing";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();

            LogId logId = EventLogProcess.StartTask(ApplicationSettings.ApplicationId, batchName, processName, taskName);

            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, ScheduleInterval.Seconds, 2);

            IScheduledTask scheduledTask = ScheduledJobProcess.CreateScheduledTask(scheduledJob);
            MockScheduledTask mockScheduledTask = scheduledTask as MockScheduledTask;

            scheduledJob.IsEnabled = true;

            Guid eventHandlerId = Guid.NewGuid();
            Guid checkEventHandlerId = eventHandlerId;
            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, e) => { processJobCalled = true; checkEventHandlerId = Guid.Empty; };

            Assert.That(scheduledTask.ApplicationId, Is.EqualTo(new AppId(1)));
            Assert.That(processJobCalled, Is.EqualTo(false));
            Assert.That(checkEventHandlerId, Is.EqualTo(eventHandlerId));
        }

        [TestCase]
        public void Test_RunTask_Enabled_False()
        {
            String batchName = "Unit Testing";
            String processName = "RunTaskTests";
            String taskName = LocationUtils.GetFunctionName();

            LogId logId = EventLogProcess.StartTask(ApplicationSettings.ApplicationId, batchName, processName, taskName);

            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, ScheduleInterval.Seconds, 2);

            IScheduledTask scheduledTask = ScheduledJobProcess.CreateScheduledTask(scheduledJob);
            MockScheduledTask mockScheduledTask = scheduledTask as MockScheduledTask;

            scheduledJob.IsEnabled = false;

            Guid eventHandlerId = Guid.NewGuid();
            Guid checkEventHandlerId = eventHandlerId;
            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, e) => { processJobCalled = true; checkEventHandlerId = Guid.Empty; };

            Assert.That(scheduledTask.ApplicationId, Is.EqualTo(new AppId(1))); 
            Assert.That(processJobCalled, Is.EqualTo(false));
            Assert.That(checkEventHandlerId, Is.EqualTo(eventHandlerId));
        }
    }
}
