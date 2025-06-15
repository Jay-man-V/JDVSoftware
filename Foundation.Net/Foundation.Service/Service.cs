//-----------------------------------------------------------------------
// <copyright file="Service.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Service
{
    public interface IMyService
    {
        /// <summary>
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// 
        /// </summary>
        void Stop();
    }

    [DependencyInjectionTransient]
    public class MyService : IMyService
    {
        public MyService
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IEventLogProcess eventLogProcess,
            IScheduledJobProcess scheduledJobProcess
        )
        {
            Core = core;
            RunTimeEnvironmentSettings = runTimeEnvironmentSettings;
            EventLogProcess = eventLogProcess;
            ScheduledJobProcess = scheduledJobProcess;
        }

        private ICore Core { get; }
        private IRunTimeEnvironmentSettings RunTimeEnvironmentSettings { get; }
        private IEventLogProcess EventLogProcess { get; }
        private IScheduledJobProcess ScheduledJobProcess { get; }

        private LogId ParentLogId { get; set; }

        public void Start()
        {
            LoggingHelpers.TraceCallEnter();

            LoggingHelpers.LogInformationMessage("Service starting");

            ParentLogId = EventLogProcess.StartTask(Core.ApplicationId, "Scheduler Service", "Scheduler Service", "Start");

            ScheduledJobProcess.StartJobs(ParentLogId);

            LoggingHelpers.LogInformationMessage("Service started");

            LoggingHelpers.TraceCallReturn();
        }

        public void Stop()
        {
            LoggingHelpers.TraceCallEnter();

            LoggingHelpers.LogInformationMessage("Service stopping");

            ScheduledJobProcess.StopJobs(ParentLogId);

            EventLogProcess.CreateLogEntry(ParentLogId, Core.ApplicationId, "Scheduler Service", "Scheduler Service", "Stop", LogSeverity.Information, "Stop");

            LoggingHelpers.LogInformationMessage("Service stopped");

            LoggingHelpers.TraceCallReturn();
        }
    }
}
