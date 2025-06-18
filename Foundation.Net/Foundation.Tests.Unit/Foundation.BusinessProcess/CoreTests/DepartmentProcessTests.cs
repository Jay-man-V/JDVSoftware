//-----------------------------------------------------------------------
// <copyright file="DepartmentProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for DepartmentProcessTests
    /// </summary>
    [TestFixture]
    public class DepartmentProcessTests : CommonBusinessProcessTestBaseClass<IDepartment, IDepartmentProcess, IDepartmentRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 10;
        protected override String ExpectedScreenTitle => "Departments";
        protected override String ExpectedStatusBarText => "Number of Departments:";

        protected override string ExpectedComboBoxDisplayMember => FDC.Department.Code;

        protected override IDepartmentRepository CreateRepository()
        {
            IDepartmentRepository dataAccess = Substitute.For<IDepartmentRepository>();

            return dataAccess;
        }

        protected override IDepartmentProcess CreateBusinessProcess()
        {
            IDepartmentProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IDepartmentProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IDepartmentProcess process = new DepartmentProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IDepartment CreateBlankEntity(IDepartmentProcess process)
        {
            IDepartment retVal = CoreInstance.Container.Get<IDepartment>();

            return retVal;
        }

        protected override IDepartment CreateEntity(IDepartmentProcess process)
        {
            IDepartment retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Code = Guid.NewGuid().ToString();
            retVal.ShortName = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IDepartment entity)
        {
            Assert.That(entity.Code, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IDepartment entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IDepartment entity)
        {
            Assert.That(entity.Code, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IDepartment entity1, IDepartment entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Code, Is.EqualTo(entity1.Code));
            Assert.That(entity2.ShortName, Is.EqualTo(entity1.ShortName));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IDepartment entity)
        {
            entity.ShortName += "Updated";
            entity.Description += "Updated";
        }
    }
}
