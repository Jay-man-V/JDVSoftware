//-----------------------------------------------------------------------
// <copyright file="ApplicationRoleViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Application Role maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IApplicationRole}" />
    [DependencyInjectionTransient]
    public class ApplicationRoleViewModel : GenericDataGridViewModelBase<IApplicationRole>, IApplicationRoleViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationRoleViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="applicationRoleProcess">The application role process.</param>
        public ApplicationRoleViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IApplicationRoleProcess applicationRoleProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                applicationRoleProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, applicationRoleProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
