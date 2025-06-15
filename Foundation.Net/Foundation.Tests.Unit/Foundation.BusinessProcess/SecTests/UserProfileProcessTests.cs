//-----------------------------------------------------------------------
// <copyright file="UserProfileProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.SecTests
{
    /// <summary>
    /// Summary description for UserProfileProcessTests
    /// </summary>
    [TestFixture]
    public class UserProfileProcessTests : CommonBusinessProcessTestBaseClass<IUserProfile, IUserProfileProcess, IUserProfileRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 11;
        protected override String ExpectedScreenTitle => "User Profiles";
        protected override String ExpectedStatusBarText => "Number of User Profiles:";

        protected override String ExpectedComboBoxDisplayMember => FDC.UserProfile.DisplayName;

        protected override IUserProfileRepository CreateDataAccess()
        {
            IUserProfileRepository dataAccess = Substitute.For<IUserProfileRepository>();

            return dataAccess;
        }

        protected override IUserProfileProcess CreateBusinessProcess()
        {
            IUserProfileProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IUserProfileProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IUserProfileProcess process = new UserProfileProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess);

            return process;
        }

        protected override IUserProfile CreateBlankEntity(IUserProfileProcess process)
        {
            IUserProfile retVal = CoreInstance.Container.Get<IUserProfile>();

            return retVal;
        }

        protected override IUserProfile CreateEntity(IUserProfileProcess process)
        {
            IUserProfile retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.DisplayName = Guid.NewGuid().ToString();
            retVal.Username = Guid.NewGuid().ToString();
            retVal.ExternalKeyId = Guid.NewGuid().ToString();
            retVal.ContactDetailId = new EntityId(1);

            return retVal;
        }

        protected override void CheckBlankEntry(IUserProfile entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(null));
            Assert.That(entity.Username, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IUserProfile entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IUserProfile entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IUserProfile entity1, IUserProfile entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.DisplayName, Is.EqualTo(entity1.DisplayName));
            Assert.That(entity2.Username, Is.EqualTo(entity1.Username));
        }

        protected override void UpdateEntityProperties(IUserProfile entity)
        {
            entity.DisplayName += "Updated";
            entity.Username += "Updated";
        }

        [TestCase]
        public void Test_GetLoggedOnUserProfile()
        {
            IUserProfileProcess process = CreateBusinessProcess();
            AppId appId = new AppId(1);
            EntityId userProfileId = new EntityId(1);

            IUserProfile expectedUserProfile = CoreInstance.Container.Get<IUserProfile>();
            expectedUserProfile.Id = new EntityId(1);
            expectedUserProfile.DisplayName = "Unit Testing user name";
            expectedUserProfile.ExternalKeyId = Guid.NewGuid().ToString();
            expectedUserProfile.IsSystemSupport = false;

            DataAccess.Get(Arg.Any<AppId>(), Arg.Any<String>()).Returns(expectedUserProfile);

            IUserProfile actualUserProfile = process.GetLoggedOnUserProfile(appId);

            Assert.That(actualUserProfile.Id, Is.EqualTo(expectedUserProfile.Id));
            Assert.That(actualUserProfile.DisplayName, Is.EqualTo(expectedUserProfile.DisplayName));
            Assert.That(actualUserProfile.ExternalKeyId, Is.EqualTo(expectedUserProfile.ExternalKeyId));
            Assert.That(actualUserProfile.IsSystemSupport, Is.EqualTo(expectedUserProfile.IsSystemSupport));
        }

        [TestCase]
        public void Test_GetUserProfile_UserProfileId()
        {
            IUserProfileProcess process = CreateBusinessProcess();
            AppId appId = new AppId(1);
            EntityId userProfileId = new EntityId(1);

            IUserProfile expectedUserProfile = CoreInstance.Container.Get<IUserProfile>();
            expectedUserProfile.Id = new EntityId(1);
            expectedUserProfile.DisplayName = "Unit Testing user name";
            expectedUserProfile.ExternalKeyId = Guid.NewGuid().ToString();
            expectedUserProfile.IsSystemSupport = false;

            DataAccess.Get(Arg.Any<AppId>(), Arg.Any<EntityId>()).Returns(expectedUserProfile);

            IUserProfile actualUserProfile = process.GetUserProfile(appId, userProfileId);

            Assert.That(actualUserProfile.Id, Is.EqualTo(expectedUserProfile.Id));
            Assert.That(actualUserProfile.DisplayName, Is.EqualTo(expectedUserProfile.DisplayName));
            Assert.That(actualUserProfile.ExternalKeyId, Is.EqualTo(expectedUserProfile.ExternalKeyId));
            Assert.That(actualUserProfile.IsSystemSupport, Is.EqualTo(expectedUserProfile.IsSystemSupport));
        }

        [TestCase]
        public void Test_GetUserProfile_Username()
        {
            IUserProfileProcess process = CreateBusinessProcess();
            AppId appId = new AppId(1);
            String username = $@"{UserSecuritySupport.UnitTestAccountDomain}\{UserSecuritySupport.UnitTestAccountUserName}";

            IUserProfile expectedUserProfile = CoreInstance.Container.Get<IUserProfile>();
            expectedUserProfile.Id = new EntityId(1);
            expectedUserProfile.DisplayName = "Unit Testing user name";
            expectedUserProfile.ExternalKeyId = Guid.NewGuid().ToString();
            expectedUserProfile.IsSystemSupport = false;

            DataAccess.Get(Arg.Any<AppId>(), Arg.Any<String>()).Returns(expectedUserProfile);

            IUserProfile actualUserProfile = process.GetUserProfile(appId, username);

            Assert.That(actualUserProfile.Id, Is.EqualTo(expectedUserProfile.Id));
            Assert.That(actualUserProfile.DisplayName, Is.EqualTo(expectedUserProfile.DisplayName));
            Assert.That(actualUserProfile.ExternalKeyId, Is.EqualTo(expectedUserProfile.ExternalKeyId));
            Assert.That(actualUserProfile.IsSystemSupport, Is.EqualTo(expectedUserProfile.IsSystemSupport));
        }

        [TestCase]
        public void Test_SyncActiveDirectoryUserDataFromStaging()
        {
            IUserProfileProcess process = CreateBusinessProcess();

            DataAccess.SyncActiveDirectoryUserDataFromStaging(Arg.Any<IUserProfile>());

            process.SyncActiveDirectoryUserDataFromStaging();
        }
    }
}
