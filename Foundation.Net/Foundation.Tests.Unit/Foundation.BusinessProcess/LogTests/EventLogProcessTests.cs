//-----------------------------------------------------------------------
// <copyright file="EventLogProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.LogTests
{
    /// <summary>
    /// Summary description for EventLogProcessTests
    /// </summary>
    [TestFixture]
    public class EventLogProcessTests : CommonBusinessProcessTestBaseClass<IEventLog, IEventLogProcess, IEventLogRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 14;
        protected override String ExpectedScreenTitle => "Event Logs";
        protected override String ExpectedStatusBarText => "Number of Event Logs:";

        protected override String ExpectedComboBoxDisplayMember => FDC.EventLog.BatchName;

        protected override IEventLogRepository CreateRepository()
        {
            IEventLogRepository dataAccess = Substitute.For<IEventLogRepository>();

            return dataAccess;
        }

        protected override IEventLogProcess CreateBusinessProcess()
        {
            IEventLogProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IEventLogProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ILogSeverityProcess logSeverityProcess = Substitute.For<ILogSeverityProcess>();
            ITaskStatusProcess taskStatusProcess = Substitute.For<ITaskStatusProcess>();

            IEventLogProcess process = new EventLogProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, logSeverityProcess, taskStatusProcess);

            return process;
        }

        protected override IEventLog CreateBlankEntity(IEventLogProcess process)
        {
            IEventLog retVal = CoreInstance.Container.Get<IEventLog>();

            return retVal;
        }

        protected override IEventLog CreateEntity(IEventLogProcess process)
        {
            IEventLog retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.ParentId = new LogId(1);
            retVal.LogSeverityId = new EntityId(1);
            retVal.ScheduledTaskId = new EntityId(1);
            retVal.BatchName = Guid.NewGuid().ToString();
            retVal.ProcessName = Guid.NewGuid().ToString();
            retVal.TaskName = Guid.NewGuid().ToString();
            retVal.TaskStatusId = new EntityId(1);
            retVal.StartedOn = DateTime.Now;
            retVal.FinishedOn = DateTime.Now;
            retVal.Information = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IEventLog entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.ParentId, Is.EqualTo(new LogId(0)));
            Assert.That(entity.LogSeverityId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.ScheduledTaskId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.BatchName, Is.EqualTo(null));
            Assert.That(entity.ProcessName, Is.EqualTo(null));
            Assert.That(entity.TaskName, Is.EqualTo(null));
            Assert.That(entity.TaskStatusId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.StartedOn, Is.EqualTo(DateTime.MinValue));
            Assert.That(entity.FinishedOn, Is.EqualTo(DateTime.MinValue));
            Assert.That(entity.Information, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IEventLog entity)
        {
            Assert.That(entity.BatchName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IEventLog entity)
        {
            Assert.That(entity.BatchName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IEventLog entity1, IEventLog entity2)
        {
            Assert.That(entity2.BatchName, Is.EqualTo(entity1.BatchName));
            Assert.That(entity2.ProcessName, Is.EqualTo(entity1.ProcessName));
            Assert.That(entity2.TaskName, Is.EqualTo(entity1.TaskName));
            Assert.That(entity2.TaskStatusId, Is.EqualTo(entity1.TaskStatusId));
            Assert.That(entity2.StartedOn, Is.EqualTo(entity1.StartedOn));
            Assert.That(entity2.FinishedOn, Is.EqualTo(entity1.FinishedOn));
            Assert.That(entity2.Information, Is.EqualTo(entity1.Information));

            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));
        }

        protected override void UpdateEntityProperties(IEventLog entity)
        {
            entity.BatchName += "Updated";
            entity.ProcessName += "Updated";
            entity.TaskName += "Updated";
            entity.TaskStatusId = new EntityId(1);
            entity.FinishedOn = entity.StartedOn.AddDays(10);
            entity.Information += "Updated";
        }

        [TestCase]
        public override void Test_Delete_Entity_Id()
        {
            IEventLogProcess process = CreateBusinessProcess();

            Repository
                .When(da => da.Delete(Arg.Any<EntityId>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                process.Delete(new EntityId(1));
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }

        [TestCase]
        public override void Test_Delete_Entity_Object()
        {
            IEventLogProcess process = CreateBusinessProcess();

            Repository
                .When(da => da.Delete(Arg.Any<IEventLog>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                process.Delete(entity);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }

        [TestCase]
        public override void Test_Delete_MultipleEntities()
        {
            IEventLogProcess process = CreateBusinessProcess();

            List<IEventLog> eventLogs = new List<IEventLog>
            {
                CoreInstance.Container.Get<IEventLog>(),
                CoreInstance.Container.Get<IEventLog>(),
            };

            Repository
                .When(da => da.Delete(Arg.Any<List<IEventLog>>()))
                .Do(x => throw new NotImplementedException("Event Log Entries cannot be deleted"));

            Exception actualException = null;
            try
            {
                process.Delete(eventLogs);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());
        }

        [TestCase(TaskStatus.NotSet, true, 0, null, null, null)]
        [TestCase(TaskStatus.NotSet, true, 1, null, null, null)]
        [TestCase(TaskStatus.NotSet, true, 0, "UnitTesting", null, null)]
        [TestCase(TaskStatus.NotSet, true, 0, null, "UnitTesting", null)]
        [TestCase(TaskStatus.NotSet, true, 0, null, null, "UnitTesting")]
        public void Test_GetLatest(TaskStatus expectedTaskStatus, Boolean isFinished, Int32 scheduledTaskId, String batchName, String processName, String taskName)
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            TaskStatus actual = TaskStatus.NotSet;

            IEventLog eventLog = eventLogProcess.GetLatest(isFinished, new EntityId(scheduledTaskId), batchName, processName, taskName);

            if (eventLog.IsNotNull())
            {
                actual = eventLog.TaskStatus;
            }

            Assert.That(expectedTaskStatus, Is.EqualTo(actual));
        }

        [TestCase]
        public void Test_StartTask()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(LogSeverity.Information.Id());
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);

            Assert.That(logId.TheLogId > 0);

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ApplicationId.TheAppId, Is.EqualTo(applicationId.TheAppId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(LogSeverity.Information.Id()));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_EndTask_1()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            LogSeverity logSeverity = LogSeverity.Warning;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);

            try
            {
                String message = $"{batchName} - {processName} - {taskName}";
                throw new Exception(message);
            }
            catch (Exception exception)
            {
                eventLogProcess.EndTask(logId, logSeverity, exception);
            }

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ApplicationId.TheAppId, Is.EqualTo(applicationId.TheAppId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_EndTask_2()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            String message = $"{batchName} - {processName} - {taskName}";
            LogSeverity logSeverity = LogSeverity.Warning;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.Information = message;
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);

            eventLogProcess.EndTask(logId, logSeverity, message);

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ApplicationId.TheAppId, Is.EqualTo(applicationId.TheAppId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.Information, Is.EqualTo(message));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_Update_EndTask()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            String message = $"{batchName} - {processName} - {taskName}";
            LogSeverity logSeverity = LogSeverity.Warning;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.Information = message + Environment.NewLine + message;
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);

            eventLogProcess.UpdateLogEntry(logId, message);

            eventLogProcess.EndTask(logId, logSeverity, message);

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ApplicationId.TheAppId, Is.EqualTo(applicationId.TheAppId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.Information, Is.EqualTo(message + Environment.NewLine + message));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_CreateLogEntry_Handling_ExceptionObjects()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            LogSeverity logSeverity = LogSeverity.Warning;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<EntityId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ParentId = new LogId(1);
                entity.BatchName = String.Empty;
                entity.ProcessName = String.Empty;
                entity.TaskName = String.Empty;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);
            LogId updateLogId;

            try
            {
                String message = $"{batchName} - {processName} - {taskName}";
                throw new Exception(message);
            }
            catch (Exception exception)
            {
                updateLogId = eventLogProcess.CreateLogEntry(logId, applicationId, logSeverity, exception);
            }

            IEventLog eventLog = eventLogProcess.Get(new EntityId(updateLogId.ToInteger()));

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(updateLogId.TheLogId));
            Assert.That(eventLog.ParentId.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.BatchName, Is.EqualTo(String.Empty));
            Assert.That(eventLog.ProcessName, Is.EqualTo(String.Empty));
            Assert.That(eventLog.TaskName, Is.EqualTo(String.Empty));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_CreateLogEntry_Parent_Child()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            String message = $"{batchName} - {processName} - {taskName}";
            LogSeverity logSeverity = LogSeverity.Trace;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ParentId = new LogId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.Information = message;
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId parentLogId = eventLogProcess.StartTask(ApplicationSettings.ApplicationId, batchName, processName, taskName);

            LogId logId = eventLogProcess.CreateLogEntry(parentLogId, applicationId, batchName, processName, taskName, logSeverity, String.Empty);

            eventLogProcess.EndTask(logId, logSeverity, message);

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ParentId.TheLogId, Is.EqualTo(parentLogId.TheLogId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.Information, Is.EqualTo(message));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }

        [TestCase]
        public void Test_Update_1()
        {
            IEventLogProcess eventLogProcess = CreateBusinessProcess();

            AppId applicationId = ApplicationSettings.ApplicationId;
            String batchName = "UnitTesting";
            String processName = LocationUtils.GetClassName();
            String taskName = LocationUtils.GetFunctionName();
            String message = $"{batchName} - {processName} - {taskName}";
            LogSeverity logSeverity = LogSeverity.Information;

            Repository.Save(Arg.Any<IEventLog>()).Returns(args =>
            {
                IEventLog entity = (IEventLog)args[0];
                entity.Id = new LogId(1);

                return entity;
            });

            Repository.Get(Arg.Any<LogId>()).Returns(args =>
            {
                IEventLog entity = CoreInstance.Container.Get<IEventLog>();
                entity.Id = new LogId(1);
                entity.ApplicationId = new AppId(1);
                entity.BatchName = batchName;
                entity.ProcessName = processName;
                entity.TaskName = taskName;
                entity.LogSeverityId = new EntityId(logSeverity.Id());
                entity.Information = message + Environment.NewLine + message;
                entity.StartedOn = DateTimeService.SystemDateTimeNow;

                return entity;
            });

            LogId logId = eventLogProcess.StartTask(applicationId, batchName, processName, taskName);

            eventLogProcess.UpdateLogEntry(logId, message);
            eventLogProcess.UpdateLogEntry(logId, message);

            IEventLog eventLog = eventLogProcess.Get(logId);

            Assert.That(eventLog.Id.TheLogId, Is.EqualTo(logId.TheLogId));
            Assert.That(eventLog.ApplicationId.TheAppId, Is.EqualTo(applicationId.TheAppId));
            Assert.That(eventLog.BatchName, Is.EqualTo(batchName));
            Assert.That(eventLog.ProcessName, Is.EqualTo(processName));
            Assert.That(eventLog.TaskName, Is.EqualTo(taskName));
            Assert.That(eventLog.LogSeverityId.ToInteger(), Is.EqualTo(logSeverity.Id()));
            Assert.That(eventLog.Information, Is.EqualTo(message + Environment.NewLine + message));
            Assert.That(eventLog.StartedOn, Is.EqualTo(DateTimeService.SystemDateTimeNow));
        }
    }
}
