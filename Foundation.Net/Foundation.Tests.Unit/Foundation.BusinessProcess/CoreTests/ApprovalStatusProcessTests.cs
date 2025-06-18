//-----------------------------------------------------------------------
// <copyright file="ApprovalStatusProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ApprovalStatusProcessTests
    /// </summary>
    [TestFixture]
    public class ApprovalStatusProcessTests : CommonBusinessProcessTestBaseClass<IApprovalStatus, IApprovalStatusProcess, IApprovalStatusRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Approval Statuses";
        protected override String ExpectedStatusBarText => "Number of Approval Statuses:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ApprovalStatus.Name;

        protected override IApprovalStatusRepository CreateRepository()
        {
            IApprovalStatusRepository dataAccess = Substitute.For<IApprovalStatusRepository>();

            return dataAccess;
        }

        protected override IApprovalStatusProcess CreateBusinessProcess()
        {
            IApprovalStatusProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApprovalStatusProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApprovalStatusProcess process = new ApprovalStatusProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IApprovalStatus CreateBlankEntity(IApprovalStatusProcess process)
        {
            IApprovalStatus retVal = CoreInstance.Container.Get<IApprovalStatus>();

            return retVal;
        }

        protected override IApprovalStatus CreateEntity(IApprovalStatusProcess process)
        {
            IApprovalStatus retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IApprovalStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IApprovalStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IApprovalStatus entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IApprovalStatus entity1, IApprovalStatus entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IApprovalStatus entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
