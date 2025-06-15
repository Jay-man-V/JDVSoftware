//-----------------------------------------------------------------------
// <copyright file="EuroMillionsRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Repository;
using Foundation.Interfaces;

using NationalLottery.Interfaces;

using NLDC = NationalLottery.Common.DataColumns;

namespace NationalLottery.Repository
{
    /// <summary>
    /// Defines the Euro Millions Numbers Data Access class
    /// </summary>
    /// <see cref="IEuroMillionsNumbers" />
    [DependencyInjectionTransient]
    public class EuroMillionsNumbersRepository : NationalLotteryModelDataAccess<IEuroMillionsNumbers>, IEuroMillionsNumbersRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EuroMillionsNumbersRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        public EuroMillionsNumbersRepository
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            INationalLotteryDatabaseProvider databaseProvider,
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

        }

        /// <inheritdoc cref="EntityName"/>
        protected override String EntityName => NLDC.EuroMillionsNumbers.EntityName;

        /// <inheritdoc cref="TableName"/>
        protected override String TableName => NLDC.TableNames.EuroMillionsNumbers;

        /// <inheritdoc cref="HasValidityPeriodColumns"/>
        public override Boolean HasValidityPeriodColumns => false;
    }
}
