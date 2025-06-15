//-----------------------------------------------------------------------
// <copyright file="ApplicationApplicationTypeProcessTests.cs" company="JDV Software Ltd">
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
    /// Summary description for ApplicationApplicationTypeProcessTests
    /// </summary>
    [TestFixture]
    public class ApplicationApplicationTypeProcessTests : CommonBusinessProcessTestBaseClass<IApplicationApplicationType, IApplicationApplicationTypeProcess, IApplicationApplicationTypeRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Application/Application Types";
        protected override String ExpectedStatusBarText => "Number of Application/Application Types:";

        protected override IApplicationApplicationTypeRepository CreateDataAccess()
        {
            IApplicationApplicationTypeRepository dataAccess = Substitute.For<IApplicationApplicationTypeRepository>();

            return dataAccess;
        }

        protected override IApplicationApplicationTypeProcess CreateBusinessProcess()
        {
            IApplicationApplicationTypeProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IApplicationApplicationTypeProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IApplicationTypeProcess applicationTypeProcess = Substitute.For<IApplicationTypeProcess>();

            IApplicationApplicationTypeProcess process = new ApplicationApplicationTypeProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationProcess, applicationTypeProcess);

            return process;
        }

        protected override IApplicationApplicationType CreateBlankEntity(IApplicationApplicationTypeProcess process)
        {
            IApplicationApplicationType retVal = CoreInstance.Container.Get<IApplicationApplicationType>();

            return retVal;
        }

        protected override IApplicationApplicationType CreateEntity(IApplicationApplicationTypeProcess process)
        {
            IApplicationApplicationType retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.ApplicationTypeId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IApplicationApplicationType entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.ApplicationTypeId, Is.EqualTo(new EntityId(0)));
        }

        protected override void CheckAllEntry(IApplicationApplicationType entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        protected override void CheckNoneEntry(IApplicationApplicationType entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        [TestCase]
        public override void Test_GetAllEntry()
        {
            IApplicationApplicationTypeProcess process = CreateBusinessProcess();

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
            IApplicationApplicationTypeProcess process = CreateBusinessProcess();

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

        protected override void CompareEntityProperties(IApplicationApplicationType entity1, IApplicationApplicationType entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.ApplicationTypeId, Is.EqualTo(entity1.ApplicationTypeId));
        }

        protected override void UpdateEntityProperties(IApplicationApplicationType entity)
        {
            entity.ApplicationId = new AppId(2);
            entity.ApplicationTypeId = new EntityId(2);
        }
    }
}
