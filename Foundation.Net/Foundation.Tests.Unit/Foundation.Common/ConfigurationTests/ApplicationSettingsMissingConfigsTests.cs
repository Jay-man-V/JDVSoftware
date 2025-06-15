//-----------------------------------------------------------------------
// <copyright file="LogWritingTests2.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ConfigurationTests
{
    /// <summary>
    /// Unit tests for ApplicationSettingsMissingConfigsTests
    /// </summary>
    [TestFixture]
    public class ApplicationSettingsMissingConfigsTests : UnitTestBase
    {
        /// <summary>
        /// Tests the correct exception is raised when no configuration is found.
        /// </summary>
        [TestCase]
        [DeploymentItem("NoEventLoggingConfig.App.config")]
        public void Test_NoEventLoggingConfig_Exception()
        {
            Exception actualException = null;

            using (AppConfigModifier newDomain = new AppConfigModifier("NoEventLoggingConfig.App.config"))
            {
                try
                {
                    String assemblyName = typeof(ApplicationSettings).Assembly.GetName().Name;
                    String typeName = typeof(ApplicationSettings).FullName;

                    newDomain.TargetDomain.CreateInstanceAndUnwrap(assemblyName, typeName);
                }
                catch (Exception exception)
                {
                    actualException = exception;
                }

                Assert.That(actualException, Is.Not.EqualTo(null));
                Assert.That(actualException, Is.InstanceOf<TargetInvocationException>());
                Assert.That(actualException.GetBaseException(), Is.InstanceOf<ConfigurationErrorsException>());

                String expectedMessage1 = "Exception has been thrown by the target of an invocation.";
                String actualMessage1 = actualException.Message;
                Assert.That(actualMessage1, Is.EqualTo(expectedMessage1));

                String expectedMessage2 = $"Application configuration not found. Missing section '{LoggingConstants.EventLoggingSection}'";
                String actualMessage2 = actualException.GetBaseException().Message;
                Assert.That(actualMessage2, Is.EqualTo(expectedMessage2));
            }
        }

        /// <summary>
        /// Tests the correct exception is raised when no connection string is found.
        /// </summary>
        [TestCase]
        [DeploymentItem("NoConnectionStringConfig.App.config")]
        public void Test_NoConnectionString_Exception()
        {
            String dataConnectionName = "ABC";
            String parameterName = "dataConnectionName";
            String expectedMessage1 = $"Cannot load Connection named '{dataConnectionName}'. Check to make sure the connection is defined in the Configuration File.\r\nParameter name: {parameterName}";
            Exception actualException = null;

            using (AppConfigModifier newDomain = new AppConfigModifier("NoConnectionStringConfig.App.config"))
            {
                try
                {
                    String assemblyName = typeof(ApplicationSettings).Assembly.GetName().Name;
                    String typeName = typeof(ApplicationSettings).FullName;

                    _ = newDomain.TargetDomain.CreateInstanceAndUnwrap(assemblyName, typeName);
                    ApplicationSettings.GetConnectionString(dataConnectionName);
                }
                catch (Exception exception)
                {
                    actualException = exception;
                }

                Assert.That(actualException, Is.Not.EqualTo(null));
                Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());

                ArgumentNullException anException = actualException as ArgumentNullException;

                String actualMessage1 = anException.Message;
                Assert.That(actualMessage1, Is.EqualTo(expectedMessage1));

                String actualParameterName = anException.ParamName;
                Assert.That(actualParameterName, Is.EqualTo(parameterName));
            }
        }

        /// <summary>
        /// Tests the correct exception is raised when no connection string is found.
        /// </summary>
        [TestCase]
        [DeploymentItem("NoTraceLevelConfig.App.config")]
        public void Test_NoTraceLevel()
        {
            using (AppConfigModifier newDomain = new AppConfigModifier("NoTraceLevelConfig.App.config"))
            {
                //Assembly commonAssembly = newDomain.TargetDomain.GetAssemblies().FirstOrDefault(n => n.FullName.StartsWith("Foundation.Common"));
                //Type loggingBase = commonAssembly.GetType("Foundation.Common.LoggingBase");

                //PropertyInfo[] properties = loggingBase.GetProperties(BindingFlags.Public | BindingFlags.Static);

                //PropertyInfo property = loggingBase.GetProperty("TraceSwitch", BindingFlags.Public | BindingFlags.Static);

                //TraceSwitch traceSwitch = property.GetValue(null, null) as TraceSwitch;
                //Assert.That(traceSwitch, Is.EqualTo(null));

                //LoggingHelpers.TraceMessage("Dummy call to trigger the LoggingBase constructor");

                //TraceSwitch traceSwitch1 = property.GetValue(null, null) as TraceSwitch;
                //Assert.That(traceSwitch1, Is.Not.EqualTo(null));
                //Assert.That(traceSwitch1.DisplayName, Is.EqualTo("TraceLevelSwitch"));
                //Assert.That(traceSwitch1.Description, Is.EqualTo(String.Empty));




                //TraceSwitch beforeTraceSwitch = LoggingBase.TraceSwitch;
                //Assert.That(beforeTraceSwitch, Is.EqualTo(null));

                LoggingHelpers.TraceMessage("Dummy call to trigger the LoggingBase constructor");

                TraceSwitch afterTraceSwitch = LoggingBase.TraceSwitch;
                Assert.That(afterTraceSwitch, Is.Not.EqualTo(null));
                Assert.That(afterTraceSwitch.DisplayName, Is.EqualTo("TraceLevelSwitch"));
                Assert.That(afterTraceSwitch.Description, Is.EqualTo(String.Empty));
            }
        }
    }
}
