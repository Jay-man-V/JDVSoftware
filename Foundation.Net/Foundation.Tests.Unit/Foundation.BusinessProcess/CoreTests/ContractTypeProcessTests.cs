//-----------------------------------------------------------------------
// <copyright file="ContractTypeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ContractTypeProcessTests
    /// </summary>
    [TestFixture]
    public class ContractTypeProcessTests : CommonBusinessProcessTestBaseClass<IContractType, IContractTypeProcess, IContractTypeRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Contract Types";
        protected override String ExpectedStatusBarText => "Number of Contract Types:";

        protected override string ExpectedComboBoxDisplayMember => FDC.ContractType.Description;

        protected override IContractTypeRepository CreateRepository()
        {
            IContractTypeRepository dataAccess = Substitute.For<IContractTypeRepository>();

            return dataAccess;
        }

        protected override IContractTypeProcess CreateBusinessProcess()
        {
            IContractTypeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IContractTypeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IContractTypeProcess process = new ContractTypeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IContractType CreateBlankEntity(IContractTypeProcess process)
        {
            IContractType retVal = CoreInstance.Container.Get<IContractType>();

            return retVal;
        }

        protected override IContractType CreateEntity(IContractTypeProcess process)
        {
            IContractType retVal = CreateBlankEntity(process);

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Description = Guid.NewGuid().ToString();
            retVal.Name = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IContractType entity)
        {
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IContractType entity)
        {
            Assert.That(entity.Description, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IContractType entity)
        {
            Assert.That(entity.Description, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IContractType entity1, IContractType entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IContractType entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
