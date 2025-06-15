//-----------------------------------------------------------------------
// <copyright file="SaveNewEntitySimpleTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using Foundation.Tests.System.Support;
using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using Foundation.Interfaces;

namespace Foundation.Tests.System.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Save New Entity Tests
    /// </summary>
    /// <see cref="DataAccessSystemTestBase" />
    [TestFixture]
    public class CreateEntitySimpleTests : DataAccessSystemTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Properties()
        {
            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            ApplicationRole expectedApplicationRole = ApplicationRole.Creator;
            ApplicationRole actualApplicationRole = obj.GetRequiredMinimumCreateRole();

            Assert.That(actualApplicationRole, Is.EqualTo(expectedApplicationRole));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_SaveNullEntity()
        {
            String parameterName = "entity";
            String errorMessage = String.Format(ErrorMessages.ArgumentNullExpectedErrorMessage, parameterName);
            Exception actualException = null;

            try
            {
                SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                const MockFoundationModel testEntity1 = null;

                obj.Save(testEntity1);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());
            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_SaveNullList()
        {
            String parameterName = "entities";
            String errorMessage = String.Format(ErrorMessages.ArgumentNullExpectedErrorMessage, parameterName);
            Exception actualException = null;

            try
            {
                SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                const List<IMockFoundationModel> entities = null;

                obj.Save(entities);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());
            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_SaveEmptyList()
        {
            List<IMockFoundationModel> entities = new List<IMockFoundationModel>();

            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            obj.Save(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_SaveValidEntity_Complex()
        {
            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

            obj.Save(testEntity1);

            TestEntitySupport.AssertTestSavedEntity(testEntity1, 1);

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);
            TestEntitySupport.AssertTestSavedEntity(loadedEntity1, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_SaveValidEntity_NoCreatedById()
        {
            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);
            testEntity1.CreatedByUserProfileId = new EntityId(-1);

            obj.Save(testEntity1);

            TestEntitySupport.AssertTestSavedEntity(testEntity1, 1);

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);
            TestEntitySupport.AssertTestSavedEntity(loadedEntity1, 1);
        }

        [TestCase]
        public void Test_SaveMultipleEntities()
        {
            List<IMockFoundationModel> entities = new List<IMockFoundationModel>
            {
                TestEntitySupport.CreateTestEntity(CoreInstance, 1),
                TestEntitySupport.CreateTestEntity(CoreInstance, 2),
                TestEntitySupport.CreateTestEntity(CoreInstance, 3),
                TestEntitySupport.CreateTestEntity(CoreInstance, 4)
            };

            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);
            obj.Save(entities);

            TestEntitySupport.AssertTestSavedEntity(entities[0], 1);
            TestEntitySupport.AssertTestSavedEntity(entities[1], 2);
            TestEntitySupport.AssertTestSavedEntity(entities[2], 3);
            TestEntitySupport.AssertTestSavedEntity(entities[3], 4);

            IMockFoundationModel loadedEntity = obj.Get(new EntityId(1));
            TestEntitySupport.AssertTestSavedEntity(loadedEntity, 1);

            loadedEntity = obj.Get(new EntityId(2));
            TestEntitySupport.AssertTestSavedEntity(loadedEntity, 2);

            loadedEntity = obj.Get(new EntityId(3));
            TestEntitySupport.AssertTestSavedEntity(loadedEntity, 3);

            loadedEntity = obj.Get(new EntityId(4));
            TestEntitySupport.AssertTestSavedEntity(loadedEntity, 4);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Create_PermissionsException()
        {
            String errorMessage = @"User: '<<domain\user>>' does not have the required permissions. Required permission is: 'Creator'";
            Exception actualException = null;

            try
            {
                SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                CoreInstance.CurrentLoggedOnUser.UserProfile.Roles.Clear();

                obj.Save(testEntity1);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ApplicationPermissionsException>());

            String actualErrorMessage = ReplaceUserNameWithConstant(actualException.Message);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }
    }
}
