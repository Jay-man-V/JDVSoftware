//-----------------------------------------------------------------------
// <copyright file="ScheduledJobProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;

using NSubstitute;

using NUnit.Framework;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Models;
using Foundation.Server.ScheduledTasks;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using FDC = Foundation.Common.DataColumns;
using FEnums = Foundation.Interfaces;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ScheduledJobProcessTests
    /// </summary>
    [TestFixture]
    public class ScheduledJobProcessTests : CommonBusinessProcessTestBaseClass<IScheduledJob, IScheduledJobProcess, IScheduledJobRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 18;
        protected override String ExpectedScreenTitle => "Scheduled Jobs";
        protected override String ExpectedStatusBarText => "Number of Scheduled Jobs:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ScheduledJob.Name;

        protected override IScheduledJobRepository CreateDataAccess()
        {
            IScheduledJobRepository dataAccess = Substitute.For<IScheduledJobRepository>();

            return dataAccess;
        }

        protected override IScheduledJobProcess CreateBusinessProcess()
        {
            IScheduledJobProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IScheduledJobProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            SchedulerSupport.Core = CoreInstance;
            SchedulerSupport.RunTimeEnvironmentSettings = RunTimeEnvironmentSettings;
            SchedulerSupport.DateTimeService = DateTimeService;
            SchedulerSupport.EventLogProcess = EventLogProcess;

            IScheduleIntervalProcess scheduleIntervalProcess = Substitute.For<IScheduleIntervalProcess>();
            ICalendarProcess calendarProcess = Substitute.For<ICalendarProcess>();

            IScheduledJobProcess process = new ScheduledJobProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, EventLogProcess, scheduleIntervalProcess, calendarProcess);

            process.AlternateCreateScheduledTaskCalled -= SchedulerSupport.OnAlternateCreateScheduledTaskCalled;
            process.AlternateCreateScheduledTaskCalled += SchedulerSupport.OnAlternateCreateScheduledTaskCalled;

            return process;
        }

        protected override IScheduledJob CreateBlankEntity(IScheduledJobProcess process)
        {
            IScheduledJob retVal = CoreInstance.Container.Get<IScheduledJob>();

            return retVal;
        }

        protected override IScheduledJob CreateEntity(IScheduledJobProcess process)
        {
            IScheduledJob retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.RunImmediately = false;
            retVal.StartTime = new TimeSpan(09, 00, 00);
            retVal.EndTime = new TimeSpan(17, 00, 00);
            retVal.Interval = 15;
            retVal.ScheduleIntervalId = new EntityId(2);
            retVal.IsEnabled = true;
            retVal.LastRunDateTime = DateTimeService.SystemDateTimeNow;
            retVal.NextRunDateTime = DateTimeService.SystemDateTimeNow;
            retVal.TaskImplementationType = Guid.NewGuid().ToString();
            retVal.TaskParameters = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IScheduledJob entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IScheduledJob entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IScheduledJob entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IScheduledJob entity1, IScheduledJob entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
        }

        protected override void UpdateEntityProperties(IScheduledJob entity)
        {
            entity.Name = "Updated";
        }

        [TestCase]
        public void Test_InitialiseJobs()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            Dictionary<String, ServerProcessTimer> scheduledTimers = scheduledJobProcess.ScheduledTimers;

            Assert.That(scheduledTimers.Count, Is.Zero);

            process.InitialiseJobs(new LogId(0));

            Assert.That(scheduledTimers.Count, Is.Not.Zero);
        }

        [TestCase]
        public void Test_StartJobs_NoInitialise()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.StartJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            //Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;
            };

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StartJobs_CheckTaskRuns()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;
            };

            process.StartJobs(new LogId(0));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StartJobs_CheckTaskRuns_Error()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTaskWithError mockScheduledTask = (MockScheduledTaskWithError)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;
            };

            process.StartJobs(new LogId(0));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StartJobs_OnElapsed_Error()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTaskWithError mockScheduledTask = (MockScheduledTaskWithError)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            mockScheduledTask.OnCanExecute += (sender, args) =>
            {
                String errorMessage = "Exception raised during checking CanExecute";
                throw new Exception(errorMessage);
            };

            process.StartJobs(new LogId(0));

            DateTime testStartTime = DateTime.Now;

            while (testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            // TODO: Assert this worked
        }

        [TestCase]
        public void Test_StartJobs_Enabled()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;
            };

            process.StartJobs(new LogId(0));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));
            Assert.That(processJobCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StartJobs_DemoJob()
        {
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledDemoJob(CoreInstance, false, currentDate, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            process.StartJobs(new LogId(0));

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StartJobs_Disabled()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            scheduledJob.IsEnabled = false;
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;
            };

            process.StartJobs(new LogId(0));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));
            Assert.That(processJobCalled, Is.EqualTo(false));
        }

        [TestCase]
        public void Test_StartStopStartJobs()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled1 = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled1 = true;
            };

            process.StartJobs(new LogId(0));
            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled1 &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled1, Is.EqualTo(true));

            process.StopJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled2 = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled2 = true;
            };

            process.StartJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));

            testStartTime = DateTime.Now;

            while (!processJobCalled2 &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled2, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_StopJobs()
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled = true;

                Int32 loopCount = 0;
                while (loopCount < 1000)
                {
                    Thread.Sleep(150);
                    //Debug.WriteLine($"{DateTime.Now:dd-MMM-yyyy HH:mm:ss}");
                    loopCount++;
                }
            };

            Thread.Sleep(250);

            process.StartJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            process.StopJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Assert.That(processJobCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_ResumeTasks()
        {
            IScheduledJobProcess process = CreateBusinessProcess();

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            ScheduledJobProcess scheduledJobProcess = process as ScheduledJobProcess;
            process.InitialiseJobs(new LogId(0));

            ServerProcessTimer serverProcessTimer = scheduledJobProcess.ScheduledTimers[scheduledJob.Name];
            MockScheduledTask mockScheduledTask = (MockScheduledTask)serverProcessTimer.ScheduledJob.ScheduledTask;

            Boolean processJobCalled1 = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled1 = true;
            };

            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            process.StartJobs(new LogId(0));
            Assert.That(scheduledJobProcess.ScheduledTimers.Count, Is.EqualTo(1));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));

            DateTime testStartTime = DateTime.Now;

            while (!processJobCalled1 &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            process.StopJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(false));

            Boolean processJobCalled2 = false;
            mockScheduledTask.ProcessJobCalled += (sender, args) =>
            {
                processJobCalled2 = true;
            };

            process.ResumeJobs(new LogId(0));
            Assert.That(serverProcessTimer.Enabled, Is.EqualTo(true));

            testStartTime = DateTime.Now;

            while (!processJobCalled2 &&
                   testStartTime.AddMinutes(1) > DateTime.Now)
            {
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }

            Assert.That(processJobCalled1, Is.EqualTo(true));
            Assert.That(processJobCalled2, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_GetServiceStatus_IScheduledJob_NotInitialised()
        {
            String errorMessage = $"{nameof(ScheduledJobProcess)} has not been initialised";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();

                process.GetServiceStatus(scheduledJob);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<InvalidOperationException>());

            InvalidOperationException invalidOperationException = (InvalidOperationException)actualException;
            String actualErrorMessage = invalidOperationException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_IScheduledJob_UnknownTask()
        {
            String errorMessage = "Scheduled Job with name 'CreateScheduledJobWithError' does not exist or was not loaded\r\nParameter name: scheduledJob";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob1 = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                IScheduledJob scheduledJob2 = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob1 };

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetServiceStatus(scheduledJob2);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<ArgumentException>());

            ArgumentException argumentException = (ArgumentException)actualException;
            String actualErrorMessage = argumentException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_IScheduledJob_NoTasks()
        {
            String errorMessage = $"{nameof(ScheduledJobProcess)} has not been initialised";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob>();

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetServiceStatus(scheduledJob);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<InvalidOperationException>());

            InvalidOperationException invalidOperationException = (InvalidOperationException)actualException;
            String actualErrorMessage = invalidOperationException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_IScheduledJob_Null()
        {
            String errorMessage = "Value cannot be null.\r\nParameter name: scheduledJob";
            Exception actualException = null;

            try
            {
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob>();

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetServiceStatus(null);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<ArgumentNullException>());

            ArgumentNullException argumentNullException = (ArgumentNullException)actualException;
            String actualErrorMessage = argumentNullException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_IScheduledJob_KnownTask()
        {
            ServiceStatus expected = ServiceStatus.Stopped;
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

            DataAccess.GetAllActive().Returns(scheduleTasks);

            IScheduledJobProcess process = CreateBusinessProcess();
            process.InitialiseJobs(new LogId(0));

            ServiceStatus actual = process.GetServiceStatus(scheduledJob);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetServiceStatus_WindowsService_Null()
        {
            String serverName = ".";
            const String serviceName = null;

            String errorMessage = "Invalid value  for parameter name.";

            Exception actualException = null;

            try
            {
                IScheduledJobProcess process = CreateBusinessProcess();
                process.GetServiceStatus(serverName, serviceName);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<ArgumentException>());

            ArgumentException argumentException = (ArgumentException)actualException;
            String actualErrorMessage = argumentException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_WindowsService_UnknownTask()
        {
            String serverName = ".";
            String serviceName = Guid.NewGuid().ToString();

            String errorMessage = $"Service {serviceName} was not found on computer '{serverName}'.";

            Exception actualException = null;

            try
            {
                IScheduledJobProcess process = CreateBusinessProcess();
                process.GetServiceStatus(serverName, serviceName);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<InvalidOperationException>());

            String actualErrorMessage = actualException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetServiceStatus_WindowsService_KnownTask_Started()
        {
            String serverName = ".";
            String serviceName = "EventLog";
            ServiceStatus expected = ServiceStatus.Running;

            IScheduledJobProcess process = CreateBusinessProcess();
            ServiceStatus actual = process.GetServiceStatus(serverName, serviceName);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetServiceStatus_WindowsService_KnownTask_Stopped()
        {
            String serverName = ".";
            String serviceName = "wuauserv"; // Windows Update
            ServiceStatus expected = ServiceStatus.Stopped;

            IScheduledJobProcess process = CreateBusinessProcess();
            ServiceStatus actual = process.GetServiceStatus(serverName, serviceName);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetJobLastRunStatus_IScheduledJob_NotInitialised()
        {
            String errorMessage = $"{nameof(ScheduledJobProcess)} has not been initialised";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob };

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();

                process.GetJobLastRunStatus(scheduledJob);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<InvalidOperationException>());

            InvalidOperationException invalidOperationException = (InvalidOperationException)actualException;
            String actualErrorMessage = invalidOperationException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetJobLastRunStatus_IScheduledJob_UnknownTask()
        {
            String errorMessage = "Scheduled Job with name 'CreateScheduledJobWithError' does not exist or was not loaded\r\nParameter name: scheduledJob";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob1 = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                IScheduledJob scheduledJob2 = SchedulerSupport.CreateScheduledJobWithError(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob> { scheduledJob1 };

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetJobLastRunStatus(scheduledJob2);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<ArgumentException>());

            ArgumentException argumentException = (ArgumentException)actualException;
            String actualErrorMessage = argumentException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetJobLastRunStatus_IScheduledJob_NoTasks()
        {
            String errorMessage = "No scheduled jobs available";
            Exception actualException = null;

            try
            {
                TimeSpan startTime = new TimeSpan(9, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
                FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
                Int32 interval = 1;

                IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob>();

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetJobLastRunStatus(scheduledJob);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<InvalidOperationException>());

            InvalidOperationException invalidOperationException = (InvalidOperationException)actualException;
            String actualErrorMessage = invalidOperationException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetJobLastRunStatus_IScheduledJob_Null()
        {
            String errorMessage = "Value cannot be null.\r\nParameter name: scheduledJob";
            Exception actualException = null;

            try
            {
                List<IScheduledJob> scheduleTasks = new List<IScheduledJob>();

                DataAccess.GetAllActive().Returns(scheduleTasks);

                IScheduledJobProcess process = CreateBusinessProcess();
                process.InitialiseJobs(new LogId(0));

                process.GetJobLastRunStatus(null);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.TypeOf<ArgumentNullException>());

            ArgumentNullException argumentNullException = (ArgumentNullException)actualException;
            String actualErrorMessage = argumentNullException.Message;
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetJobLastRunStatus_IScheduledJob_KnownTask()
        {
            FEnums.TaskStatus expected = FEnums.TaskStatus.Success;
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            DateTime currentDate = DateTimeService.SystemDateTimeNow.Date;
            FEnums.ScheduleInterval scheduleInterval = FEnums.ScheduleInterval.Seconds;
            Int32 interval = 1;

            IScheduledJob scheduledJob = SchedulerSupport.CreateScheduledJob(CoreInstance, false, currentDate, startTime, endTime, scheduleInterval, interval);
            List<IScheduledJob> scheduleTasks = new List<IScheduledJob>
            {
                scheduledJob
            };

            DataAccess.GetAllActive().Returns(scheduleTasks);
            EventLogProcess.GetLatest(isFinished: true, Arg.Any<EntityId>()).Returns
            (
                new EventLog
                {
                    TaskStatusId = new EntityId(FEnums.TaskStatus.Success.Id())
                }
            );

            IScheduledJobProcess process = CreateBusinessProcess();
            process.InitialiseJobs(new LogId(0));

            FEnums.TaskStatus actual = process.GetJobLastRunStatus(scheduledJob);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
