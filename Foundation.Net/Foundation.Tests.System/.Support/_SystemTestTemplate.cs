//-----------------------------------------------------------------------
// <copyright file="SystemTestTemplate.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

namespace Foundation.Tests.System.Support
{
    /// <summary>
    /// The system test template class
    /// </summary>
    [TestFixture]
    public class SystemTestTemplate : SystemTestBase
    {
        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestCase]
        public void TestMethod1()
        {
            ////
            //// TODO: Add test logic here
            ////
        }

        /// <summary>
        /// Starts the test.
        /// </summary>
        protected override void StartTest() { }

        /// <summary>
        /// Ends the test.
        /// </summary>
        protected override void EndTest() { }
    }
}
