//-----------------------------------------------------------------------
// <copyright file="LottoNumbersViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.ViewModels;

using NationalLottery.Interfaces;

namespace NationalLottery.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Lotto Numbers maintenance
    /// </summary>
    /// <seealso cref="NationalLotteryViewModelBase" />
    [DependencyInjectionTransient]
    public class LottoNumbersViewModel : GenericDataGridViewModelBase<ILottoNumbers>, ILottoNumbersViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LottoNumbersViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="wpfApplicationObjects">The wpf application objects collection.</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="lottoNumbersProcess">The lotto numbers process.</param>
        public LottoNumbersViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IWpfApplicationObjects wpfApplicationObjects,
            IFileApi fileApi,
            ILottoNumbersProcess lottoNumbersProcess
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                wpfApplicationObjects,
                fileApi,
                lottoNumbersProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, wpfApplicationObjects, fileApi, lottoNumbersProcess);

            LottoNumbersProcess = lottoNumbersProcess;

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Gets the Lotto process.
        /// </summary>
        /// <value>
        /// The Lotto process.
        /// </value>
        private ILottoNumbersProcess LottoNumbersProcess { get; }

        /// <summary>
        /// Gets or sets all Lotto Numbers.
        /// </summary>
        /// <value>
        /// All Lotto Numbers.
        /// </value>
        private List<ILottoNumbers> AllLottoNumbers { get; set; }

        /// <inheritdoc cref="IGenericDataGridViewModelBase{TModel}.StatusBarText"/>
        public override String StatusBarText => CommonBusinessProcess.StatusBarText;

        /// <inheritdoc cref="GenericDataGridViewModelBase{TModel}.RefreshData()"/>
        protected override List<ILottoNumbers> RefreshData()
        {
            LoggingHelpers.TraceCallEnter();

            AllLottoNumbers = base.RefreshData();

            LoggingHelpers.TraceCallReturn(AllLottoNumbers);

            return AllLottoNumbers;
        }
    }
}
