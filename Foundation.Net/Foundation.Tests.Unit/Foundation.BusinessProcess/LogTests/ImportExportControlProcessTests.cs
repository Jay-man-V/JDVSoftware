//-----------------------------------------------------------------------
// <copyright file="ImportExportControlProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ImportExportControlProcessTests
    /// </summary>
    [TestFixture]
    public class ImportExportControlProcessTests : CommonBusinessProcessTestBaseClass<IImportExportControl, IImportExportControlProcess, IImportExportControlRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 7;
        protected override String ExpectedScreenTitle => "Import/Export Control";
        protected override String ExpectedStatusBarText => "Number of Import/Export Controls:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ImportExportControl.Name;

        protected override IImportExportControlRepository CreateDataAccess()
        {
            IImportExportControlRepository dataAccess = Substitute.For<IImportExportControlRepository>();

            return dataAccess;
        }

        protected override IImportExportControlProcess CreateBusinessProcess()
        {
            IImportExportControlProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IImportExportControlProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IImportExportControlProcess process = new ImportExportControlProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IImportExportControl CreateBlankEntity(IImportExportControlProcess process)
        {
            IImportExportControl retVal = CoreInstance.Container.Get<IImportExportControl>();

            return retVal;
        }

        protected override IImportExportControl CreateEntity(IImportExportControlProcess process)
        {
            IImportExportControl retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ProcessedOn = new DateTime(2025, 01, 25, 23, 03, 15);
            retVal.Name = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IImportExportControl entity)
        {
            Assert.That(entity.ProcessedOn, Is.EqualTo(DateTime.MinValue));
            Assert.That(entity.Name, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IImportExportControl entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IImportExportControl entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IImportExportControl entity1, IImportExportControl entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ProcessedOn, Is.EqualTo(entity1.ProcessedOn));
            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
        }

        protected override void UpdateEntityProperties(IImportExportControl entity)
        {
            entity.ProcessedOn = entity.ProcessedOn.AddDays(1);
            entity.Name += "Updated";
        }
    }
}
