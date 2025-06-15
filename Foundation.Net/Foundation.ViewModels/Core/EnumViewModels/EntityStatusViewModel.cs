//-----------------------------------------------------------------------
// <copyright file="EntityStatusViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Entity Status maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IEntityStatus}" />
    [DependencyInjectionTransient]
    public class EntityStatusViewModel : GenericDataGridViewModelBase<IEntityStatus>, IEntityStatusViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EntityStatusViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="entityStatusProcess">The Entity status process</param>
        public EntityStatusViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IEntityStatusProcess entityStatusProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                entityStatusProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, entityStatusProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
