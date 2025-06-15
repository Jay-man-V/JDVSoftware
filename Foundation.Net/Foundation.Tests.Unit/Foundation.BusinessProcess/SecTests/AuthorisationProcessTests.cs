//-----------------------------------------------------------------------
// <copyright file="AuthorisationProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.SecTests
{
    /// <summary>
    /// Summary description for AuthorisationProcessTests
    /// </summary>
    [TestFixture]
    public class AuthorisationProcessTests : UnitTestBase
    {
        private AppId AppId { get; } = new AppId(1);
        private Guid TokenReference { get; } = Guid.NewGuid();
        private IPermissionMatrixProcess PermissionMatrixProcess { get; set; }

        private IAuthorisationProcess CreateBusinessProcess()
        {
            PermissionMatrixProcess = Substitute.For<IPermissionMatrixProcess>();
            IAuthenticationProcess authenticationProcess = Substitute.For<IAuthenticationProcess>();

            IAuthorisationProcess process = new AuthorisationProcess(CoreInstance, PermissionMatrixProcess, authenticationProcess);

            return process;
        }

        private AuthenticationToken GetAuthenticationToken()
        {
            IUserProfile userProfile = CoreInstance.CurrentLoggedOnUser.UserProfile;

            AuthenticationToken authenticationToken = new AuthenticationToken(new EntityId(1), AppId, userProfile.Id, CreatedOnDateTime, TokenReference.ToString(), LastUpdatedOnDateTime);

            return authenticationToken;
        }

        [TestCase]
        public void Test_IsUserAuthorised_Success()
        {
            AuthenticationToken authenticationToken = GetAuthenticationToken();

            IAuthorisationProcess process = CreateBusinessProcess();

            PermissionMatrixProcess.CanUserPerformFunction(ref authenticationToken, Arg.Any<String>()).Returns(true);

            process.IsUserAuthorised(ref authenticationToken, LocationUtils.GetFunctionName());
        }

        [TestCase]
        public void Test_IsUserAuthorised_Fail()
        {
            AuthenticationToken authenticationToken = GetAuthenticationToken();

            IAuthorisationProcess process = CreateBusinessProcess();

            Exception actualException = null;

            try
            {
                process.IsUserAuthorised(ref authenticationToken, LocationUtils.GetFunctionName());
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
        }
    }
}
