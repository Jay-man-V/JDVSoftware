//-----------------------------------------------------------------------
// <copyright file="ApplicationTypeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ApplicationTypeProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationTypeProcessTests : CommonBusinessProcessTestBaseClass<IApplicationType, IApplicationTypeProcess, IApplicationTypeRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Application Types";
        protected override String ExpectedStatusBarText => "Number of Application Types:";

        protected override String ExpectedComboBoxDisplayMember => FDC.ApplicationType.Name;


        protected override IApplicationTypeRepository CreateDataAccess()
        {
            IApplicationTypeRepository dataAccess = Substitute.For<IApplicationTypeRepository>();

            return dataAccess;
        }

        protected override IApplicationTypeProcess CreateBusinessProcess()
        {
            IApplicationTypeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationTypeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationTypeProcess process = new ApplicationTypeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IApplicationType CreateBlankEntity(IApplicationTypeProcess process)
        {
            IApplicationType retVal = CoreInstance.Container.Get<IApplicationType>();

            return retVal;
        }

        protected override IApplicationType CreateEntity(IApplicationTypeProcess process)
        {
            IApplicationType retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IApplicationType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IApplicationType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IApplicationType entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IApplicationType entity1, IApplicationType entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IApplicationType entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
