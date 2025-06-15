//-----------------------------------------------------------------------
// <copyright file="WorldRegionViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for World Region maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IWorldRegion}" />
    [DependencyInjectionTransient]
    public class WorldRegionViewModel : GenericDataGridViewModelBase<IWorldRegion>, IWorldRegionViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorldRegionViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="worldRegionProcess">The world region process.</param>
        public WorldRegionViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IWorldRegionProcess worldRegionProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                worldRegionProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, worldRegionProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
