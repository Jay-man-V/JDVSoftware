//-----------------------------------------------------------------------
// <copyright file="ExceptionOutputTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Text;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.LoggingTests.FormattingTests
{
    /// <summary>
    /// The Exception Output Tests
    /// </summary>
    [TestFixture]
    public class ExceptionOutputTests : UnitTestBase
    {
        private const String BaseFolder = @".ExpectedResults\Logging\Formatting\";

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_RethrowAsNew.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_RethrowAsNew()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(true);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService,actualException);
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_RethrowAsNewWithMessage.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_RethrowAsNewWithMessage()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(true);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService, actualException, "Show this message to the users");
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_RethrowAsNewWithMessageFormat.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_RethrowAsNewWithMessageFormat()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(true);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService, actualException, "Message: {0}. {1}", "A", 123);
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_ThrowOriginal.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_ThrowOriginal()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(false);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService, actualException);
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_ThrowOriginalWithMessage.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_ThrowOriginalWithMessage()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(false);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService, actualException, "Show this message to the users");
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [TestCase]
        [DeploymentItem(@".ExpectedResults\Logging\Formatting\TestNestedException_ThrowOriginalWithMessageFormat.txt", @".ExpectedResults\Logging\Formatting\")]
        public void TestNestedException_ThrowOriginalWithMessageFormat()
        {
            String functionName = LocationUtils.GetFunctionName();
            Exception actualException = null;

            IFileApi fileApi = CoreInstance.Container.Get<IFileApi>();
            String expectedValue = fileApi.GetFileContentsAsText(BaseFolder + functionName + ".txt", Encoding.UTF8);
            expectedValue = FixUpStringWithReplacements(expectedValue);

            try
            {
                Common.NestedExceptionFirstMethod(false);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            ExceptionOutput exceptionOutput = MessageFormatter.FormatMessage(RunTimeEnvironmentSettings, DateTimeService, actualException, "Message: {0}. {1}", "A", 123);
            String actualValue = exceptionOutput.ToString();
            actualValue = FixUpStringWithReplacements(actualValue);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}
