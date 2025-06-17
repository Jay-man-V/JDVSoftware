//-----------------------------------------------------------------------
// <copyright file="LogSeverityViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Log Severity maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IApprovalStatus}" />
    [DependencyInjectionTransient]
    public class LogSeverityViewModel : GenericDataGridViewModelBase<ILogSeverity>, ILogSeverityViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LogSeverityViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="logSeverityProcess">The log severity process.</param>
        public LogSeverityViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            ILogSeverityProcess logSeverityProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                logSeverityProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, logSeverityProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
