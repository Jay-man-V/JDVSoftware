//-----------------------------------------------------------------------
// <copyright file="GetValueTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using NSubstitute;

using Foundation.BusinessProcess;
using Foundation.Interfaces;
using Foundation.Resources;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.BusinessProcess.CoreTests.ApplicationConfigurationTests
{
    /// <summary>
    /// Summary description for ApplicationConfigurationProcessTests
    /// </summary>
    [TestFixture]
    public class GetValueTests : UnitTestBase
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
        public void Test_GetNullValue_Exception()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = null;

            String errorMessage = $"Configuration value with Key '{key}' for application id '{applicationId.TheAppId}' not found. Null value retrieved from database.";
            NullValueException actualException = null;

            try
            {
                IApplicationConfigurationProcess process = CreateBusinessProcess();
                EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);
                process.GetValue<String>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);
            }
            catch (NullValueException e)
            {
                actualException = e;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));

            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetValue_StandardKeys()
        {
            String[] standardKeys =
            {
                ApplicationConfigurationKeys.UserDataPath,
                ApplicationConfigurationKeys.SystemDataPath,
                ApplicationConfigurationKeys.EmailFromAddress,
                ApplicationConfigurationKeys.EmailSmtpHostAddress,
                ApplicationConfigurationKeys.EmailSmtpHostPort,
                ApplicationConfigurationKeys.EmailSmtpHostPort,
                ApplicationConfigurationKeys.EmailSmtpHostEnableSsl,

                ApplicationConfigurationKeys.EmailSmtpHostUsername,
                ApplicationConfigurationKeys.EmailSmtpHostPassword,
                ApplicationConfigurationKeys.ServiceHolidaysNationalUkUrl,
                ApplicationConfigurationKeys.ServiceGeneratorPasswordRandomUrl,
                ApplicationConfigurationKeys.ServiceGeneratorPasswordMemorableUrl,
                ApplicationConfigurationKeys.ServiceGeneratorPasswordRuleLength,
                ApplicationConfigurationKeys.ServiceGeneratorPasswordRuleCount,
            };

            AppId applicationId = new AppId(0);

            foreach (String key in standardKeys)
            {
                String expectedValueFromDatabase = Guid.NewGuid().ToString();

                IApplicationConfigurationProcess process = CreateBusinessProcess();
                EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

                String actualValue = process.GetValue<String>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

                Assert.That(actualValue, Is.EqualTo(expectedValueFromDatabase));
            }
        }

        [TestCase]
        public void Test_GetValue_Boolean_True()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "true";
            const Boolean expectedValue = true;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Boolean actualValue = process.GetValue<Boolean>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Boolean_False()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "false";
            const Boolean expectedValue = false;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Boolean actualValue = process.GetValue<Boolean>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_TimeSpan()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "10:05:00";
            TimeSpan expectedValue = new TimeSpan(10, 5, 0);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            TimeSpan actualValue = process.GetValue<TimeSpan>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Date()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "2023-09-08";
            DateTime expectedValue = new DateTime(2023, 09, 08);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            DateTime actualValue = process.GetValue<DateTime>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_DateTime()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "2023-09-08 21:38:45";
            DateTime expectedValue = new DateTime(2023, 09, 08, 21, 38, 45);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            DateTime actualValue = process.GetValue<DateTime>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_DateTimeMilliseconds()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "2023-09-08 21:38:45.123";
            DateTime expectedValue = new DateTime(2023, 09, 08, 21, 38, 45, 123);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            DateTime actualValue = process.GetValue<DateTime>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Guid()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            Guid expectedValueFromDatabase = new Guid("{0B368339-E43E-4AFF-9FBC-C9F0074FD068}");
            Guid expectedValue = expectedValueFromDatabase;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase.ToString());

            Guid actualValue = process.GetValue<Guid>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Char()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const Char expectedValueFromDatabase = 'Z';
            Char expectedValue = expectedValueFromDatabase;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase.ToString());

            Char actualValue = process.GetValue<Char>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_String()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "{0B368339-E43E-4AFF-9FBC-C9F0074FD068}";
            String expectedValue = expectedValueFromDatabase;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            String actualValue = process.GetValue<String>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int16()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "32767";
            const Int16 expectedValue = Int16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Int16 actualValue = process.GetValue<Int16>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt16()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "65535";
            const UInt16 expectedValue = UInt16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            UInt16 actualValue = process.GetValue<UInt16>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int32()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "2147483647";
            const Int32 expectedValue = Int32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Int32 actualValue = process.GetValue<Int32>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt32()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "4294967295";
            const UInt32 expectedValue = UInt32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            UInt32 actualValue = process.GetValue<UInt32>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Int64()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "9223372036854775807";
            const Int64 expectedValue = Int64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Int64 actualValue = process.GetValue<Int64>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_UInt64()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "18446744073709551615";
            const UInt64 expectedValue = UInt64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            UInt64 actualValue = process.GetValue<UInt64>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Decimal()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "79228162514264337593543950335";
            const Decimal expectedValue = Decimal.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Decimal actualValue = process.GetValue<Decimal>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Double()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "1.79769313486232";
            const Double expectedValue = 1.79769313486232d;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Double actualValue = process.GetValue<Double>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_Byte()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "255";
            const Byte expectedValue = Byte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            Byte actualValue = process.GetValue<Byte>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        public void Test_GetValue_SByte()
        {
            AppId applicationId = new AppId(0);
            const String key = "value";
            const String expectedValueFromDatabase = "127";
            const SByte expectedValue = SByte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            EntityDataAccess.GetValue(Arg.Any<AppId>(), Arg.Any<IUserProfile>(), key).Returns(expectedValueFromDatabase);

            SByte actualValue = process.GetValue<SByte>(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, key);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}
