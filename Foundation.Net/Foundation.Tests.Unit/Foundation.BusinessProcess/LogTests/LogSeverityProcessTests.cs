//-----------------------------------------------------------------------
// <copyright file="LogSeverityProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for LogSeverityProcessTests
    /// </summary>
    [TestFixture]
    public class LogSeverityProcessTests : CommonBusinessProcessTestBaseClass<ILogSeverity, ILogSeverityProcess, ILogSeverityRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Log Severities";
        protected override String ExpectedStatusBarText => "Number of Log Severities:";

        protected override String ExpectedComboBoxDisplayMember => FDC.LogSeverity.Code;

        protected override ILogSeverityRepository CreateRepository()
        {
            ILogSeverityRepository dataAccess = Substitute.For<ILogSeverityRepository>();

            return dataAccess;
        }

        protected override ILogSeverityProcess CreateBusinessProcess()
        {
            ILogSeverityProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ILogSeverityProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            ILogSeverityProcess process = new LogSeverityProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override ILogSeverity CreateBlankEntity(ILogSeverityProcess process)
        {
            ILogSeverity retVal = CoreInstance.Container.Get<ILogSeverity>();

            return retVal;
        }

        protected override ILogSeverity CreateEntity(ILogSeverityProcess process)
        {
            ILogSeverity retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            IRandomService randomService = CoreInstance.Container.Get<IRandomService>();

            retVal.Code = randomService.NextInt32(9999).ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(ILogSeverity entity)
        {
            Assert.That(entity.Code, Is.EqualTo(null));
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ILogSeverity entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ILogSeverity entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ILogSeverity entity1, ILogSeverity entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Code, Is.EqualTo(entity1.Code));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(ILogSeverity entity)
        {
            entity.Code += "Updated";
            entity.Description += "Updated";
        }
    }
}
