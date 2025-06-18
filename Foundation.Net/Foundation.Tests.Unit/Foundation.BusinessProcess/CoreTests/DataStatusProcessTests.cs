//-----------------------------------------------------------------------
// <copyright file="DataStatusProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for DataStatusProcessTests
    /// </summary>
    [TestFixture]
    public class DataStatusProcessTests : CommonBusinessProcessTestBaseClass<IDataStatus, IDataStatusProcess, IDataStatusRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Data Statuses";
        protected override String ExpectedStatusBarText => "Number of Data Statuses:";

        protected override string ExpectedComboBoxDisplayMember => FDC.DataStatus.Name;

        protected override IDataStatusRepository CreateRepository()
        {
            IDataStatusRepository dataAccess = Substitute.For<IDataStatusRepository>();

            return dataAccess;
        }

        protected override IDataStatusProcess CreateBusinessProcess()
        {
            IDataStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IDataStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IDataStatusProcess process = new DataStatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IDataStatus CreateBlankEntity(IDataStatusProcess process)
        {
            IDataStatus retVal = CoreInstance.Container.Get<IDataStatus>();

            return retVal;
        }

        protected override IDataStatus CreateEntity(IDataStatusProcess process)
        {
            IDataStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IDataStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IDataStatus entity1, IDataStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IDataStatus entity)
        {
            entity.Name = "Updated";
            entity.Description += "Updated";
        }
    }
}
