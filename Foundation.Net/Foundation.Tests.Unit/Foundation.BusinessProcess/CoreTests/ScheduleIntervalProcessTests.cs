//-----------------------------------------------------------------------
// <copyright file="ScheduleIntervalProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ScheduleIntervalProcessTests
    /// </summary>
    [TestFixture]
    public class ScheduleIntervalProcessTests : CommonBusinessProcessTestBaseClass<IScheduleInterval, IScheduleIntervalProcess, IScheduleIntervalRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Scheduled Intervals";
        protected override String ExpectedStatusBarText => "Number of Schedule Intervals:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ScheduleInterval.Name;

        protected override IScheduleIntervalRepository CreateDataAccess()
        {
            IScheduleIntervalRepository dataAccess = Substitute.For<IScheduleIntervalRepository>();

            return dataAccess;
        }

        protected override IScheduleIntervalProcess CreateBusinessProcess()
        {
            IScheduleIntervalProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IScheduleIntervalProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IScheduleIntervalProcess process = new ScheduleIntervalProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IScheduleInterval CreateBlankEntity(IScheduleIntervalProcess process)
        {
            IScheduleInterval retVal = CoreInstance.Container.Get<IScheduleInterval>();

            return retVal;
        }

        protected override IScheduleInterval CreateEntity(IScheduleIntervalProcess process)
        {
            IScheduleInterval retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IScheduleInterval entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IScheduleInterval entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IScheduleInterval entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IScheduleInterval entity1, IScheduleInterval entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IScheduleInterval entity)
        {
            entity.Name = "Updated";
            entity.Description += "Updated";
        }
    }
}
