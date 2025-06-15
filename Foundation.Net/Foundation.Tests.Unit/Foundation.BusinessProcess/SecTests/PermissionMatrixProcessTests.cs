//-----------------------------------------------------------------------
// <copyright file="PermissionMatrixProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for PermissionMatrixProcessTests
    /// </summary>
    [TestFixture]
    public class PermissionMatrixProcessTests : CommonBusinessProcessTestBaseClass<IPermissionMatrix, IPermissionMatrixProcess, IPermissionMatrixRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 12;
        protected override String ExpectedScreenTitle => "Permissions Matrix";
        protected override String ExpectedStatusBarText => "Number of Permission Matrices:";

        protected override IPermissionMatrixRepository CreateDataAccess()
        {
            IPermissionMatrixRepository dataAccess = Substitute.For<IPermissionMatrixRepository>();

            return dataAccess;
        }

        protected override IPermissionMatrixProcess CreateBusinessProcess()
        {
            IPermissionMatrixProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IPermissionMatrixProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IRoleProcess roleProcess = Substitute.For<IRoleProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            IPermissionMatrixProcess process = new PermissionMatrixProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationProcess, roleProcess, userProfileProcess);

            return process;
        }

        protected override IPermissionMatrix CreateBlankEntity(IPermissionMatrixProcess process)
        {
            IPermissionMatrix retVal = CoreInstance.Container.Get<IPermissionMatrix>();

            return retVal;
        }

        protected override IPermissionMatrix CreateEntity(IPermissionMatrixProcess process)
        {
            IPermissionMatrix retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.RoleId = new EntityId(1);
            retVal.UserProfileId = new EntityId(1);
            retVal.FunctionKey = Guid.NewGuid().ToString();
            retVal.Permission = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IPermissionMatrix entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.RoleId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.UserProfileId, Is.EqualTo(new EntityId(0)));
            Assert.That(entity.FunctionKey, Is.EqualTo(null));
            Assert.That(entity.Permission, Is.EqualTo(null));
        }

        [TestCase]
        public override void Test_GetAllEntry()
        {
            IPermissionMatrixProcess process = CreateBusinessProcess();

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

        protected override void CheckAllEntry(IPermissionMatrix entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        [TestCase]
        public override void Test_GetNoneEntry()
        {
            IPermissionMatrixProcess process = CreateBusinessProcess();

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

        protected override void CheckNoneEntry(IPermissionMatrix entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        protected override void CompareEntityProperties(IPermissionMatrix entity1, IPermissionMatrix entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.RoleId, Is.EqualTo(entity1.RoleId));
            Assert.That(entity2.UserProfileId, Is.EqualTo(entity1.UserProfileId));
            Assert.That(entity2.FunctionKey, Is.EqualTo(entity1.FunctionKey));
            Assert.That(entity2.Permission, Is.EqualTo(entity1.Permission));
        }

        protected override void UpdateEntityProperties(IPermissionMatrix entity)
        {
            entity.FunctionKey += "Updated";
            entity.Permission += "Updated";
        }
    }
}
