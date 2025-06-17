//-----------------------------------------------------------------------
// <copyright file="CurrentLoggedOnUserTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Core;
using Foundation.Interfaces;
using Foundation.Models;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Core
{
    /// <summary>
    /// Summary description for CurrentLoggedOnUserTests
    /// </summary>
    [TestFixture]
    public class CurrentLoggedOnUserTests : UnitTestBase
    {
        [TestCase]
        public void Test_Constructor()
        {
            IUserProfile userProfile = new UserProfile();
            userProfile.Id = new EntityId(123456);
            userProfile.Username = Guid.NewGuid().ToString();
            userProfile.DisplayName = Guid.NewGuid().ToString();
            userProfile.IsSystemSupport = true;

            ICurrentLoggedOnUser currentLoggedOnUser = new CurrentLoggedOnUser(userProfile);

            Assert.That(currentLoggedOnUser.UserProfile, Is.EqualTo(userProfile));
            Assert.That(currentLoggedOnUser.Id, Is.EqualTo(userProfile.Id));
            Assert.That(currentLoggedOnUser.Username, Is.EqualTo(userProfile.Username));
            Assert.That(currentLoggedOnUser.DisplayName, Is.EqualTo(userProfile.DisplayName));
            Assert.That(currentLoggedOnUser.IsSystemSupport, Is.EqualTo(userProfile.IsSystemSupport));
        }

        [TestCase]
        public void Test_SetLoggedOnUser()
        {
            IUserProfile userProfile1 = new UserProfile { Id = new EntityId(123) };
            IUserProfile userProfile2 = new UserProfile { Id = new EntityId(456) };
            Assert.That(userProfile1, Is.Not.EqualTo(userProfile2));

            ICurrentLoggedOnUser currentLoggedOnUser = new CurrentLoggedOnUser(userProfile1);
            Assert.That(currentLoggedOnUser.UserProfile, Is.EqualTo(userProfile1));

            currentLoggedOnUser.SetLoggedOnUser(userProfile2);
            Assert.That(currentLoggedOnUser.UserProfile, Is.Not.EqualTo(userProfile1));
            Assert.That(currentLoggedOnUser.UserProfile, Is.EqualTo(userProfile2));
        }
    }
}
