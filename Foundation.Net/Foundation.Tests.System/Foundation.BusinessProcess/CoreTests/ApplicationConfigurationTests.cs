//-----------------------------------------------------------------------
// <copyright file="ApplicationConfigurationTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.System.Support;

namespace Foundation.Tests.System.Foundation.BusinessProcess.CoreTests
{
    /// <summary>
    /// Summary description for ApplicationConfigurationTests
    /// </summary>
    [TestFixture]
    public class ApplicationConfigurationTests : SystemTestBase
    {
        protected IApplicationConfigurationProcess TheProcess { get; set; }

        protected override void StartTest()
        {
            ProcessName = LocationUtils.GetClassName();
            TaskName = LocationUtils.GetFunctionName();

            base.StartTest();

            TheProcess = Core.Core.Instance.Container.Get<IApplicationConfigurationProcess>();
        }

        [TestCase]
        public void Test_GetAll()
        {
            List<IApplicationConfiguration> groupValues = TheProcess.GetAll();

            Assert.That(groupValues, Is.Not.Null);
        }

        [TestCase]
        public void Test_GetValue()
        {
            String expected = "*@datalore.me.uk";
            String key = "email.smtp.host.username";

            String actualValue = TheProcess.Get<String>(CoreSystemApplicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expected));
        }

        [TestCase]
        public void Test_GetGroupValues()
        {
            List<IApplicationConfiguration> groupValues = TheProcess.GetGroupValues(CoreSystemApplicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, "email");

            Assert.That(6, Is.EqualTo(groupValues.Count));
        }
    }
}
