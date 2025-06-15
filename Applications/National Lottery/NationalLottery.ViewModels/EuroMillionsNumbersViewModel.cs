//-----------------------------------------------------------------------
// <copyright file="EuroMillionsNumbersViewModel.cs" company="JDV Software Ltd">
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
    /// The User Interface interaction logic for Euro Millions Numbers maintenance
    /// </summary>
    /// <seealso cref="NationalLotteryViewModelBase" />
    [DependencyInjectionTransient]
    public class EuroMillionsNumbersViewModel : GenericDataGridViewModelBase<IEuroMillionsNumbers>, IEuroMillionsNumbersViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EuroMillionsNumbersViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="euroMillionsNumbersProcess">The euro millions numbers process</param>
        public EuroMillionsNumbersViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IEuroMillionsNumbersProcess euroMillionsNumbersProcess
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                euroMillionsNumbersProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, euroMillionsNumbersProcess);

            EuroMillionsNumbersProcess = euroMillionsNumbersProcess;

            LoggingHelpers.TraceCallReturn();
        }

        /// <summary>
        /// Gets the Euro Millions process.
        /// </summary>
        /// <value>
        /// The Euro Millions process.
        /// </value>
        private IEuroMillionsNumbersProcess EuroMillionsNumbersProcess { get; }

        /// <summary>
        /// Gets or sets all Euro Millions Numbers.
        /// </summary>
        /// <value>
        /// All Euro Millions Numbers.
        /// </value>
        private List<IEuroMillionsNumbers> AllEuroMillionsNumbers { get; set; }

        /// <inheritdoc cref="IGenericDataGridViewModelBase{TModel}.StatusBarText"/>
        public override String StatusBarText => CommonBusinessProcess.StatusBarText;

        /// <inheritdoc cref="GenericDataGridViewModelBase{TModel}.RefreshData()"/>
        protected override List<IEuroMillionsNumbers> RefreshData()
        {
            LoggingHelpers.TraceCallEnter();

            AllEuroMillionsNumbers = base.RefreshData();

            LoggingHelpers.TraceCallReturn(AllEuroMillionsNumbers);

            return AllEuroMillionsNumbers;
        }
    }
}
