//-----------------------------------------------------------------------
// <copyright file="ApplicationUserRoleProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ApplicationUserRoleProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationUserRoleProcessTests : CommonBusinessProcessTestBaseClass<IApplicationUserRole, IApplicationUserRoleProcess, IApplicationUserRoleRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "Application/User/Roles";
        protected override String ExpectedStatusBarText => "Number of Application/User/Roles:";

        protected override IApplicationUserRoleRepository CreateDataAccess()
        {
            IApplicationUserRoleRepository dataAccess = Substitute.For<IApplicationUserRoleRepository>();

            return dataAccess;
        }

        protected override IApplicationUserRoleProcess CreateBusinessProcess()
        {
            IApplicationUserRoleProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationUserRoleProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IRoleProcess roleProcess = Substitute.For<IRoleProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            IApplicationUserRoleProcess process = new ApplicationUserRoleProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationProcess, roleProcess, userProfileProcess);

            return process;
        }

        protected override IApplicationUserRole CreateBlankEntity(IApplicationUserRoleProcess process)
        {
            IApplicationUserRole retVal = CoreInstance.Container.Get<IApplicationUserRole>();

            return retVal;
        }

        protected override IApplicationUserRole CreateEntity(IApplicationUserRoleProcess process)
        {
            IApplicationUserRole retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.UserProfileId = new EntityId(1);
            retVal.RoleId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IApplicationUserRole entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.UserProfileId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.RoleId, Is.EqualTo(new EntityId(0)));
        }

        protected override void CheckAllEntry(IApplicationUserRole entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        protected override void CheckNoneEntry(IApplicationUserRole entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        [TestCase]
        public override void Test_GetAllEntry()
        {
            IApplicationUserRoleProcess process = CreateBusinessProcess();

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
            IApplicationUserRoleProcess process = CreateBusinessProcess();

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

        protected override void CompareEntityProperties(IApplicationUserRole entity1, IApplicationUserRole entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.UserProfileId, Is.EqualTo(entity1.UserProfileId));
            Assert.That(entity2.RoleId, Is.EqualTo(entity1.RoleId));
        }

        protected override void UpdateEntityProperties(IApplicationUserRole entity)
        {
            entity.ApplicationId = new AppId(2);
            entity.UserProfileId = new EntityId(2);
            entity.RoleId = new EntityId(2);
        }
    }
}
