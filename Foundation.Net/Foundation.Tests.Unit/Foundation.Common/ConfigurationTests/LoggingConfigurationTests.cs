//-----------------------------------------------------------------------
// <copyright file="LoggingConfigurationTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ConfigurationTests
{
    /// <summary>
    /// The Logging Configuration Tests class
    /// </summary>
    [TestFixture]
    public class LoggingConfigurationTests : UnitTestBase
    {
        [TestCase]
        public void Test_CountMembers()
        {
            // This test exists to ensure all the class are tested/checked in the next test
            PropertyInfo[] propertyInfos = GetInstanceOnlyPropertyInfosForType(typeof(LoggingConfiguration));
            Int32 index = 0;

            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Application)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.LogWriters)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Tracing)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Information)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Error)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Warning)));
            Assert.That(propertyInfos[index++].Name, Is.EqualTo(nameof(LoggingConfiguration.Audit)));

            Assert.That(propertyInfos.Length, Is.EqualTo(index));
        }

        /// <summary>
        /// Verify that the properties contain the expected values.
        /// </summary>
        [TestCase]
        public void Test_LoggingConfigurationProperties()
        {
            String expectedApplication = @"MyApp";

            String actualApplication = ApplicationSettings.LoggingConfiguration.Application;
            LogWriterCollection actualLogWriters = ApplicationSettings.LoggingConfiguration.LogWriters;
            TracingElement actualTracingElement = ApplicationSettings.LoggingConfiguration.Tracing;
            InformationElement actualInformationElement = ApplicationSettings.LoggingConfiguration.Information;
            ErrorElement actualErrorElement = ApplicationSettings.LoggingConfiguration.Error;
            WarningElement actualWarningElement = ApplicationSettings.LoggingConfiguration.Warning;
            AuditElement actualAuditElement = ApplicationSettings.LoggingConfiguration.Audit;

            Assert.That(actualApplication, Is.EqualTo(expectedApplication));
            Assert.That(actualLogWriters.GetType(), Is.EqualTo(typeof(LogWriterCollection)));
            Assert.That(actualTracingElement.GetType(), Is.EqualTo(typeof(TracingElement)));
            Assert.That(actualInformationElement.GetType(), Is.EqualTo(typeof(InformationElement)));
            Assert.That(actualErrorElement.GetType(), Is.EqualTo(typeof(ErrorElement)));
            Assert.That(actualWarningElement.GetType(), Is.EqualTo(typeof(WarningElement)));
            Assert.That(actualAuditElement.GetType(), Is.EqualTo(typeof(AuditElement)));

            Assert.That(actualTracingElement.Prefix, Is.EqualTo("Trace"));
            Assert.That(actualInformationElement.Prefix, Is.EqualTo("Information Message"));
            Assert.That(actualErrorElement.Prefix, Is.EqualTo("Error Message"));
            Assert.That(actualWarningElement.Prefix, Is.EqualTo("Warning Message"));
            Assert.That(actualAuditElement.Prefix, Is.EqualTo("Audit Message"));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_LogWritersCollection()
        {
            LogWriterCollection logWriters = ApplicationSettings.LoggingConfiguration.LogWriters;

            Assert.That(logWriters, Is.Not.EqualTo(null));
            Assert.That(logWriters.Count, Is.EqualTo(1));

            //String assemblyName = typeof(ApplicationSettings).Assembly.GetName().Name;
            //String typeName = typeof(ApplicationSettings).FullName;

            LogWriterElement logWriter = logWriters[0];

            Assert.That(logWriter.Events, Is.EqualTo("All"));
            Assert.That(logWriter.Assembly, Is.EqualTo("Foundation.Common"));
            Assert.That(logWriter.LoggingType, Is.EqualTo("Foundation.Common.LogWriters.TextFileWriter"));
        }
    }
}
