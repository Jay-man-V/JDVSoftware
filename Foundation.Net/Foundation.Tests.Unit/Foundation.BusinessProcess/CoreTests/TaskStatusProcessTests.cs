//-----------------------------------------------------------------------
// <copyright file="TaskStatusProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for TaskStatusProcessTests
    /// </summary>
    [TestFixture]
    public class TaskStatusProcessTests : CommonBusinessProcessTestBaseClass<ITaskStatus, ITaskStatusProcess, ITaskStatusRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Task Statuses";
        protected override String ExpectedStatusBarText => "Number of Task Statuses:";

        protected override String ExpectedComboBoxDisplayMember => FDC.TaskStatus.Name;

        protected override ITaskStatusRepository CreateDataAccess()
        {
            ITaskStatusRepository dataAccess = Substitute.For<ITaskStatusRepository>();

            return dataAccess;
        }

        protected override ITaskStatusProcess CreateBusinessProcess()
        {
            ITaskStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ITaskStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ITaskStatusProcess process = new TaskStatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override ITaskStatus CreateBlankEntity(ITaskStatusProcess process)
        {
            ITaskStatus retVal = CoreInstance.Container.Get<ITaskStatus>();

            return retVal;
        }

        protected override ITaskStatus CreateEntity(ITaskStatusProcess process)
        {
            ITaskStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(ITaskStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ITaskStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ITaskStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ITaskStatus entity1, ITaskStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(ITaskStatus entity)
        {
            entity.Name = "Updated";
            entity.Description += "Updated";
        }
    }
}
