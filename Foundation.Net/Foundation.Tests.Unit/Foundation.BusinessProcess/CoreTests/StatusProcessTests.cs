//-----------------------------------------------------------------------
// <copyright file="StatusProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for StatusProcessTests
    /// </summary>
    [TestFixture]
    public class StatusProcessTests : CommonBusinessProcessTestBaseClass<IStatus, IStatusProcess, IStatusRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Statuses";
        protected override String ExpectedStatusBarText => "Number of Statuses:";

        protected override String ExpectedComboBoxDisplayMember => FDC.Status.Name;

        protected override IStatusRepository CreateDataAccess()
        {
            IStatusRepository dataAccess = Substitute.For<IStatusRepository>();

            return dataAccess;
        }

        protected override IStatusProcess CreateBusinessProcess()
        {
            IStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IStatusProcess process = new StatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IStatus CreateBlankEntity(IStatusProcess process)
        {
            IStatus retVal = CoreInstance.Container.Get<IStatus>();

            return retVal;
        }

        protected override IStatus CreateEntity(IStatusProcess process)
        {
            IStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IStatus entity1, IStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IStatus entity)
        {
            entity.Name = "Updated";
            entity.Description += "Updated";
        }
    }
}
