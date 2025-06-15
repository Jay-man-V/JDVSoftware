//-----------------------------------------------------------------------
// <copyright file="RoleProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for RoleProcessTests
    /// </summary>
    [TestFixture]
    public class RoleProcessTests : CommonBusinessProcessTestBaseClass<IRole, IRoleProcess, IRoleRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 10;
        protected override String ExpectedScreenTitle => "Roles";
        protected override String ExpectedStatusBarText => "Number of Roles:";

        protected override String ExpectedComboBoxDisplayMember => FDC.Role.Name;

        protected override IRoleRepository CreateDataAccess()
        {
            IRoleRepository dataAccess = Substitute.For<IRoleRepository>();

            return dataAccess;
        }

        protected override IRoleProcess CreateBusinessProcess()
        {
            IRoleProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IRoleProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IRoleProcess process = new RoleProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IRole CreateBlankEntity(IRoleProcess process)
        {
            IRole retVal = CoreInstance.Container.Get<IRole>();

            return retVal;
        }

        protected override IRole CreateEntity(IRoleProcess process)
        {
            IRole retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IRole entity)
        {
            Assert.That(entity.Name, Is.EqualTo(null));
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IRole entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IRole entity)
        {
            Assert.That(entity.Name, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IRole entity1, IRole entity2)
        {
            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));

            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));
        }

        protected override void UpdateEntityProperties(IRole entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
