﻿//-----------------------------------------------------------------------
// <copyright file="TraceLogWriterTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;

using NSubstitute;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.LoggingTests.LogWritersTests
{
    /// <summary>
    /// The Trace Log Writer Tests
    /// </summary>
    [TestFixture]
    public class TraceLogWriterTests : UnitTestBase
    {
        /// <summary>
        /// The requested log level
        /// </summary>
        private const TraceLevel RequestedLogLevel = TraceLevel.Info;

        /// <summary>
        /// The message prefix
        /// </summary>
        private const String MessagePrefix = "UnitTest Message";

        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [TestCase]
        public void Test_Constructor()
        {
            TraceLogWriter logWriter = new TraceLogWriter(RunTimeEnvironmentSettings, DateTimeService, RequestedLogLevel, MessagePrefix);

            Assert.That(logWriter.RequestedLogLevel, Is.EqualTo(RequestedLogLevel));
            Assert.That(logWriter.MessagePrefix, Is.EqualTo(MessagePrefix));
        }

        /// <summary>
        /// Tests WriteMessage.
        /// </summary>
        [TestCase]
        public void Test_WriteMessage()
        {
            IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();

            CustomTraceListener customTraceListener = new CustomTraceListener();
            Trace.Listeners.Add(customTraceListener);
            TraceLogWriter logWriter = new TraceLogWriter(RunTimeEnvironmentSettings, DateTimeService, RequestedLogLevel, MessagePrefix);

            String outputMessage = Guid.NewGuid().ToString();
            String expectedMessage = outputMessage + Environment.NewLine;
            logWriter.WriteMessage(outputMessage);

            String actualMessage = customTraceListener.Message;

            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
            Trace.Listeners.Remove(customTraceListener);
        }

        /// <summary>
        /// Tests WriteLine.
        /// </summary>
        [TestCase]
        public void Test_CustomTraceListener_WriteLine()
        {
            CustomTraceListener customTraceListener = new CustomTraceListener();
            Trace.Listeners.Add(customTraceListener);
            String outputMessage = Guid.NewGuid().ToString();
            String expectedMessage = outputMessage + Environment.NewLine;
            customTraceListener.WriteLine(outputMessage);

            String actualMessage = customTraceListener.Message;

            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
            Trace.Listeners.Remove(customTraceListener);
        }

        /// <summary>
        /// Tests Write.
        /// </summary>
        [TestCase]
        public void Test_CustomTraceListener_Write()
        {
            CustomTraceListener customTraceListener = new CustomTraceListener();
            Trace.Listeners.Add(customTraceListener);
            String outputMessage = Guid.NewGuid().ToString();
            String expectedMessage = outputMessage;
            customTraceListener.Write(outputMessage);

            String actualMessage = customTraceListener.Message;

            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
            Trace.Listeners.Remove(customTraceListener);
        }
    }
}
