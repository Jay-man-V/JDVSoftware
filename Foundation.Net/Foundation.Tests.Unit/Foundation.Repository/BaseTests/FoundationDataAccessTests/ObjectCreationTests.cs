//-----------------------------------------------------------------------
// <copyright file="ObjectCreationTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.DataAccess.Database;
using Foundation.Interfaces;
using Foundation.Repository;
using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using NSubstitute;

using NUnit.Framework;

using System;

namespace Foundation.Tests.Unit.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Object Creation Tests
    /// </summary>
    /// <see cref="UnitTestBase" />
    [TestFixture]
    public class ObjectCreationTests : UnitTestBase
    {
        private SimpleTestEntityRepository CreateProcess()
        {
            IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
            databaseProvider.ConnectionName.Returns("UnitTesting");

            SimpleTestEntityRepository retVal = new SimpleTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);

            return retVal;
        }

        /// <summary>
        /// Tests the object creation1.
        /// </summary>
        [TestCase]
        public void Test_ObjectCreation1()
        {
            IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
            databaseProvider.ConnectionName.Returns("UnitTesting");

            ComplexTestEntityRepository obj = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);

            Assert.That(obj, Is.Not.EqualTo(null));
            Assert.That(obj, Is.InstanceOf<FoundationModelRepository<IMockFoundationModel>>());
            Assert.That(obj, Is.InstanceOf<IFoundationModelRepository<IMockFoundationModel>>());
            Assert.That(obj, Is.InstanceOf<IDisposable>());
        }

        [TestCase]
        public void Test_RefreshCacheData()
        {
            SimpleTestEntityRepository obj = CreateProcess();

            obj.RefreshCache();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_EntityKey_Exception()
        {
            String errorMessage = "The method or operation is not implemented.";
            Exception actualException = null;

            try
            {
                SimpleTestEntityRepository obj = CreateProcess();
                _ = obj.GetEntityKey();
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotImplementedException>());

            String actualErrorMessage = actualException.Message;

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }
    }
}
