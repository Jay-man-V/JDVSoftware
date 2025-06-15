//-----------------------------------------------------------------------
// <copyright file="EntityStatusProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for EntityStatusProcessTests
    /// </summary>
    [TestFixture]
    public class EntityStatusProcessTests : CommonBusinessProcessTestBaseClass<IEntityStatus, IEntityStatusProcess, IEntityStatusRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Entity Statuses";
        protected override String ExpectedStatusBarText => "Number of Entity Statuses:";

        protected override String ExpectedComboBoxDisplayMember => FDC.EntityStatus.Name;

        protected override IEntityStatusRepository CreateDataAccess()
        {
            IEntityStatusRepository dataAccess = Substitute.For<IEntityStatusRepository>();

            return dataAccess;
        }

        protected override IEntityStatusProcess CreateBusinessProcess()
        {
            IEntityStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IEntityStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IEntityStatusProcess process = new EntityStatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IEntityStatus CreateBlankEntity(IEntityStatusProcess process)
        {
            IEntityStatus retVal = CoreInstance.Container.Get<IEntityStatus>();

            return retVal;
        }

        protected override IEntityStatus CreateEntity(IEntityStatusProcess process)
        {
            IEntityStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IEntityStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IEntityStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IEntityStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IEntityStatus entity1, IEntityStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IEntityStatus entity)
        {
            entity.Name = "Updated";
            entity.Description += "Updated";
        }
    }
}
