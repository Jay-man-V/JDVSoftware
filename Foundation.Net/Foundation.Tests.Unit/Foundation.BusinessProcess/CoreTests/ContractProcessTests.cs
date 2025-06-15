//-----------------------------------------------------------------------
// <copyright file="ContractProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ContractProcessTests
    /// </summary>
    [TestFixture]
    public class ContractProcessTests : CommonBusinessProcessTestBaseClass<IContract, IContractProcess, IContractRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 13;
        protected override String ExpectedScreenTitle => "Contracts";
        protected override String ExpectedStatusBarText => "Number of Contracts:";

        protected override Boolean ExpectedHasOptionalDropDownParameter1 => true;
        protected override String ExpectedFilter1Name => "Contract Type:";
        protected override string ExpectedFilter1DisplayMemberPath => FDC.ContractType.Description;

        protected override string ExpectedComboBoxDisplayMember => FDC.Contract.ShortName;

        private readonly DateTime _contractStartDate = new DateTime(2022, 01, 01, 0, 0, 0);
        private readonly DateTime _contractEndDate = new DateTime(2022, 12, 31, 23, 59, 59);

        protected override IContractRepository CreateDataAccess()
        {
            IContractRepository dataAccess = Substitute.For<IContractRepository>();

            return dataAccess;
        }

        protected override IContractProcess CreateBusinessProcess()
        {
            IContractProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IContractProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IContractTypeProcess contractTypeProcess = Substitute.For<IContractTypeProcess>();

            CopyProperties(contractTypeProcess, CoreInstance.Container.Get<IContractTypeProcess>());

            IContractProcess process = new ContractProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, contractTypeProcess);

            return process;
        }

        protected override IContract CreateBlankEntity(IContractProcess process)
        {
            IContract retVal = CoreInstance.Container.Get<IContract>();

            return retVal;
        }

        protected override IContract CreateEntity(IContractProcess process)
        {
            IContract retVal = CreateBlankEntity(process);

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ContractTypeId = new EntityId(1);
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.FullName = retVal.ShortName.Substring(0, 24);
            retVal.ContractReference = retVal.ShortName;
            retVal.StartDate = _contractStartDate;
            retVal.EndDate = _contractEndDate;

            return retVal;
        }

        protected override void CheckBlankEntry(IContract entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IContract entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IContract entity)
        {
            Assert.That(entity.ShortName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IContract entity1, IContract entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ContractTypeId, Is.EqualTo(entity1.ContractTypeId));
            Assert.That(entity2.StartDate, Is.EqualTo(entity1.StartDate));
            Assert.That(entity2.EndDate, Is.EqualTo(entity1.EndDate));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.FullName, Is.EqualTo(entity1.FullName));
            Assert.That(entity2.ContractReference, Is.EqualTo(entity1.ContractReference));
        }

        protected override void UpdateEntityProperties(IContract entity)
        {
            entity.ShortName += "Updated";
            entity.ContractReference += "Updated";
        }

        [TestCase]
        public void Test_ApplyFilter_ContractType()
        {
            List<IContract> contracts = new List<IContract>();
            IContractProcess process = CreateBusinessProcess();

            IContractType contractType1 = CoreInstance.Container.Get<IContractType>();
            contractType1.Id = new EntityId(1);

            IContractType contractType2 = CoreInstance.Container.Get<IContractType>();
            contractType2.Id = new EntityId(2);

            contracts.Add(CreateEntity(process));
            contracts.Add(CreateEntity(process));
            contracts.Add(CreateEntity(process));
            contracts.Add(CreateEntity(process));
            contracts.Add(CreateEntity(process));

            contracts[0].Id = new EntityId(0);
            contracts[0].ContractTypeId = new EntityId(1);

            contracts[1].Id = new EntityId(1);
            contracts[1].ContractTypeId = new EntityId(2);

            contracts[2].Id = new EntityId(2);
            contracts[2].ContractTypeId = new EntityId(1);

            contracts[3].Id = new EntityId(3);
            contracts[3].ContractTypeId = new EntityId(2);

            contracts[4].Id = new EntityId(4);
            contracts[4].ContractTypeId = new EntityId(1);

            List<IContract> filteredContacts1 = process.ApplyFilter(contracts, contractType1);
            Assert.That(filteredContacts1.Count, Is.EqualTo(3));

            List<IContract> filteredContacts2 = process.ApplyFilter(contracts, contractType2);
            Assert.That(filteredContacts2.Count, Is.EqualTo(2));
        }
    }
}
