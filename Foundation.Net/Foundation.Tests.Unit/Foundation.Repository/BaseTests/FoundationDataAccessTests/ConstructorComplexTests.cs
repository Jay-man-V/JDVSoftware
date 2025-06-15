//-----------------------------------------------------------------------
// <copyright file="ConstructorComplexTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NSubstitute;

using NUnit.Framework;

using Foundation.DataAccess.Database;
using Foundation.Interfaces;
using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Get Tests
    /// </summary>
    /// <see cref="UnitTestBase" />
    [TestFixture]
    public class ConstructorComplexTests : UnitTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Constructor_Exception()
        {
            String parameterName = "dataConnectionName";
            String connectionStringKey = "Made up connection string key";
            String errorMessage = $"Cannot load Connection named '{connectionStringKey}'. Check to make sure the connection is defined in the Configuration File.{Environment.NewLine}Parameter name: {parameterName}";
            Exception actualException = null;

            try
            {
                IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
                databaseProvider.ConnectionName.Returns(connectionStringKey);

                _ = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            if (actualException is ArgumentNullException ane)
            {
                String actualErrorMessage = actualException.Message;
                Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
                Assert.That(ane.ParamName, Is.EqualTo(parameterName));
            }
            else
            {
                Assert.Fail($"Unexpected exception: {actualException}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Constructor_MsSqlDataLogicProvider()
        {
            String connectionStringKey = "MsSQLDataLogicProviderTest";
            String dataProviderName = "System.Data.SqlClient";

            IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
            databaseProvider.ConnectionName.Returns(connectionStringKey);

            ComplexTestEntityRepository obj = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);
            String actualDataProviderName = obj.GetDataProviderName();
            IDataLogicProvider actualDataLogicProvider = obj.GetDataLogicProvider();

            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
            Assert.That(actualDataLogicProvider, Is.InstanceOf<MsSqlDataLogicProvider>());
            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Constructor_MySqlDataLogicProvider()
        {
            String connectionStringKey = "MySQLDataLogicProviderTest";
            String dataProviderName = "MySql.Data.MySqlClient";

            IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
            databaseProvider.ConnectionName.Returns(connectionStringKey);

            ComplexTestEntityRepository obj = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);
            String actualDataProviderName = obj.GetDataProviderName();
            IDataLogicProvider actualDataLogicProvider = obj.GetDataLogicProvider();

            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
            Assert.That(actualDataLogicProvider, Is.InstanceOf<MySqlDataLogicProvider>());
            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_Constructor_OracleSqlDataLogicProvider()
        {
            String connectionStringKey = "OracleDataLogicProviderTest";
            String dataProviderName = "System.Data.OracleClient";

            IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
            databaseProvider.ConnectionName.Returns(connectionStringKey);

            ComplexTestEntityRepository obj = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);
            String actualDataProviderName = obj.GetDataProviderName();
            IDataLogicProvider actualDataLogicProvider = obj.GetDataLogicProvider();

            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
            Assert.That(actualDataLogicProvider, Is.InstanceOf<OracleDataLogicProvider>());
            Assert.That(actualDataProviderName, Is.EqualTo(dataProviderName));
        }
    }
}
