//-----------------------------------------------------------------------
// <copyright file="TimeZoneProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for TimeZoneProcessTests
    /// </summary>
    [TestFixture]
    public class TimeZoneProcessTests : CommonBusinessProcessTestBaseClass<ITimeZone, ITimeZoneProcess, ITimeZoneRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Time Zones";
        protected override String ExpectedStatusBarText => "Number of Time Zones:";

        protected override String ExpectedComboBoxDisplayMember => FDC.TimeZone.Code;

        protected override ITimeZoneRepository CreateRepository()
        {
            ITimeZoneRepository dataAccess = Substitute.For<ITimeZoneRepository>();

            return dataAccess;
        }

        protected override ITimeZoneProcess CreateBusinessProcess()
        {
            ITimeZoneProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ITimeZoneProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ITimeZoneProcess process = new TimeZoneProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override ITimeZone CreateBlankEntity(ITimeZoneProcess process)
        {
            ITimeZone retVal = CoreInstance.Container.Get<ITimeZone>();

            return retVal;
        }

        protected override ITimeZone CreateEntity(ITimeZoneProcess process)
        {
            ITimeZone retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Code = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();
            retVal.Offset = 12;
            retVal.HasDaylightSavings = true;

            return retVal;
        }

        protected override void CheckBlankEntry(ITimeZone entity)
        {
            Assert.That(entity.Code, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ITimeZone entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ITimeZone entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ITimeZone entity1, ITimeZone entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Code, Is.EqualTo(entity1.Code));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
            Assert.That(entity2.Offset, Is.EqualTo(entity1.Offset));
            Assert.That(entity2.HasDaylightSavings, Is.EqualTo(entity1.HasDaylightSavings));
        }

        protected override void UpdateEntityProperties(ITimeZone entity)
        {
            entity.Code = "Updated";
            entity.Description += "Updated";
            entity.Offset += 10;
            entity.HasDaylightSavings = false;
        }
    }
}
