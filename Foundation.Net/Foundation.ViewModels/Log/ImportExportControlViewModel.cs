//-----------------------------------------------------------------------
// <copyright file="ImportExportControlViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Event Log Application maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IImportExportControl}" />
    [DependencyInjectionTransient]
    public class ImportExportControlViewModel : GenericDataGridViewModelBase<IImportExportControl>, IImportExportControlViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ImportExportControlViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="importExportControlProcess">The event log application process.</param>
        public ImportExportControlViewModel
        (   
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IImportExportControlProcess importExportControlProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                importExportControlProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, importExportControlProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
