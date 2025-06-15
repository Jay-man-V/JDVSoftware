﻿//-----------------------------------------------------------------------
// <copyright file="DemoScheduledTask.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;

namespace Foundation.Server.ScheduledTasks
{
    /// <summary>
    /// Demo Scheduled Task
    /// </summary>
    [DependencyInjectionTransient]
    public class DemoScheduledTask : ScheduledTaskBase
    {
        /// <summary>
        /// Demo Scheduled Task
        /// </summary>
        /// <param name="core"></param>
        /// <param name="runTimeEnvironmentSettings"></param>
        /// <param name="dateTimeService"></param>
        /// <param name="eventLogProcess"></param>
        /// <param name="calendarProcess"></param>
        public DemoScheduledTask
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IEventLogProcess eventLogProcess,
            ICalendarProcess calendarProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                eventLogProcess,
                calendarProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, eventLogProcess, calendarProcess);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="IScheduledTask.Process(LogId, String)"/>
        public override void Process(LogId logId, String taskParameters)
        {
            DateTime currentDateTime = DateTimeService.SystemDateTimeNow;
            String message = $"{GetType()}::ProcessJob running at: {currentDateTime.ToString(Formats.DotNet.DateTimeSeconds)} with Task Parameters: '{taskParameters}'";
            LoggingHelpers.LogInformationMessage(message);
        }
    }
}
