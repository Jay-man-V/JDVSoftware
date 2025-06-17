//-----------------------------------------------------------------------
// <copyright file="MenuItemRepository.cs" company="JDV Software Ltd">
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
    /// Defines the Menu Item Data Access class
    /// </summary>
    /// <see cref="ICountry" />
    [DependencyInjectionTransient]
    public class MenuItemRepository : FoundationModelRepository<IMenuItem>, IMenuItemRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MenuItemRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings.</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        public MenuItemRepository
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
        protected override String EntityName => FDC.MenuItem.EntityName;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.TableName"/>
        protected override String TableName => FDC.TableNames.MenuItem;
    }
}
