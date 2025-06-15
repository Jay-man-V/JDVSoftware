using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Foundation.Core;

namespace DemoUnitTests
{
    [TestClass]
    public class MsTestsDemo
    {
        [TestMethod]
        public void Test_ContinuousTesting()
        {
            Boolean expectedSuccess = false;
            String expectedVersion = "1.2.3";
            DateTime expectedRunDate = new DateTime(2025, 1, 9, 19, 46, 25, 123);
            HeartbeatResult heartbeatResult = new HeartbeatResult
            {
                DateRun = expectedRunDate,
                Success = expectedSuccess,
                Version = expectedVersion
            };

            String expectedToString = $"{expectedRunDate:yyyy-MM-ddTHH:mm:ss.fff}. {expectedVersion}. {expectedSuccess}. {String.Join(", ", heartbeatResult.Logs)}";

            Assert.AreEqual(expectedRunDate, heartbeatResult.DateRun);
            Assert.AreEqual(expectedToString, heartbeatResult.ToString());
        }
    }
}
