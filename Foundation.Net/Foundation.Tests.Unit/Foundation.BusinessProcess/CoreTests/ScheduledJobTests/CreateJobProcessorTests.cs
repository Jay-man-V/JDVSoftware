//-----------------------------------------------------------------------
// <copyright file="CreateJobProcessorTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.ScheduledJobTests
{
    /// <summary>
    /// Summary description for CreateTaskProcessorTests
    /// </summary>
    [TestFixture]
    public class CreateJobProcessorTests : UnitTestBase
    {
        [TestCase]
        public void TestCreateJobProcessor()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
            dateTimeService.SystemDateTimeNowWithoutMilliseconds.Returns(new DateTime(2022, 11, 27, 23, 11, 54));
            dateTimeService.SystemDateTimeNow.Returns(new DateTime(2022, 11, 27, 23, 11, 54, 300));

            DateTime currentDate = dateTimeService.SystemDateTimeNow.Date;

            IScheduledJobRepository dataAccess = Substitute.For<IScheduledJobRepository>();
            IEventLogProcess eventLogProcess = Substitute.For<IEventLogProcess>();
            IScheduleIntervalProcess scheduleIntervalProcess = Substitute.For<IScheduleIntervalProcess>();
            ICalendarProcess calendarProcess = Substitute.For<ICalendarProcess>();

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, ScheduleInterval.Seconds, 30);

            IScheduledJobProcess process = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, dataAccess, StatusDataAccess, UserProfileDataAccess, eventLogProcess, scheduleIntervalProcess, calendarProcess);
            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = eventLogProcess;
            process.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            process.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            IScheduledTask scheduledTask = process.CreateScheduledTask(scheduledJob);

            Assert.That(SchedulerSupport.TaskImplementationType, Is.EqualTo(scheduledTask.GetType().ToString()));
        }
    }
}
