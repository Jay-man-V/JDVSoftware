//-----------------------------------------------------------------------
// <copyright file="ScheduledDataStatusViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for ScheduledDataStatus maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IScheduledDataStatus}" />
    [DependencyInjectionTransient]
    public class ScheduledDataStatusViewModel : GenericDataGridViewModelBase<IScheduledDataStatus>, IScheduledDataStatusViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ScheduledDataStatusViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="scheduledDataStatusProcess">The scheduled task process.</param>
        public ScheduledDataStatusViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IScheduledDataStatusProcess scheduledDataStatusProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                scheduledDataStatusProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, scheduledDataStatusProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
