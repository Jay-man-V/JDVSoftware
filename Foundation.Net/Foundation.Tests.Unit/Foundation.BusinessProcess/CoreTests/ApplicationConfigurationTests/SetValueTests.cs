//-----------------------------------------------------------------------
// <copyright file="SetValueTests.cs" company="JDV Software Ltd">
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
    public class SetValueTests : UnitTestBase
    {
        private IApplicationConfigurationRepository EntityRepository { get; set; }

        private IApplicationConfigurationProcess CreateBusinessProcess()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
            EntityRepository = Substitute.For<IApplicationConfigurationRepository>();
            IConfigurationScopeProcess configurationScopeProcess = Substitute.For<IConfigurationScopeProcess>();
            IApplicationProcess applicationProcess = Substitute.For<IApplicationProcess>();
            IUserProfileProcess userProfileProcess = Substitute.For<IUserProfileProcess>();

            IApplicationConfigurationProcess process = new ApplicationConfigurationProcess(CoreInstance, RunTimeEnvironmentSettings, dateTimeService, EntityRepository, StatusRepository, UserProfileRepository, configurationScopeProcess, applicationProcess, userProfileProcess);

            return process;
        }

        [TestCase]
        public void Test_SetValue_True()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Boolean value = true;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_False()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Boolean value = true;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Char()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Char value = 'Z';

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_String()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            String value = Guid.NewGuid().ToString();

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);
        }

        [TestCase]
        public void Test_SetValue_Sbyte()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const SByte value = SByte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Byte()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Byte value = Byte.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Int16()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Int16 value = Int16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_UInt16()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const UInt16 value = UInt16.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Int32()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Int32 value = Int32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_UInt32()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const UInt32 value = UInt32.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Int64()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Int64 value = Int64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_UInt64()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const UInt64 value = UInt64.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Double()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            const Double value = Double.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Decimal()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            Decimal value = Decimal.MaxValue;

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Guid()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            Guid value = new Guid("{0b368339-e43e-4aff-9fbc-c9f0074fd068}");

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_TimeSpan()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            TimeSpan value = new TimeSpan(10, 5, 0);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value.ToString());
        }

        [TestCase]
        public void Test_SetValue_Date()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            DateTime value = new DateTime(2023, 09, 08);
            String expected = value.ToString(Formats.DotNet.Iso8601DateTimeMilliseconds);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, expected);
        }

        [TestCase]
        public void Test_SetValue_DateTime()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            DateTime value = new DateTime(2023, 09, 08, 21, 38, 45);
            String expected = value.ToString(Formats.DotNet.Iso8601DateTimeMilliseconds);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, expected);
        }

        [TestCase]
        public void Test_SetValue_DateTimeMilliseconds()
        {
            AppId applicationId = new AppId(0);
            const ConfigurationScope configurationScope = ConfigurationScope.System;
            const String key = "value";
            DateTime value = new DateTime(2023, 09, 08, 21, 38, 45, 123);
            String expected = value.ToString(Formats.DotNet.Iso8601DateTimeMilliseconds);

            IApplicationConfigurationProcess process = CreateBusinessProcess();
            process.SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, value);

            EntityRepository.Received().SetValue(applicationId, CoreInstance.CurrentLoggedOnUser.UserProfile, configurationScope, key, expected);
        }
    }
}
