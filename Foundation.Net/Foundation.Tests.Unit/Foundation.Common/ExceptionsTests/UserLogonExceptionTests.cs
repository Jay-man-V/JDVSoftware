//-----------------------------------------------------------------------
// <copyright file="UserLogonExceptionTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExceptionsTests
{
    /// <summary>
    /// The UserLogonExceptionTests Tests
    /// </summary>
    [TestFixture]
    public class UserLogonExceptionTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor_1()
        {
            String userCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String processName = UserLogonException.ApplicationSystemLogon;

            String errorMessage = UserLogonException.CannotLocateUserCredentials;

            UserLogonException exception = new UserLogonException(RunTimeEnvironmentSettings.UserLogonName);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_Constructor_2()
        {
            String userCredentials = $@"{Environment.UserDomainName}\{Environment.UserName}";
            String processName = UserLogonException.ApplicationSystemLogon;

            String errorMessage = "Please enter user name";

            UserLogonException exception = new UserLogonException(userCredentials, errorMessage);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }
    }
}
