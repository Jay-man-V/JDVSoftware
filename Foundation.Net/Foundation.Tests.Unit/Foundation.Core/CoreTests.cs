//-----------------------------------------------------------------------
// <copyright file="CoreTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Core
{
    /// <summary>
    /// Summary description for CoreTests
    /// </summary>
    [TestFixture]
    public class CoreTests : UnitTestBase
    {
        [TestCase]
        public void Test_Initialise()
        {
            /*
             *
             *
             * This test focuses on making the setup works for Unit Tests
             *
             *
             */

            Assert.That(CoreInstance.ApplicationId, Is.EqualTo(ApplicationSettings.ApplicationId));
            Assert.That(CoreInstance.Initialised, Is.EqualTo(true));
            Assert.That(CoreInstance.Container, Is.Not.EqualTo(null));
            Assert.That(CoreInstance.TheInstance, Is.Not.EqualTo(null));
            Assert.That(CoreInstance.CurrentLoggedOnUser, Is.Not.EqualTo(null));
            Assert.That(CoreInstance.CurrentLoggedOnUser.UserProfile.Id, Is.EqualTo(new EntityId(1)));
            Assert.That(CoreInstance.CurrentLoggedOnUser.UserProfile.DisplayName, Is.EqualTo(UserSecuritySupport.UnitTestAccountDisplayName));
            Assert.That(CoreInstance.CurrentLoggedOnUser.UserProfile.Username, Is.EqualTo($@"{UserSecuritySupport.UnitTestAccountDomain}\{UserSecuritySupport.UnitTestAccountUserName}"));
        }

        [TestCase]
        public void Test_Initialise_Exception()
        {
            String expectedUserCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String expectedProcessName = UserLogonException.ApplicationSystemLogon;
            String errorMessage = UserLogonException.CannotLocateUserCredentials;

            Exception actualException = null;
            try
            {
                IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();
                const IUserProfile userProfile = null;
                userProfileProcess.GetLoggedOnUserProfile(Arg.Any<AppId>()).Returns(userProfile);

                global::Foundation.Core.Core.Reset();
                global::Foundation.Core.Core.Initialise(ApplicationSettings.ApplicationId, RunTimeEnvironmentSettings, userProfileProcess);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            UserLogonException ulException = actualException as UserLogonException;
            Assert.That(ulException, Is.Not.EqualTo(null));
            Assert.That(ulException.Message, Is.EqualTo(errorMessage));
            Assert.That(ulException.UserCredentials, Is.EqualTo(expectedUserCredentials));
            Assert.That(ulException.ProcessName, Is.EqualTo(expectedProcessName));
        }
    }
}
