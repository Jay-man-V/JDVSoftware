//-----------------------------------------------------------------------
// <copyright file="CommonBusinessProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Mocks;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess
{
    /// <summary>
    /// Summary description for CommonBusinessProcessTests
    /// </summary>
    [TestFixture]
    public class CommonBusinessProcessTests : CommonBusinessProcessTestBaseClass<IMockFoundationModel, IMockFoundationModelProcess, IMockFoundationModelRepository>
    {
        protected override Int32 ColumnDefinitionsCount => 19;
        //protected override String ExpectedScreenTitle => "Mock Foundations";
        //protected override String ExpectedStatusBarText => "Number of Mock Foundation rows:";

        protected override string ExpectedComboBoxDisplayMember => "Made up property name";

        protected override String ExpectedScreenTitle => String.Empty;
        protected override String ExpectedStatusBarText => "Number of rows:";

        //protected override string ExpectedComboBoxDisplayMember => String.Empty;

        protected override IMockFoundationModelRepository CreateRepository()
        {
            IMockFoundationModelRepository repository = Substitute.For<IMockFoundationModelRepository>();

            return repository;
        }

        protected override IMockFoundationModelProcess CreateBusinessProcess()
        {
            IMockFoundationModelProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IMockFoundationModelProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IMockFoundationModelProcess process = new MockFoundationModelProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, Repository, StatusRepository, UserProfileRepository);

            return process;
        }

        protected override IMockFoundationModel CreateBlankEntity(IMockFoundationModelProcess process)
        {
            IMockFoundationModel retVal = CoreInstance.Container.Get<IMockFoundationModel>();

            return retVal;
        }

        protected override IMockFoundationModel CreateEntity(IMockFoundationModelProcess process)
        {
            IMockFoundationModel retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.Name = Guid.NewGuid().ToString();
            retVal.Description = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBaseClassProperties(IMockFoundationModelProcess process)
        {
        }

        protected override void CheckBlankEntry(IMockFoundationModel entity)
        {
            Assert.That(entity.Description, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IMockFoundationModel entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        protected override void CheckNoneEntry(IMockFoundationModel entity)
        {
            Assert.Fail($"{LocationUtils.GetFunctionName()} should not be called as it is not implemented for the Business Process");
        }

        //[TestCase]
        //public override void Test_GetAllEntry()
        //{
        //    IMockFoundationModelProcess process = CreateBusinessProcess();

        //    String paramName = "Made up property name";
        //    String typeName = typeof(MockFoundationModel).FullName;
        //    String errorMessage = $"Member '{typeName}.{paramName}' not found.";
        //    MissingMemberException actualException = null;

        //    try
        //    {
        //        _ = process.GetAllEntry();
        //    }
        //    catch (MissingMemberException exception)
        //    {
        //        actualException = exception;
        //    }

        //    Assert.That(actualException, Is.Not.EqualTo(null));

        //    String actualErrorMessage = actualException.Message;
        //    //String actualErrorParamName = mmException.;

        //    Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        //    //Assert.That(actualErrorParamName, Is.EqualTo(paramName));
        //}

        //[TestCase]
        //public override void Test_GetNoneEntry()
        //{
        //    IMockFoundationModelProcess process = CreateBusinessProcess();

        //    String paramName = "Made up property name";
        //    String typeName = typeof(MockFoundationModel).FullName;
        //    String errorMessage = $"Member '{typeName}.{paramName}' not found.";
        //    MissingMemberException actualException = null;

        //    try
        //    {
        //        IMockFoundationModel _ = process.GetNoneEntry();
        //    }
        //    catch (MissingMemberException exception)
        //    {
        //        actualException = exception;
        //    }

        //    Assert.That(actualException, Is.Not.EqualTo(null));

        //    String actualErrorMessage = actualException.Message;
        //    //String actualErrorParamName = mmException.;

        //    Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        //    //Assert.That(actualErrorParamName, Is.EqualTo(paramName));
        //}

        protected override void CompareEntityProperties(IMockFoundationModel entity1, IMockFoundationModel entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.Description, Is.EqualTo(entity1.Description));
        }

        protected override void UpdateEntityProperties(IMockFoundationModel entity)
        {
            entity.Name += "Updated";
            entity.Description += "Updated";
        }
    }
}
