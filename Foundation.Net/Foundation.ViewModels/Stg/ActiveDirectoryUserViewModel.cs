﻿//-----------------------------------------------------------------------
// <copyright file="UserManagementViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Active Directory User maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IActiveDirectoryUser}" />
    [DependencyInjectionTransient]
    public class ActiveDirectoryUserViewModel : GenericDataGridViewModelBase<IActiveDirectoryUser>, IActiveDirectoryUserViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ActiveDirectoryUserViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="activeDirectoryUserProcess">The active directory user process.</param>
        public ActiveDirectoryUserViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IActiveDirectoryUserProcess activeDirectoryUserProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                activeDirectoryUserProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, activeDirectoryUserProcess);

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Refreshes the data.
        /// </summary>
        protected override List<IActiveDirectoryUser> RefreshData()
        {
            LoggingHelpers.TraceCallEnter();

            base.RefreshData();

            Action1CommandEnabled = GridDataSource.Any();
            Action2CommandEnabled = GridDataSource.Any();

            LoggingHelpers.TraceCallReturn(GridDataSource);

            return GridDataSource;
        }

        /// <summary>
        /// Called when [action1 command execute].
        /// </summary>
        protected override void ExecuteAction1()
        {
            LoggingHelpers.TraceCallEnter();

            CommonBusinessProcess.Save(GridDataSource.ToList());

            Action2CommandEnabled = GridDataSource.Any();

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Called when [action2 command execute].
        /// </summary>
        protected override void ExecuteAction2()
        {
            LoggingHelpers.TraceCallEnter();

            UserProfileProcess.SyncActiveDirectoryUserDataFromStaging();

            LoggingHelpers.TraceCallReturn();
        }
    }
}
