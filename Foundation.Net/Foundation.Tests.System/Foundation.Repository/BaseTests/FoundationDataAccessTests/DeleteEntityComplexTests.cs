//-----------------------------------------------------------------------
// <copyright file="DeleteEntityComplexTests.cs" company="JDV Software Ltd">
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
    /// Delete Entity Tests
    /// </summary>
    /// <see cref="DataAccessSystemTestBase" />
    [TestFixture]
    public class DeleteEntityComplexTests : DataAccessSystemTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Properties()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            ApplicationRole expectedApplicationRole = ApplicationRole.OwnDelete;
            ApplicationRole actualApplicationRole = obj.GetRequiredMinimumDeleteRole();

            Assert.That(actualApplicationRole, Is.EqualTo(expectedApplicationRole));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_DeleteValidEntity_1()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

            obj.Save(testEntity1);

            testEntity1.EntityLife = EntityLife.Deleted;

            obj.Save(testEntity1);

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);

            Assert.That(loadedEntity1.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(loadedEntity1.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(loadedEntity1.EntityLife, Is.EqualTo(EntityLife.Loaded));

            Assert.That(loadedEntity1.Timestamp, Is.EquivalentTo(testEntity1.Timestamp));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_DeleteValidEntity_2()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);


            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

            obj.Save(testEntity1);

            testEntity1.EntityLife = EntityLife.Deleted;

            obj.Delete(testEntity1);

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);

            Assert.That(loadedEntity1.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(loadedEntity1.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(loadedEntity1.EntityLife, Is.EqualTo(EntityLife.Loaded));

            Assert.That(loadedEntity1.Timestamp, Is.EquivalentTo(testEntity1.Timestamp));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_DeleteMultipleEntities()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);
            IMockFoundationModel testEntity2 = TestEntitySupport.CreateTestEntity(CoreInstance, 2);
            IMockFoundationModel testEntity3 = TestEntitySupport.CreateTestEntity(CoreInstance, 3);

            List<IMockFoundationModel> testEntities = new List<IMockFoundationModel> { testEntity1, testEntity2, testEntity3 };

            obj.Save(testEntities);

            testEntity1.EntityLife = EntityLife.Deleted;
            testEntity2.EntityLife = EntityLife.Deleted;
            testEntity3.EntityLife = EntityLife.Deleted;

            obj.Delete(testEntities);

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);

            Assert.That(loadedEntity1.EntityStatus, Is.EqualTo(EntityStatus.Inactive));
            Assert.That(loadedEntity1.EntityState, Is.EqualTo(EntityState.Saved));
            Assert.That(loadedEntity1.EntityLife, Is.EqualTo(EntityLife.Loaded));

            Assert.That(loadedEntity1.Timestamp, Is.EquivalentTo(testEntity1.Timestamp));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Delete_ArgumentNullException()
        {
            String parameterName = "entities";
            String errorMessage = String.Format(ErrorMessages.ArgumentNullExpectedErrorMessage, parameterName);
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                const List<IMockFoundationModel> testEntities = null;

                obj.Delete(testEntities);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Delete_ConcurrencyException_1()
        {
            String errorMessage = "Unable to update record. It may have been updated by someone else since it was loaded. Record Id: '-1', Name: 'TestEntity', Table: '[TestEntity]'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                testEntity1.EntityLife = EntityLife.Deleted;

                obj.Save(testEntity1);
            }
            catch(Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ApplicationConcurrencyException>());

            String actualErrorMessage = ReplaceDateTimeWithConstant(actualException.Message);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Delete_ConcurrencyException_2()
        {
            String errorMessage = "Unable to update record. It may have been updated by someone else since it was loaded. Record Id: '1', Name: 'TestEntity', Table: '[TestEntity]', Last saved by: 'System', Last saved time: '<<dd-MMM-yyyy HH:mm:ss>>'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                obj.Save(testEntity1);

                MockFoundationModel testEntity2 = testEntity1.Clone() as MockFoundationModel;
                testEntity2.Name = "Modified TestEntity2";
                obj.Save(testEntity2);

                testEntity1.EntityLife = EntityLife.Deleted;

                obj.Save(testEntity1);
            }
            catch(Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ApplicationConcurrencyException>());

            String actualErrorMessage = ReplaceDateTimeWithConstant(actualException.Message);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Delete_ConcurrencyException_3()
        {
            String errorMessage = "Unable to update record. It may have been updated by someone else since it was loaded. Record Id: '-1', Name: 'TestEntity', Table: '[TestEntity]'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                testEntity1.EntityLife = EntityLife.Deleted;

                obj.Save(testEntity1);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ApplicationConcurrencyException>());

            String actualErrorMessage = ReplaceDateTimeWithConstant(actualException.Message);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Delete_PermissionsException_1()
        {
            String errorMessage = @"User: '<<domain\user>>' does not have the required permissions. Required permission is: 'OwnDelete'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                obj.Save(testEntity1);

                CoreInstance.CurrentLoggedOnUser.UserProfile.Roles.Clear();

                testEntity1.EntityLife = EntityLife.Deleted;

                obj.Save(testEntity1);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            String actualErrorMessage = ReplaceUserNameWithConstant(actualException.Message);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }
    }
}
