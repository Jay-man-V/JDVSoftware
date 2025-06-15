﻿//-----------------------------------------------------------------------
// <copyright file="MockScheduledTaskWithError.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Interfaces;

namespace Foundation.Tests.Unit.Mocks
{
    public class MockScheduledTaskWithError : MockScheduledTask
    {
        public MockScheduledTaskWithError
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IEventLogProcess eventLogProcess, 
            ICalendarProcess calendarProcess
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                eventLogProcess,
                calendarProcess
            )
        {
        }

        /// <inheritdoc cref="IScheduledTask.Process(LogId, String)"/>
        public override void Process(LogId logId, String taskParameters)
        {
            base.Process(logId, taskParameters);

            throw new Exception("Forced exception to test code");
        }
    }
}
