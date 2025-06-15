//-----------------------------------------------------------------------
// <copyright file="LottoRepository.cs" company="JDV Software Ltd">
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
    /// Defines the Lotto Numbers Data Access class
    /// </summary>
    /// <see cref="ILottoNumbers" />
    [DependencyInjectionTransient]
    public class LottoNumbersRepository : NationalLotteryModelDataAccess<ILottoNumbers>, ILottoNumbersRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LottoNumbersRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings</param>
        /// <param name="databaseProvider"></param>
        /// <param name="dateTimeService"></param>
        public LottoNumbersRepository
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
        protected override String EntityName => NLDC.LottoNumbers.EntityName;

        /// <inheritdoc cref="TableName"/>
        protected override String TableName => NLDC.TableNames.LottoNumbers;

        /// <inheritdoc cref="HasValidityPeriodColumns"/>
        public override Boolean HasValidityPeriodColumns => false;
    }
}
