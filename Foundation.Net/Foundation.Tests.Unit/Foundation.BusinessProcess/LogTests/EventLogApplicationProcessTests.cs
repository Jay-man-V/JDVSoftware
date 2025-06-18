//-----------------------------------------------------------------------
// <copyright file="EventLogApplicationProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.LogTests
{
    /// <summary>
    /// Summary description for EventLogApplicationProcessTests
    /// </summary>
    [TestFixture]
    public class EventLogApplicationProcessTests : CommonBusinessProcessTestBaseClass<IEventLogApplication, IEventLogApplicationProcess, IEventLogApplicationRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 10;
        protected override String ExpectedScreenTitle => "Event Log Applications";
        protected override String ExpectedStatusBarText => "Number of Event Log Applications:";

        protected override String ExpectedComboBoxDisplayMember => FDC.EventLogApplication.ShortName;

        protected override IEventLogApplicationRepository CreateRepository()
        {
            IEventLogApplicationRepository dataAccess = Substitute.For<IEventLogApplicationRepository>();

            return dataAccess;
        }

        protected override IEventLogApplicationProcess CreateBusinessProcess()
        {
            IEventLogApplicationProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IEventLogApplicationProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationRepository applicationRepository = Substitute.For<IApplicationRepository>();
            IApplicationProcess applicationProcess = new ApplicationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, applicationRepository, StatusRepository, UserProfileRepository);

            IEventLogApplicationProcess process = new EventLogApplicationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, applicationProcess);

            return process;
        }

        protected override IEventLogApplication CreateBlankEntity(IEventLogApplicationProcess process)
        {
            IEventLogApplication retVal = CoreInstance.Container.Get<IEventLogApplication>();

            return retVal;
        }

        protected override IEventLogApplication CreateEntity(IEventLogApplicationProcess process)
        {
            IEventLogApplication retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.ProcessName = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IEventLogApplication entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(null));
            Assert.That(entity.ProcessName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IEventLogApplication entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IEventLogApplication entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IEventLogApplication entity1, IEventLogApplication entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.ProcessName, Is.EqualTo(entity1.ProcessName));
        }

        protected override void UpdateEntityProperties(IEventLogApplication entity)
        {
            entity.ShortName += "Updated";
            entity.ProcessName += "Updated";
        }
    }
}
