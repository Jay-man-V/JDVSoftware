//-----------------------------------------------------------------------
// <copyright file="ApplicationRoleProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.SecTests
{
    /// <summary>
    /// Summary description for ApplicationRoleProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationRoleProcessTests : CommonBusinessProcessTestBaseClass<IApplicationRole, IApplicationRoleProcess, IApplicationRoleRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Application Roles";
        protected override String ExpectedStatusBarText => "Number of Application Roles:";

        protected override IApplicationRoleRepository CreateRepository()
        {
            IApplicationRoleRepository dataAccess = Substitute.For<IApplicationRoleRepository>();

            return dataAccess;
        }

        protected override IApplicationRoleProcess CreateBusinessProcess()
        {
            IApplicationRoleProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationRoleProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IRoleProcess roleProcess = Substitute.For<IRoleProcess>();

            IApplicationRoleProcess process = new ApplicationRoleProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository, applicationProcess, roleProcess);

            return process;
        }

        protected override IApplicationRole CreateBlankEntity(IApplicationRoleProcess process)
        {
            IApplicationRole retVal = CoreInstance.Container.Get<IApplicationRole>();

            return retVal;
        }

        protected override IApplicationRole CreateEntity(IApplicationRoleProcess process)
        {
            IApplicationRole retVal = CoreInstance.Container.Get<IApplicationRole>();

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.RoleId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IApplicationRole entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.RoleId, Is.EqualTo(new EntityId(0)));
        }

        protected override void CheckAllEntry(IApplicationRole entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        protected override void CheckNoneEntry(IApplicationRole entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        [TestCase]
        public override void Test_GetAllEntry()
        {
            IApplicationRoleProcess process = CreateBusinessProcess();

            String paramName = "ComboBoxDisplayMember";
            String errorMessage = $"Empty {paramName} passed to {process.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {paramName}";
            ArgumentNullException actualException = null;

            try
            {
                _ = process.GetAllEntry();
            }
            catch (ArgumentNullException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;
            String actualErrorParamName = actualException.ParamName;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualErrorParamName, Is.EqualTo(paramName));
        }

        [TestCase]
        public override void Test_GetNoneEntry()
        {
            IApplicationRoleProcess process = CreateBusinessProcess();

            String paramName = "ComboBoxDisplayMember";
            String errorMessage = $"Empty {paramName} passed to {process.GetType()}.SetFilterItemProperties{Environment.NewLine}Parameter name: {paramName}";
            ArgumentNullException actualException = null;

            try
            {
                _ = process.GetNoneEntry();
            }
            catch (ArgumentNullException exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = actualException.Message;
            String actualErrorParamName = actualException.ParamName;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualErrorParamName, Is.EqualTo(paramName));
        }

        protected override void CompareEntityProperties(IApplicationRole entity1, IApplicationRole entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.RoleId, Is.EqualTo(entity1.RoleId));
        }

        protected override void UpdateEntityProperties(IApplicationRole entity)
        {
            entity.ApplicationId = new AppId(2);
            entity.RoleId = new EntityId(2);
        }
    }
}
