//-----------------------------------------------------------------------
// <copyright file="ScheduledDataStatusProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ScheduledDataStatusProcessTests
    /// </summary>
    [TestFixture]
    public class ScheduledDataStatusProcessTests : CommonBusinessProcessTestBaseClass<IScheduledDataStatus, IScheduledDataStatusProcess, IScheduledDataStatusRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 10;
        protected override String ExpectedScreenTitle => "Scheduled Data Statuses";
        protected override String ExpectedStatusBarText => "Number of Scheduled Data Statuses:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ScheduledDataStatus.Name;

        protected override IScheduledDataStatusRepository CreateRepository()
        {
            IScheduledDataStatusRepository dataAccess = Substitute.For<IScheduledDataStatusRepository>();

            return dataAccess;
        }

        protected override IScheduledDataStatusProcess CreateBusinessProcess()
        {
            IScheduledDataStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IScheduledDataStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IDataStatusProcess dataStatusProcess = Substitute.For<IDataStatusProcess>();

            IScheduledDataStatusProcess process = new ScheduledDataStatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, dataStatusProcess);

            return process;
        }

        protected override IScheduledDataStatus CreateBlankEntity(IScheduledDataStatusProcess process)
        {
            IScheduledDataStatus retVal = CoreInstance.Container.Get<IScheduledDataStatus>();

            return retVal;
        }

        protected override IScheduledDataStatus CreateEntity(IScheduledDataStatusProcess process)
        {
            IScheduledDataStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.DataDate = DateTimeService.SystemDateTimeNow;
            retVal.Name = Guid.NewGuid().ToString();
            retVal.DataStatusId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IScheduledDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IScheduledDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IScheduledDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IScheduledDataStatus entity1, IScheduledDataStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
        }

        protected override void UpdateEntityProperties(IScheduledDataStatus entity)
        {
            entity.Name = "Updated";
        }
    }
}
