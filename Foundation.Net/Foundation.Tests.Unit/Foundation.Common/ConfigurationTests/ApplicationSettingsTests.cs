//-----------------------------------------------------------------------
// <copyright file="ApplicationSettingsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ConfigurationTests
{
    /// <summary>
    /// The Application Settings Tests class
    /// </summary>
    [TestFixture]
    public class ApplicationSettingsTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetStaticPropertyInfosForType(typeof(ApplicationSettings));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationSettings.DefaultValidToDateTime)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationSettings.ApplicationName)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationSettings.ApplicationId)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationSettings.TraceLevel)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(ApplicationSettings.LoggingConfiguration)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Verify that the properties contain the expected values.
        /// </summary>
        [TestCase]
        public void Test_ApplicationSettingsProperties()
        {
            String expectedApplicationName = @"MyApp";
            AppId expectedApplicationId = new AppId(1);
            TraceLevel expectedTraceLevel = TraceLevel.Verbose;

            String actualApplicationName = ApplicationSettings.ApplicationName;
            AppId actualApplicationId = ApplicationSettings.ApplicationId;
            TraceLevel actualTraceLevel = ApplicationSettings.TraceLevel;

            Assert.That(actualApplicationName, Is.EqualTo(expectedApplicationName));
            Assert.That(actualApplicationId, Is.EqualTo(expectedApplicationId));
            Assert.That(actualTraceLevel, Is.EqualTo(expectedTraceLevel));

            Assert.That(ApplicationSettings.LoggingConfiguration, Is.Not.EqualTo(null));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_LoggingConfiguration()
        {
            String expectedApplicationName = "MyApp";
            String expectedTracingPrefix = "Trace";
            String expectedAuditPrefix = "Audit Message";
            String expectedInformationPrefix = "Information Message";
            String expectedWarningPrefix = "Warning Message";
            String expectedErrorPrefix = "Error Message";

            String actualApplicationName = ApplicationSettings.LoggingConfiguration.Application;
            String actualTracingPrefix = ApplicationSettings.LoggingConfiguration.Tracing.Prefix;
            String actualAuditPrefix = ApplicationSettings.LoggingConfiguration.Audit.Prefix;
            String actualInformationPrefix = ApplicationSettings.LoggingConfiguration.Information.Prefix;
            String actualWarningPrefix = ApplicationSettings.LoggingConfiguration.Warning.Prefix;
            String actualErrorPrefix = ApplicationSettings.LoggingConfiguration.Error.Prefix;

            Assert.That(actualApplicationName, Is.EqualTo(expectedApplicationName));
            Assert.That(actualTracingPrefix, Is.EqualTo(expectedTracingPrefix));
            Assert.That(actualAuditPrefix, Is.EqualTo(expectedAuditPrefix));
            Assert.That(actualInformationPrefix, Is.EqualTo(expectedInformationPrefix));
            Assert.That(actualWarningPrefix, Is.EqualTo(expectedWarningPrefix));
            Assert.That(actualErrorPrefix, Is.EqualTo(expectedErrorPrefix));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Trace()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Trace);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Verbose));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
            //Assert.That(ts.TraceInfo, Is.EqualTo(true));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(true));
            //Assert.That(ts.TraceWarning, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Debug()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Debug);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Verbose));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(true));
            //Assert.That(ts.TraceInfo, Is.EqualTo(true));
            //Assert.That(ts.TraceWarning, Is.EqualTo(true));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Information()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Information);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Info));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(false));
            //Assert.That(ts.TraceInfo, Is.EqualTo(true));
            //Assert.That(ts.TraceWarning, Is.EqualTo(true));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Warning()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Warning);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Warning));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(false));
            //Assert.That(ts.TraceInfo, Is.EqualTo(false));
            //Assert.That(ts.TraceWarning, Is.EqualTo(true));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Error()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Error);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Error));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(false));
            //Assert.That(ts.TraceInfo, Is.EqualTo(false));
            //Assert.That(ts.TraceWarning, Is.EqualTo(false));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_Critical()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.Critical);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Error));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(false));
            //Assert.That(ts.TraceInfo, Is.EqualTo(false));
            //Assert.That(ts.TraceWarning, Is.EqualTo(false));
            //Assert.That(ts.TraceError, Is.EqualTo(true));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_InitialiseLogLevel_None()
        {
            //TraceSwitch ts = ApplicationSettings.InitialiseLogLevel(LogLevel.None);

            //Assert.That(ts.DisplayName, Is.EqualTo("TraceLevelSwitch"));
            //Assert.That(ts.Description, Is.EqualTo(String.Empty));
            //Assert.That(ts.Level, Is.EqualTo(TraceLevel.Off));
            //Assert.That(ts.TraceVerbose, Is.EqualTo(false));
            //Assert.That(ts.TraceInfo, Is.EqualTo(false));
            //Assert.That(ts.TraceWarning, Is.EqualTo(false));
            //Assert.That(ts.TraceError, Is.EqualTo(false));
        }
    }
}
