//-----------------------------------------------------------------------
// <copyright file="ActiveDirectoryUserProcessTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.StgTests
{
    /// <summary>
    /// Summary FullName for ActiveDirectoryUserProcessTests
    /// </summary>
    [TestFixture]
    public class ActiveDirectoryUserProcessTests : CommonBusinessProcessTestBaseClass<IActiveDirectoryUser, IActiveDirectoryUserProcess, IActiveDirectoryUserRepository>
    {
        protected override Int32 GetColumnDefinitionsCount => 3;
        protected override String ExpectedScreenTitle => "Active Directory Users";
        protected override String ExpectedStatusBarText => "Number of Active Directory Users:";

        protected override Boolean ExpectedHasOptionalAction1 => true;
        protected override String ExpectedAction1Name => "Save to Staging";
        protected override Boolean ExpectedHasOptionalAction2 => true;
        protected override String ExpectedAction2Name => "Sync User Profiles";
        protected override String ExpectedComboBoxDisplayMember => FDC.ActiveDirectoryUser.FullName;

        protected override IActiveDirectoryUserRepository CreateDataAccess()
        {
            IActiveDirectoryUserRepository dataAccess = Substitute.For<IActiveDirectoryUserRepository>();

            return dataAccess;
        }

        protected override IActiveDirectoryUserProcess CreateBusinessProcess()
        {
            IActiveDirectoryUserProcess process = CreateBusinessProcess(DateTimeService);

            return process;
        }

        protected override IActiveDirectoryUserProcess CreateBusinessProcess(IDateTimeService dateTimeService)
        {
            IActiveDirectoryUserProcess process = new ActiveDirectoryUserProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, DataAccess, StatusDataAccess, UserProfileDataAccess);

            return process;
        }

        protected override IActiveDirectoryUser CreateBlankEntity(IActiveDirectoryUserProcess process)
        {
            IActiveDirectoryUser retVal = CoreInstance.Container.Get<IActiveDirectoryUser>();

            return retVal;
        }

        protected override IActiveDirectoryUser CreateEntity(IActiveDirectoryUserProcess process)
        {
            IActiveDirectoryUser retVal = CreateBlankEntity(process);

            retVal.CreatedOn = process.DefaultValidFromDateTime;

            retVal.ValidFrom = process.DefaultValidFromDateTime;
            retVal.ValidTo = process.DefaultValidToDateTime;

            retVal.ObjectSId = Guid.NewGuid().ToString();
            retVal.Name = Guid.NewGuid().ToString();
            retVal.FullName = Guid.NewGuid().ToString();

            return retVal;
        }

        protected override void CheckBlankEntry(IActiveDirectoryUser entity)
        {
            Assert.That(entity.FullName, Is.EqualTo(null));
        }

        protected override void CheckAllEntry(IActiveDirectoryUser entity)
        {
            Assert.That(entity.FullName, Is.EqualTo(ExpectedAllText));
        }

        protected override void CheckNoneEntry(IActiveDirectoryUser entity)
        {
            Assert.That(entity.FullName, Is.EqualTo(ExpectedNoneText));
        }

        protected override void CompareEntityProperties(IActiveDirectoryUser entity1, IActiveDirectoryUser entity2)
        {
            Assert.That(entity2.ValidFrom, Is.EqualTo(entity1.ValidFrom));
            Assert.That(entity2.ValidTo, Is.EqualTo(entity1.ValidTo));

            Assert.That(entity2.ObjectSId, Is.EqualTo(entity1.ObjectSId));
            Assert.That(entity2.Name, Is.EqualTo(entity1.Name));
            Assert.That(entity2.FullName, Is.EqualTo(entity1.FullName));
        }

        protected override void UpdateEntityProperties(IActiveDirectoryUser entity)
        {
            throw new NotImplementedException("This method should not be called during the test");
        }

        [TestCase]
        public override void Test_Update_Entity()
        {
            // Does nothing
            // This Test is not valid for ActiveDirectoryUser
        }
    }
}
