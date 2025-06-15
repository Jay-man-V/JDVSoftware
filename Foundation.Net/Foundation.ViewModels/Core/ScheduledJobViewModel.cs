//-----------------------------------------------------------------------
// <copyright file="ScheduledJobViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Scheduled Job maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IScheduledJob}" />
    [DependencyInjectionTransient]
    public class ScheduledJobViewModel : GenericDataGridViewModelBase<IScheduledJob>, IScheduledJobViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ScheduledJobViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="scheduledJobProcess">The scheduled task process.</param>
        public ScheduledJobViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IScheduledJobProcess scheduledJobProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                scheduledJobProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, scheduledJobProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
