//-----------------------------------------------------------------------
// <copyright file="GetConnectionTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Foundation.Tests.System.Support;
using Foundation.Tests.Unit.Foundation.Repository.Support;

namespace Foundation.Tests.System.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    /// <summary>
    /// Get Tests
    /// </summary>
    /// <see cref="DataAccessSystemTestBase" />
    [TestFixture]
    public class GetConnectionTests : DataAccessSystemTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_GetConnection_Once()
        {
            using (SimpleTestEntityRepository dataAccess = BaseTestsHelpers.CreateSimpleProcess(CoreInstance, RunTimeEnvironmentSettings, DateTimeService))
            {
                dataAccess.GetConnectionTwice();
            }
        }
    }
}
