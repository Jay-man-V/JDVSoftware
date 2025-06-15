//-----------------------------------------------------------------------
// <copyright file="ApplicationUserRoleViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Application/User/Role maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IApplicationUserRole}" />
    [DependencyInjectionTransient]
    public class ApplicationUserRoleViewModel : GenericDataGridViewModelBase<IApplicationUserRole>, IApplicationUserRoleViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationUserRoleViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="applicationUserRoleProcess">The application user role process.</param>
        public ApplicationUserRoleViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IApplicationUserRoleProcess applicationUserRoleProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                applicationUserRoleProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, applicationUserRoleProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
