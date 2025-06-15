//-----------------------------------------------------------------------
// <copyright file="NationalLotteryViewModelBase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common;
using Foundation.Interfaces;
using Foundation.ViewModels;

namespace NationalLottery.ViewModels
{
    /// <summary>
    /// Implements generic routines for all National Lottery application view models
    /// </summary>
    /// <seealso cref="ViewModelBase" />
    public abstract class NationalLotteryViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NationalLotteryViewModelBase"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        protected NationalLotteryViewModelBase
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper
        )
            : base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                "National Lottery"
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
