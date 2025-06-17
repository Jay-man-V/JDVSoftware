//-----------------------------------------------------------------------
// <copyright file="ApplicationProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.SecTests
{
    /// <summary>
    /// Summary description for ApplicationProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationProcessTests : CommonBusinessProcessTestBaseClass<IApplication, IApplicationProcess, IApplicationRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Applications";
        protected override String ExpectedStatusBarText => "Number of Applications:";

        protected override String ExpectedComboBoxDisplayMember => FDC.Application.Name;

        protected override IApplicationRepository CreateDataAccess()
        {
            IApplicationRepository dataAccess = Substitute.For<IApplicationRepository>();

            return dataAccess;
        }

        protected override IApplicationProcess CreateBusinessProcess()
        {
            IApplicationProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess process = new ApplicationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IApplication CreateBlankEntity(IApplicationProcess process)
        {
            IApplication retVal = CoreInstance.Container.Get<IApplication>();

            return retVal;
        }

        protected override IApplication CreateEntity(IApplicationProcess process)
        {
            IApplication retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Id = new AppId(1);
            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IApplication entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IApplication entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IApplication entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityBaseProperties_Id(IApplication expectedEntity, IApplication actualEntity)
        {
            Assert.That(actualEntity.Id, Is.EqualTo(expectedEntity.Id));
        }

        protected override void Test_NullId(IApplicationProcess process)
        {
            Assert.That(process.NullId, Is.EqualTo(new AppId(ExpectedNullId.ToInteger())));
        }

        protected override void Test_AllId(IApplicationProcess process)
        {
            Assert.That(process.AllId, Is.EqualTo(new AppId(ExpectedAllId.ToInteger())));
        }

        protected override void Test_NoneId(IApplicationProcess process)
        {
            Assert.That(process.NoneId, Is.EqualTo(new AppId(ExpectedNoneId.ToInteger())));
        }

        protected override void CompareEntityProperties(IApplication entity1, IApplication entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IApplication entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
