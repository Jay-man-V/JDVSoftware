//-----------------------------------------------------------------------
// <copyright file="PasswordServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NSubstitute;

using NUnit.Framework;

using Foundation.Interfaces;
using Foundation.Services.Application;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for PasswordService
    /// </summary>
    [TestFixture]
    public class PasswordServiceTests : UnitTestBase
    {
        [TestCase]
        public void Test_GeneratePassword()
        {
            String expectedPassword = "Password1";
            String expectedPasswords = "[\"Password1\",\"Password2\",\"Password3\"]";
            IApplicationConfigurationProcess applicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            IRestApi restApi = Substitute.For<IRestApi>();
            IRandomService randomService = Substitute.For<IRandomService>();

            restApi.DownloadString(Arg.Any<IFileTransferSettings>()).Returns(expectedPasswords);
            randomService.NextInt32(Arg.Any<Int32>(), Arg.Any<Int32>()).Returns(1);

            IPasswordService service = new PasswordService(CoreInstance, applicationConfigurationProcess, restApi, randomService);
            String actual = service.GeneratePassword();

            Assert.That(service, Is.Not.EqualTo(null));
            Assert.That(actual, Is.Not.EqualTo(expectedPassword));
        }
        [TestCase]
        public void Test_GenerateMultiplePasswords()
        {
            String expectedPasswords = "[\"Password1\",\"Password2\",\"Password3\"]";
            IApplicationConfigurationProcess applicationConfigurationProcess = Substitute.For<IApplicationConfigurationProcess>();
            IRestApi restApi = Substitute.For<IRestApi>();
            IRandomService randomService = Substitute.For<IRandomService>();

            restApi.DownloadString(Arg.Any<IFileTransferSettings>()).Returns(expectedPasswords);

            IPasswordService service = new PasswordService(CoreInstance, applicationConfigurationProcess, restApi, randomService);
            String actual = service.GeneratePassword();

            Assert.That(service, Is.Not.EqualTo(null));
            Assert.That(actual, Is.Not.EqualTo(expectedPasswords));
        }
    }
}
