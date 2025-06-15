//-----------------------------------------------------------------------
// <copyright file="NationalLotteryModelRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Repository;

using NationalLottery.Interfaces;

//using FDC = Foundation.Common.DataColumns;
//using NLModels = NationalLottery.Models;

namespace NationalLottery.Repository
{
    /// <summary>
    /// Defines the NationalLotteryModelDataAccess class
    /// Provides entity specific Data Access services
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <see cref="FoundationModelRepository{TModel}" />
    public abstract class NationalLotteryModelDataAccess<TModel> : FoundationModelRepository<TModel>, INationalLotteryModelRepository<TModel> where TModel : INationalLotteryModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="INationalLotteryModelRepository{TModel}"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        protected NationalLotteryModelDataAccess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            INationalLotteryDatabaseProvider databaseProvider,
            IDateTimeService dateTimeService
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                databaseProvider,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, databaseProvider, dateTimeService);

            // Does nothing

            LoggingHelpers.TraceCallReturn();
        }
    }
}
