//-----------------------------------------------------------------------
// <copyright file="UpdateEntityComplexTests.cs" company="JDV Software Ltd">
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
    /// Update Entity Tests
    /// </summary>
    /// <see cref="DataAccessSystemTestBase" />
    [TestFixture]
    public class UpdateEntityComplexTests : DataAccessSystemTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Properties()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            ApplicationRole expectedEditApplicationRole = ApplicationRole.OwnEditor;
            ApplicationRole actualEditApplicationRole = obj.GetRequiredMinimumEditRole();

            Assert.That(actualEditApplicationRole, Is.EqualTo(expectedEditApplicationRole));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_UpdateValidEntity()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

            obj.Save(testEntity1);

            Byte[] timestamp = testEntity1.Timestamp;

            UpdateTestEntity(testEntity1, 1, 1);

            obj.Save(testEntity1);

            Assert.That(testEntity1.Timestamp, Is.Not.EqualTo(timestamp));

            IMockFoundationModel loadedEntity1 = obj.Get(testEntity1.Id);
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity1, 1, 1);
        }

        [TestCase]
        public void Test_SaveMultipleEntities()
        {
            // Arrange Save pass 1

            List<IMockFoundationModel> entities = new List<IMockFoundationModel>()
            {
                TestEntitySupport.CreateTestEntity(CoreInstance, 1),
                TestEntitySupport.CreateTestEntity(CoreInstance, 2),
                TestEntitySupport.CreateTestEntity(CoreInstance, 3),
                TestEntitySupport.CreateTestEntity(CoreInstance, 4)
            };

            // Act Save pass 1

            IMockFoundationModel loadedEntity;
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            obj.Save(entities);

            UpdateTestEntity(entities[0], 1, 1);
            UpdateTestEntity(entities[1], 1, 2);
            UpdateTestEntity(entities[2], 1, 3);
            UpdateTestEntity(entities[3], 1, 4);
            obj.Save(entities);

            // Assert Save pass 1

            loadedEntity = obj.Get(new EntityId(1));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 1, 1);

            loadedEntity = obj.Get(new EntityId(2));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 1, 2);

            loadedEntity = obj.Get(new EntityId(3));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 1, 3);

            loadedEntity = obj.Get(new EntityId(4));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 1, 4);

            // Arrange Save pass 2

            UpdateTestEntity(entities[0], 2, 1);
            UpdateTestEntity(entities[1], 2, 2);
            UpdateTestEntity(entities[2], 2, 3);
            UpdateTestEntity(entities[3], 2, 4);
            obj.Save(entities);

            // Assert Save pass 2

            loadedEntity = obj.Get(new EntityId(1));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 2, 1);

            loadedEntity = obj.Get(new EntityId(2));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 2, 2);

            loadedEntity = obj.Get(new EntityId(3));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 2, 3);

            loadedEntity = obj.Get(new EntityId(4));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 2, 4);

            // Arrange Save pass 3

            UpdateTestEntity(entities[0], 3, 1);
            UpdateTestEntity(entities[1], 3, 2);
            UpdateTestEntity(entities[2], 3, 3);
            UpdateTestEntity(entities[3], 3, 4);
            obj.Save(entities);

            // Assert Save pass 3

            loadedEntity = obj.Get(new EntityId(1));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 3, 1);

            loadedEntity = obj.Get(new EntityId(2));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 3, 2);

            loadedEntity = obj.Get(new EntityId(3));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 3, 3);

            loadedEntity = obj.Get(new EntityId(4));
            TestEntitySupport.AssertTestLoadedEntity(loadedEntity, 3, 4);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Update_ConcurrencyException()
        {
            String errorMessage = "Unable to update record. It may have been updated by someone else since it was loaded. Record Id: '1', Name: 'TestEntity', Table: '[TestEntity]', Last saved by: 'System', Last saved time: '<<dd-MMM-yyyy HH:mm:ss>>'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                obj.Save(testEntity1);

                IMockFoundationModel testEntity2 = testEntity1.Clone() as IMockFoundationModel;
                testEntity2.Name = "Modified TestEntity2";
                obj.Save(testEntity2);

                testEntity1.Name = "Modified TestEntity1";

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
        public void Test_Update_PermissionsException()
        {
            String errorMessage = @"User: '<<domain\user>>' does not have the required permissions. Required permission is: 'OwnEditor'";
            Exception actualException = null;

            try
            {
                ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

                IMockFoundationModel testEntity1 = TestEntitySupport.CreateTestEntity(CoreInstance, 1);

                obj.Save(testEntity1);

                IMockFoundationModel testEntity2 = testEntity1.Clone() as IMockFoundationModel;
                testEntity2.Name = "Modified TestEntity2";
                obj.Save(testEntity2);

                testEntity1.Name = "Modified TestEntity1";

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

        private void UpdateTestEntity(IMockFoundationModel entity, Int32 actionCount, Int32 index)
        {
            entity.Name = $"Updated {actionCount} Name: {index}";
            entity.Code = $"Updated {actionCount} Code: {index}";
            entity.Description = $"Updated {actionCount} Description: {index}";

            entity.ValidFrom = new DateTime(2018, 1, 1);
            entity.ValidTo = new DateTime(2100, 12, 31);

            entity.LastUpdatedByUserProfileId = new EntityId(2);
        }
    }
}
