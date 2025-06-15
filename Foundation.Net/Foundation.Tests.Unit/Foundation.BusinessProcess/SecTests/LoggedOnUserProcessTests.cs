//-----------------------------------------------------------------------
// <copyright file="LoggedOnUserProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.SecTests
{
    /// <summary>
    /// Summary description for LoggedOnUserProcessTests
    /// </summary>
    [TestFixture]
    public class LoggedOnUserProcessTests : CommonBusinessProcessTestBaseClass<ILoggedOnUser, ILoggedOnUserProcess, ILoggedOnUserRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 9;
        protected override String ExpectedScreenTitle => "Logged On Users";
        protected override String ExpectedStatusBarText => "Number of Logged On Users:";

        protected override string ExpectedComboBoxDisplayMember => FDC.LoggedOnUser.DisplayName;

        protected override ILoggedOnUserRepository CreateDataAccess()
        {
            ILoggedOnUserRepository dataAccess = Substitute.For<ILoggedOnUserRepository>();

            return dataAccess;
        }

        protected override ILoggedOnUserProcess CreateBusinessProcess()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override ILoggedOnUserProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IRoleProcess roleProcess = Substitute.For<IRoleProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            ILoggedOnUserProcess process = new LoggedOnUserProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess, applicationProcess, roleProcess, userProfileProcess);

            return process;
        }

        protected override ILoggedOnUser CreateBlankEntity(ILoggedOnUserProcess process)
        {
            ILoggedOnUser retVal = CoreInstance.Container.Get<ILoggedOnUser>();

            return retVal;
        }

        protected override ILoggedOnUser CreateEntity(ILoggedOnUserProcess process)
        {
            ILoggedOnUser retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ApplicationId = new AppId(1);
            retVal.UserProfileId = new EntityId(1);
            retVal.LoggedOn = DateTimeService.SystemDateTimeNow;
            retVal.LastActive = DateTimeService.SystemDateTimeNow;
            retVal.Command = Guid.NewGuid().ToString();
            
            return retVal;
        }

        protected override void CheckBlankEntry(ILoggedOnUser entity)
        {
            Assert.That(entity.ApplicationId, Is.EqualTo(new AppId(0)));
            Assert.That(entity.LoggedOn, Is.EqualTo(DateTime.MinValue));
            Assert.That(entity.LastActive, Is.EqualTo(DateTime.MinValue));
            Assert.That(entity.Command, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(ILoggedOnUser entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(ILoggedOnUser entity)
        {
            Assert.That(entity.DisplayName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(ILoggedOnUser entity1, ILoggedOnUser entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ApplicationId, Is.EqualTo(entity1.ApplicationId));
            Assert.That(entity2.LoggedOn, Is.EqualTo(entity1.LoggedOn));
            Assert.That(entity2.LastActive, Is.EqualTo(entity1.LastActive));
            Assert.That(entity2.Command, Is.EqualTo(entity1.Command));
        }

        protected override void UpdateEntityProperties(ILoggedOnUser entity)
        {
            entity.LastActive = DateTimeService.SystemDateTimeNow;
            entity.Command += " Updated";
        }

        [TestCase]
        public void Test_GetColumnDefinitionsForDisplayControl()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();

            List<IGridColumnDefinition> gridColumnDefinitions = process.GetColumnDefinitionsForDisplayControl();

            Assert.That(gridColumnDefinitions.Count, Is.EqualTo(7));
        }

        [TestCase]
        public void Test_SendQuitCommand()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);

            ILoggedOnUser loggedOnUser = CreateBlankEntity(process);

            process.SendQuitCommand(applicationId, loggedOnUser);
        }

        [TestCase]
        public void Test_SendAbortCommand()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);

            ILoggedOnUser loggedOnUser = CreateBlankEntity(process);

            process.SendAbortCommand(applicationId, loggedOnUser);
        }

        [TestCase]
        public void Test_SendMessageCommand()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);
            String message = "Testing message";

            ILoggedOnUser loggedOnUser = CreateBlankEntity(process);

            process.SendMessageCommand(applicationId, loggedOnUser, message);
        }

        [TestCase]
        public void Test_ClearCommand()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);

            ILoggedOnUser loggedOnUser = CreateBlankEntity(process);

            process.ClearCommand(applicationId, loggedOnUser);
        }

        [TestCase]
        public void Test_LogOnUser()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);

            process.LogOnUser(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile);
        }

        [TestCase]
        public void Test_UpdateLoggedOnUser()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();
            AppId applicationId = new AppId(1);

            process.UpdateLoggedOnUser(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile);
        }

        [TestCase]
        public void Test_GetLoggedOnUsers()
        {
            ILoggedOnUserProcess process = CreateBusinessProcess();

            AppId applicationId = new AppId(1);

            List<ILoggedOnUser> expectedLoggedOnUsers = new List<ILoggedOnUser>
            {
                CoreInstance.Container.Get<ILoggedOnUser>(),
                CoreInstance.Container.Get<ILoggedOnUser>(),
                CoreInstance.Container.Get<ILoggedOnUser>(),
                CoreInstance.Container.Get<ILoggedOnUser>(),
                CoreInstance.Container.Get<ILoggedOnUser>(),
            };

            DataAccess.GetLoggedOnUsers(Arg.Any<AppId>()).Returns(expectedLoggedOnUsers);

            IEnumerable<ILoggedOnUser> actualLoggedOnUsers = process.GetLoggedOnUsers(applicationId);

            Assert.That(actualLoggedOnUsers.Count(), Is.EqualTo(expectedLoggedOnUsers.Count));
        }
    }
}
