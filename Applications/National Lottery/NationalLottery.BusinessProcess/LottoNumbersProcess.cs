//-----------------------------------------------------------------------
// <copyright file="LottoProcess.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Foundation.BusinessProcess;
using Foundation.Common;
using Foundation.Interfaces;
using Foundation.Resources;

using NationalLottery.Interfaces;

using NLDC = NationalLottery.Common.DataColumns;

namespace NationalLottery.BusinessProcess
{
    /// <summary>
    /// Lotto Numbers process
    /// </summary>
    [DependencyInjectionTransient]
    public class LottoNumbersProcess : CommonBusinessProcess<ILottoNumbers, ILottoNumbersRepository>, ILottoNumbersProcess
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LottoNumbersProcess"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="repository">The data access.</param>
        /// <param name="statusRepository">The status data access.</param>
        /// <param name="userProfileRepository">The user profile data access.</param>
        public LottoNumbersProcess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            ILottoNumbersRepository repository,
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
        }

        /// <inheritdoc cref="ICommonBusinessProcess.ScreenTitle"/>
        public override String ScreenTitle => "Lotto Numbers";

        /// <inheritdoc cref="ICommonBusinessProcess.StatusBarText"/>
        public override String StatusBarText => "Number of Lotto Numbers:";

        /// <inheritdoc cref="ICommonBusinessProcess.GetColumnDefinitions()"/>
        public override List<IGridColumnDefinition> GetColumnDefinitions()
        {
            LoggingHelpers.TraceCallEnter();

            List<IGridColumnDefinition> retVal = GetStandardEntityColumnDefinitions();
            IGridColumnDefinition gridColumnDefinition;

            gridColumnDefinition = new GridColumnDefinition(120, NLDC.LottoNumbers.DrawDate, "Draw Date", typeof(DateTime))
            {
                DotNetFormat = Formats.DotNet.DateOnly,
            };
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball1, "Ball 1", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball2, "Ball 2", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball3, "Ball 3", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball4, "Ball 4", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball5, "Ball 5", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.LottoNumbers.Ball6, "Ball 6", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(100, NLDC.LottoNumbers.BonusBall1, "Bonus Ball 6", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
