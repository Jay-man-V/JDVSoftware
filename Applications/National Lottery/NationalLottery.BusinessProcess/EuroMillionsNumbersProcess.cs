//-----------------------------------------------------------------------
// <copyright file="EuroMillionsNumbersProcess.cs" company="JDV Software Ltd">
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
    /// Euro Millions Numbers process
    /// </summary>
    [DependencyInjectionTransient]
    public class EuroMillionsNumbersProcess : CommonBusinessProcess<IEuroMillionsNumbers, IEuroMillionsNumbersRepository>, IEuroMillionsNumbersProcess
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EuroMillionsNumbersProcess"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="repository">The data access.</param>
        /// <param name="statusRepository">The status data access.</param>
        /// <param name="userProfileRepository">The user profile data access.</param>
        public EuroMillionsNumbersProcess
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IEuroMillionsNumbersRepository repository,
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
        public override String ScreenTitle => "EuroMillions Numbers";

        /// <inheritdoc cref="ICommonBusinessProcess.StatusBarText"/>
        public override String StatusBarText => "Number of Euro Millions Numbers:";

        /// <inheritdoc cref="ICommonBusinessProcess.GetColumnDefinitions()"/>
        public override List<IGridColumnDefinition> GetColumnDefinitions()
        {
            LoggingHelpers.TraceCallEnter();

            List<IGridColumnDefinition> retVal = GetStandardEntityColumnDefinitions();
            IGridColumnDefinition gridColumnDefinition;

            gridColumnDefinition = new GridColumnDefinition(120, NLDC.EuroMillionsNumbers.DrawDate, "Draw Date", typeof(DateTime))
            {
                DotNetFormat = Formats.DotNet.DateOnly,
            };
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.EuroMillionsNumbers.Ball1, "Ball 1", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.EuroMillionsNumbers.Ball2, "Ball 2", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.EuroMillionsNumbers.Ball3, "Ball 3", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.EuroMillionsNumbers.Ball4, "Ball 4", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(70, NLDC.EuroMillionsNumbers.Ball5, "Ball 5", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(100, NLDC.EuroMillionsNumbers.LuckyStar1, "Lucky Star 1", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(100, NLDC.EuroMillionsNumbers.LuckyStar2, "Lucky Star 2", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            gridColumnDefinition = new GridColumnDefinition(110, NLDC.EuroMillionsNumbers.Jackpot, "Jackpot", typeof(Int32));
            retVal.Add(gridColumnDefinition);

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
