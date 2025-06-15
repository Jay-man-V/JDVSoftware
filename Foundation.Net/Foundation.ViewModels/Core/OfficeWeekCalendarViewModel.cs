//-----------------------------------------------------------------------
// <copyright file="OfficeWeekCalendarViewModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// The User Interface interaction logic for Office Week Calendar maintenance
    /// </summary>
    /// <seealso cref="GenericDataGridViewModelBase{IOfficeWeekCalendar}" />
    [DependencyInjectionTransient]
    public class OfficeWeekCalendarViewModel : GenericDataGridViewModelBase<IOfficeWeekCalendar>, IOfficeWeekCalendarViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OfficeWeekCalendarViewModel"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings.</param>
        /// <param name="dateTimeService">The date time service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="clipBoardWrapper">The clip board wrapper</param>
        /// <param name="fileApi">The file service.</param>
        /// <param name="officeWeekCalendarProcess">The office week calendar process</param>
        public OfficeWeekCalendarViewModel
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            IDialogService dialogService,
            IClipBoardWrapper clipBoardWrapper,
            IFileApi fileApi,
            IOfficeWeekCalendarProcess officeWeekCalendarProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService,
                dialogService,
                clipBoardWrapper,
                fileApi,
                officeWeekCalendarProcess
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, dialogService, clipBoardWrapper, fileApi, officeWeekCalendarProcess);

            LoggingHelpers.TraceCallReturn();
        }
    }
}
