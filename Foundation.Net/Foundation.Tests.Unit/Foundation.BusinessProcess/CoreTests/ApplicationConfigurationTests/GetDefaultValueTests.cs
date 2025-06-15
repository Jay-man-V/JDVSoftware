//-----------------------------------------------------------------------
// <copyright file="GetDefaultValueTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.ApplicationConfigurationTests
{
    /// <summary>
    /// Summary description for ApplicationConfigurationProcessTests
    /// </summary>
    [TestFixture]
    public class GetDefaultValueTests : UnitTestBase
    {
        private IApplicationConfigurationRepository EntityDataAccess { get; set; }

        private IApplicationConfigurationProcess CreateBusinessProcess()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
            EntityDataAccess = Substitute.For<IApplicationConfigurationRepository>();
            IConfigurationScopeProcess configurationScopeProcess = Substitute.For<IConfigurationScopeProcess>();
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            IApplicationConfigurationProcess process = new ApplicationConfigurationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, EntityDataAccess, StatusDataAccess, UserProfileDataAccess, configurationScopeProcess, applicationProcess, userProfileProcess);

            return process;
        }

        [TestCase]
        public void Test_GetValue_True()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Boolean expectedValue = true;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_False()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Boolean expectedValue = true;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Char()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Char expectedValue = 'Z';

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_String()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            String expectedValue = Guid.NewGuid().ToString();

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_SByte()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const SByte expectedValue = SByte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Byte()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Byte expectedValue = Byte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int16()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Int16 expectedValue = Int16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt16()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const UInt16 expectedValue = UInt16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int32()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Int32 expectedValue = Int32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt32()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const UInt32 expectedValue = UInt32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int64()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Int64 expectedValue = Int64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt64()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const UInt64 expectedValue = UInt64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Object actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_TimeSpan()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            TimeSpan expectedValue = new TimeSpan(10, 5, 0);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            TimeSpan actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Date()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            DateTime expectedValue = new DateTime(2023, 09, 08);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            DateTime actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_DateTime()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            DateTime expectedValue = new DateTime(2023, 09, 08, 21, 38, 45);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            DateTime actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_DateTimeMilliseconds()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            DateTime expectedValue = new DateTime(2023, 09, 08, 21, 38, 45, 123);
            
            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            DateTime actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Guid()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            Guid expectedValue = new Guid("{0B368339-E43E-4AFF-9FBC-C9F0074FD068}");

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Guid actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Decimal()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Decimal expectedValue = Decimal.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Decimal actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Double()
        {
            AppId applicationId = new AppId(0);
            const String key = "key";
            const Double expectedValue = 1.79769313486232d;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(default(String));

            Double actualValue = process.GetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key, expectedValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}
