﻿//-----------------------------------------------------------------------
// <copyright file="ContractRepository.cs" company="JDV Software Ltd">
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
    /// Defines the Contract Data Access class
    /// </summary>
    /// <see cref="IContract" />
    [DependencyInjectionTransient]
    public class ContractRepository : FoundationModelRepository<IContract>, IContractRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ContractRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings.</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        public ContractRepository
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
        protected override String EntityName => FDC.Contract.EntityName;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.TableName"/>
        protected override String TableName => FDC.TableNames.Contract;
    }
}
