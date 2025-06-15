//-----------------------------------------------------------------------
// <copyright file="ApplicationSupportTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ApplicationSupportTests
{
    /// <summary>
    /// ApplicationSupport Tests
    /// </summary>
    [TestFixture]
    public class ApplicationSupportTests : UnitTestBase
    {
        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_ApplicationStart()
        {
            MockApplication _ = new MockApplication();
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_LogUnhandledExceptionMessage()
        {
            try
            {
                throw new Exception(LocationUtils.GetFunctionName());
            }
            catch (Exception exception)
            {
                ApplicationControl.LogUnhandledExceptionMessage(exception);
            }
        }
    }
}
