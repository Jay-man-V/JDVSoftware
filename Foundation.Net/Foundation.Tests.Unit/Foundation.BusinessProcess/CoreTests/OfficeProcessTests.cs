//-----------------------------------------------------------------------
// <copyright file="OfficeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for OfficeProcessTests
    /// </summary>
    [TestFixture]
    public class OfficeProcessTests : CommonBusinessProcessTestBaseClass<IOffice, IOfficeProcess, IOfficeRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Offices";
        protected override String ExpectedStatusBarText => "Number of Offices:";

        protected override String ExpectedComboBoxDisplayMember => FDC.Office.ShortName;

        protected override IOfficeRepository CreateDataAccess()
        {
            IOfficeRepository dataAccess = Substitute.For<IOfficeRepository>();

            return dataAccess;
        }

        protected override IOfficeProcess CreateBusinessProcess()
        {
            IOfficeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IOfficeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IOfficeProcess process = new OfficeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IOffice CreateBlankEntity(IOfficeProcess process)
        {
            IOffice retVal = CoreInstance.Container.Get<IOffice>();

            return retVal;
        }

        protected override IOffice CreateEntity(IOfficeProcess process)
        {
            IOffice retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Code = Guid.NewGuid().ToString();
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.ContactDetailId = new EntityId(1);
            retVal.OfficeWeekCalendarId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IOffice entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IOffice entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IOffice entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IOffice entity1, IOffice entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Code, Is.EqualTo(entity1.Code));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.ContactDetailId, Is.EqualTo(entity1.ContactDetailId));
            Assert.That(entity2.OfficeWeekCalendarId, Is.EqualTo(entity1.OfficeWeekCalendarId));
        }

        protected override void UpdateEntityProperties(IOffice entity)
        {
            entity.Code = "Updated";
            entity.ShortName += "Updated";
        }
    }
}
