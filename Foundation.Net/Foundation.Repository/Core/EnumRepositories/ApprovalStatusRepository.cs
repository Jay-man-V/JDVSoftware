﻿//-----------------------------------------------------------------------
// <copyright file="ApprovalStatusRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Repository
{
    /// <summary>
    /// Defines the Approval Status Data Access class
    /// </summary>
    /// <see cref="IApprovalStatus" />
    [DependencyInjectionTransient]
    public class ApprovalStatusRepository : FoundationModelRepository<IApprovalStatus>, IApprovalStatusRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApprovalStatusRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings.</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        public ApprovalStatusRepository
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            ICoreDatabaseProvider databaseProvider,
            IDateTimeService dateTimeService
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                databaseProvider,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, databaseProvider, dateTimeService);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="FoundationModelRepository{TModel}.EntityName"/>
        protected override String EntityName => FDC.ApprovalStatus.EntityName;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.TableName"/>
        protected override String TableName => FDC.TableNames.ApprovalStatus;
    }
}
