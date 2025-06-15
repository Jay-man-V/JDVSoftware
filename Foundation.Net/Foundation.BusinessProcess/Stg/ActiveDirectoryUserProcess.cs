﻿//-----------------------------------------------------------------------
// <copyright file="ActiveDirectoryUserProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.BusinessProcess
{
    /// <summary>
    /// Defines the Active Directory User Process
    /// </summary>
    [DependencyInjectionTransient]
    public class ActiveDirectoryUserProcess : CommonBusinessProcess<IActiveDirectoryUser, IActiveDirectoryUserRepository>, IActiveDirectoryUserProcess
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ActiveDirectoryUserProcess" /> class.
        /// </summary>
        /// <param name="core">The Foundation Core service</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="repository">The data access.</param>
        /// <param name="statusRepository">The status data access.</param>
        /// <param name="userProfileRepository">The user profile data access.</param>
        public ActiveDirectoryUserProcess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IActiveDirectoryUserRepository repository,
            IStatusRepository statusRepository,
            IUserProfileRepository userProfileRepository
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                repository,
                statusRepository,
                userProfileRepository
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, repository, statusRepository, userProfileRepository);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalAction1"/>
        public override Boolean HasOptionalAction1 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Action1Name"/>
        public override String Action1Name => "Save to Staging";


        /// <inheritdoc cref="ICommonBusinessProcess.HasOptionalAction2"/>
        public override Boolean HasOptionalAction2 => true;

        /// <inheritdoc cref="ICommonBusinessProcess.Action2Name"/>
        public override String Action2Name => "Sync User Profiles";


        /// <inheritdoc cref="ICommonBusinessProcess.ScreenTitle"/>
        public override String ScreenTitle => "Active Directory Users";

        /// <inheritdoc cref="ICommonBusinessProcess.StatusBarText"/>
        public override String StatusBarText => "Number of Active Directory Users:";

        /// <inheritdoc cref="ICommonBusinessProcess.ComboBoxDisplayMember" />
        public override String ComboBoxDisplayMember => FDC.ActiveDirectoryUser.FullName;

        /// <inheritdoc cref="ICommonBusinessProcess.GetColumnDefinitions()" />
        public override List<IGridColumnDefinition> GetColumnDefinitions()
        {
            LoggingHelpers.TraceCallEnter();

            List<IGridColumnDefinition> retVal = new List<IGridColumnDefinition>();
            IGridColumnDefinition gridColumnDefinition;

            gridColumnDefinition = new GridColumnDefinition(450, FDC.ActiveDirectoryUser.ObjectSId, "Object SId", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(200, FDC.ActiveDirectoryUser.Name, "User name", typeof(String));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(400, FDC.ActiveDirectoryUser.FullName, "Full name", typeof(String));
            retVal.Add(gridColumnDefinition);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
