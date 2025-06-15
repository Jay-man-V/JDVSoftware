//-----------------------------------------------------------------------
// <copyright file="ObjectCreation2Tests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NSubstitute;

using NUnit.Framework;

using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Object Creation Tests
    /// </summary>
    /// <see cref="UnitTestBase" />
    [TestFixture]
    public class ObjectCreation2Tests : UnitTestBase
    {
        /// <summary>
        /// Tests the object creation1.
        /// </summary>
        [TestCase]
        public void Test_InvalidProvideName()
        {
            String connectionStringConfiguration = "UnitTestingUnknownProvider";
            String errorMessage = $"The Data Provider '{connectionStringConfiguration}' is unknown and not supported";
            Exception actualException = null;

            try
            {
                IUnitTestingDatabaseProvider databaseProvider = Substitute.For<IUnitTestingDatabaseProvider>();
                databaseProvider.ConnectionName.Returns(connectionStringConfiguration);

                _ = new ComplexTestEntityRepository(CoreInstance, RunTimeEnvironmentSettings, databaseProvider, DateTimeService);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<NotSupportedException>());
            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
        }
    }
}
