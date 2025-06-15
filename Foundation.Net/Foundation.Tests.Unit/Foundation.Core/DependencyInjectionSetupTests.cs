//-----------------------------------------------------------------------
// <copyright file="DependencyInjectionSetupTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Core
{
    /// <summary>
    /// Summary description for DependencyInjectionSetupTests
    /// </summary>
    [TestFixture]
    public class DependencyInjectionSetupTests : UnitTestBase
    {
        [TestCase]
        public void Test_GetSingleInstanceOfType()
        {
            IHeartbeatResult heartbeatResult = global::Foundation.Core.Core.Instance.Container.Get<IHeartbeatResult>();
            Assert.That(heartbeatResult, Is.Not.EqualTo(null));

            // Must be one instance - can be multiple if the Dependency Injection setup is incorrect
            List<IHeartbeatResult> allInstances = global::Foundation.Core.Core.Instance.Container.GetAll<IHeartbeatResult>().ToList();
            Assert.That(allInstances.Count, Is.EqualTo(1));
        }

        [TestCase]
        public void Test_GetAllInstanceOfType()
        {
            List<IMultipleInstances> multipleInstances = global::Foundation.Core.Core.Instance.Container.GetAll<IMultipleInstances>().ToList();
            Assert.That(multipleInstances, Is.Not.EqualTo(null));
            Assert.That(multipleInstances.Count, Is.EqualTo(2));
        }

        [TestCase]
        public void Test_LoopAllInterfaces()
        {
            List<IMultipleInstances> multipleInstances = global::Foundation.Core.Core.Instance.Container.GetAll<IMultipleInstances>().ToList();
            Assert.That(multipleInstances, Is.Not.EqualTo(null));
            Assert.That(multipleInstances.Count, Is.EqualTo(2));
        }
    }
}
