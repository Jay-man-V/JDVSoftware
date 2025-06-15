//-----------------------------------------------------------------------
// <copyright file="GetTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Foundation.Tests.System.Support;
using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using Foundation.Interfaces;

namespace Foundation.Tests.System.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Get Tests
    /// </summary>
    /// <see cref="DataAccessSystemTestBase" />
    [TestFixture]
    [DeploymentItem(@".Support\Sql\Foundation.Repository\GetTests - CreateTestData.sql", @".Support\Sql\Foundation.Repository\")]
    public class GetTests : DataAccessSystemTestBase
    {
        protected override void StartTest()
        {
            base.StartTest();

            // Create the Test Data
            String createTestDataScript = @".Support\Sql\Foundation.Repository\GetTests - CreateTestData.sql";
            ExecuteSqlFile(createTestDataScript);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetAllActive_Simple()
        {
            SimpleTestEntityRepository obj = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IList<IMockFoundationModel> allActive = obj.GetAllActive();
            Assert.That(allActive, Is.Not.EqualTo(null));
            Assert.That(allActive.ToList().Count, Is.EqualTo(5));

            IMockFoundationModel testEntity = allActive[0];

            TestEntitySupport.AssertTestEntity_2(testEntity, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetAllActive()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IList<IMockFoundationModel> allActive = obj.GetAllActive();
            Assert.That(allActive, Is.Not.EqualTo(null));
            Assert.That(allActive.ToList().Count, Is.EqualTo(5));

            IMockFoundationModel testEntity = allActive[0];

            TestEntitySupport.AssertTestEntity_2(testEntity, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetById_1()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity = obj.Get(new EntityId(1));

            TestEntitySupport.AssertTestEntity_1(testEntity, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetById_2()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity = obj.Get(new EntityId(2));

            TestEntitySupport.AssertTestEntity_2(testEntity, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetNonExistent()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity = obj.Get(new EntityId(2000));

            Assert.That(testEntity, Is.EqualTo(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Get_ListOfEntityIds()
        {
            List<EntityId> entityIds = new List<EntityId>
            {
                new EntityId(1),
                new EntityId(2),
                new EntityId(3),
            };

            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IEnumerable<IMockFoundationModel> testEntities = obj.Get(entityIds);

            Assert.That(testEntities, Is.Not.EqualTo(null));
            IMockFoundationModel testEntity = testEntities.ToList()[1];

            TestEntitySupport.AssertTestEntity_2(testEntity, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Get_EntityKey()
        {
            ComplexTestEntityRepository obj = BaseTestsHelpers.CreateComplexProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService);

            IMockFoundationModel testEntity = obj.Get("Code: 2");

            Assert.That(testEntity, Is.Not.EqualTo(null));

            TestEntitySupport.AssertTestEntity_2(testEntity, 2);
        }
    }
}
