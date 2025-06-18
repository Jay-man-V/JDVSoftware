//-----------------------------------------------------------------------
// <copyright file="OfficeWeekCalendarProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for OfficeWeekCalendarProcessTests
    /// </summary>
    [TestFixture]
    public class OfficeWeekCalendarProcessTests : CommonBusinessProcessTestBaseClass<IOfficeWeekCalendar, IOfficeWeekCalendarProcess, IOfficeWeekCalendarRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 16;
        protected override String ExpectedScreenTitle => "Office Week Calendars";
        protected override String ExpectedStatusBarText => "Number of Office Week Calendars:";

        protected override String ExpectedComboBoxDisplayMember => FDC.OfficeWeekCalendar.ShortName;

        protected override IOfficeWeekCalendarRepository CreateRepository()
        {
            IOfficeWeekCalendarRepository dataAccess = Substitute.For<IOfficeWeekCalendarRepository>();

            return dataAccess;
        }

        protected override IOfficeWeekCalendarProcess CreateBusinessProcess()
        {
            IOfficeWeekCalendarProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IOfficeWeekCalendarProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IOfficeWeekCalendarProcess process = new OfficeWeekCalendarProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IOfficeWeekCalendar CreateBlankEntity(IOfficeWeekCalendarProcess process)
        {
            IOfficeWeekCalendar retVal = CoreInstance.Container.Get<IOfficeWeekCalendar>();

            return retVal;
        }

        protected override IOfficeWeekCalendar CreateEntity(IOfficeWeekCalendarProcess process)
        {
            IOfficeWeekCalendar retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Code = Guid.NewGuid().ToString();
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.Mon = true;
            retVal.Tue = true;
            retVal.Wed = true;
            retVal.Thu = true;
            retVal.Fri = true;
            retVal.Sat = true;
            retVal.Sun = true;

            return retVal;
        }

        protected override void CheckBlankEntry(IOfficeWeekCalendar entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IOfficeWeekCalendar entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IOfficeWeekCalendar entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IOfficeWeekCalendar entity1, IOfficeWeekCalendar entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Code, Is.EqualTo(entity1.Code));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.Mon, Is.EqualTo(entity1.Mon));
            Assert.That(entity2.Tue, Is.EqualTo(entity1.Tue));
            Assert.That(entity2.Wed, Is.EqualTo(entity1.Wed));
            Assert.That(entity2.Thu, Is.EqualTo(entity1.Thu));
            Assert.That(entity2.Fri, Is.EqualTo(entity1.Fri));
            Assert.That(entity2.Sat, Is.EqualTo(entity1.Sat));
            Assert.That(entity2.Sun, Is.EqualTo(entity1.Sun));
        }

        protected override void UpdateEntityProperties(IOfficeWeekCalendar entity)
        {
            entity.Code += "Updated";
            entity.ShortName += "Updated";
            entity.Mon = true;
            entity.Tue = false;
            entity.Wed = true;
            entity.Thu = false;
            entity.Fri = true;
            entity.Sat = false;
            entity.Sun = true;
        }
    }
}
