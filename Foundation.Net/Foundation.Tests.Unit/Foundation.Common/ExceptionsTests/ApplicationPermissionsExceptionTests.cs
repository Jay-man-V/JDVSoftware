//-----------------------------------------------------------------------
// <copyright file="ApplicationPermissionsExceptionTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExceptionsTests
{
    /// <summary>
    /// The ApplicationPermissionsException Tests
    /// </summary>
    [TestFixture]
    public class ApplicationPermissionsExceptionTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor_ValidEntity()
        {
            String userCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String processName = "Application/System Logon";
            ApplicationRole requiredPermission = ApplicationRole.Approver;
            IFoundationModel unitTestEntity = CoreInstance.Container.Get<IMockFoundationModel>();

            String errorMessage = $"User: '{userCredentials}' does not have the required permissions. Required permission is: '{requiredPermission}'";

            ApplicationPermissionsException exception = new ApplicationPermissionsException(RunTimeEnvironmentSettings.UserLogonName, processName, requiredPermission, unitTestEntity);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.RequiredPermission, Is.EqualTo(requiredPermission.ToString()));
            Assert.That(exception.FoundationModel, Is.EqualTo(unitTestEntity));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_Constructor_NullEntity()
        {
            String userCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String processName = "Application/System Logon";
            ApplicationRole requiredPermission = ApplicationRole.Approver;
            const IFoundationModel unitTestEntity = null;

            String errorMessage = $"User: '{userCredentials}' does not have the required permissions. Required permission is: '{requiredPermission}'";

            ApplicationPermissionsException exception = new ApplicationPermissionsException(RunTimeEnvironmentSettings.UserLogonName, processName, requiredPermission, unitTestEntity);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.RequiredPermission, Is.EqualTo(requiredPermission.ToString()));
            Assert.That(exception.FoundationModel, Is.EqualTo(unitTestEntity));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_Constructor_MultiplePermissions()
        {
            String userCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String processName = "Application/System Logon";
            ApplicationRole[] requiredPermission = { ApplicationRole.Approver, ApplicationRole.Creator };
            IFoundationModel unitTestEntity = CoreInstance.Container.Get<IMockFoundationModel>();
            IUserProfile userProfile = CoreInstance.CurrentLoggedOnUser.UserProfile;

            String errorMessage = $"User: '{userCredentials}' does not have the required permissions. Required permission is: '{String.Join(", ", requiredPermission)}'";

            ApplicationPermissionsException exception = new ApplicationPermissionsException(processName, requiredPermission, unitTestEntity, userProfile);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.RequiredPermission, Is.EqualTo(String.Join(", ", requiredPermission)));
            Assert.That(exception.FoundationModel, Is.EqualTo(unitTestEntity));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }

        [TestCase]
        public void Test_Constructor_FunctionKey()
        {
            String userCredentials = RunTimeEnvironmentSettings.UserLogonName;
            String processName = LocationUtils.GetFullyQualifiedFunctionName();
            IUserProfile userProfile = CoreInstance.CurrentLoggedOnUser.UserProfile;
            ApplicationRole[] assignedRoles = {ApplicationRole.SystemAdministrator, ApplicationRole.Creator };
            String functionKey = LocationUtils.GetFunctionName();

            String errorMessage = $"User: '{userCredentials}' does not have the required permissions. Assigned Roles are: '{String.Join(", ", assignedRoles)}'. Function Key is: '{functionKey}'.";
            ApplicationPermissionsException exception = new ApplicationPermissionsException(processName, userProfile, functionKey);

            Assert.That(exception.UserCredentials, Is.EqualTo(userCredentials));
            Assert.That(exception.ProcessName, Is.EqualTo(processName));

            Assert.That(exception.RequiredPermission, Is.EqualTo(null));
            Assert.That(exception.FoundationModel, Is.EqualTo(null));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
            Assert.That(exception.GetBaseException().Message, Is.EqualTo(errorMessage));
            Assert.That(exception.InnerException, Is.EqualTo(null));
        }
    }
}
