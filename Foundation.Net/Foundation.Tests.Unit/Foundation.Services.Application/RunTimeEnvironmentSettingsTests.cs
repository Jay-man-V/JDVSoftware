//-----------------------------------------------------------------------
// <copyright file="RunTimeEnvironmentSettingsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for RunTimeEnvironmentSettings
    /// </summary>
    [TestFixture]
    public class RunTimeEnvironmentSettingsTests : UnitTestBase
    {
        [TestCase]
        public void Test_Properties()
        {
            IRunTimeEnvironmentSettings service = CoreInstance.Container.Get<IRunTimeEnvironmentSettings>();
            Assert.That(service, Is.Not.EqualTo(null));

            String standardCountryCode = service.StandardCountryCode;
            Assert.That(standardCountryCode, Is.Not.EqualTo(null));
            Assert.That(standardCountryCode, Is.EqualTo("GB"));

            String userName = service.UserName;
            Assert.That(userName, Is.Not.EqualTo(null));
            Assert.That(userName, Is.EqualTo(Environment.UserName));

            String userDomainName = service.UserDomainName;
            Assert.That(userDomainName, Is.Not.EqualTo(null));
            Assert.That(userDomainName, Is.EqualTo(Environment.UserDomainName));

            String userLogonName = service.UserLogonName;
            Assert.That(userLogonName, Is.Not.EqualTo(null));
            Assert.That(userLogonName, Is.EqualTo($@"{Environment.UserDomainName}\{Environment.UserName}"));

            String machineName = service.MachineName;
            Assert.That(machineName, Is.Not.EqualTo(null));
            Assert.That(machineName, Is.EqualTo(Environment.MachineName));
        }
    }
}
