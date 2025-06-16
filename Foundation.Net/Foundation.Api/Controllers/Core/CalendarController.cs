//-----------------------------------------------------------------------
// <copyright file="CalendarController.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Web.Http;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Api.Controllers
{
    [DependencyInjectionTransient]
    public class CalendarController : CommonController, ICalendarController
    {
        public CalendarController
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            IDateTimeService dateTimeService,
            ICalendarProcess calendarProcess
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, dateTimeService, calendarProcess);

            CalendarProcess = calendarProcess;

            LoggingHelpers.TraceCallReturn();
        }

        private ICalendarProcess CalendarProcess { get; }

        /// <inheritdoc cref="ICalendarController.IsHoliday(String, DateTime)"/>
        [HttpGet]
        public Task<Boolean> IsHoliday([FromUri] String countryCode, [FromUri] DateTime date)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date);

            Task<Boolean> retVal = Task.Run(() => CalendarProcess.IsHoliday(countryCode, date));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetNextWorkingDay(String, DateTime)"/>
        [HttpGet]
        public Task<DateTime> GetNextWorkingDay([FromUri] String countryCode, [FromUri] DateTime date)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetNextWorkingDay(countryCode, date));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetNextWorkingDayScheduleInterval(String, DateTime, ScheduleInterval, Int32)"/>
        [HttpGet]
        public Task<DateTime> GetNextWorkingDayScheduleInterval([FromUri] String countryCode, [FromUri] DateTime date, [FromUri] ScheduleInterval intervalType, [FromUri] Int32 interval)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date, intervalType, interval);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetNextWorkingDay(countryCode, date, intervalType, interval));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.CheckIsWorkingDayOrGetNextWorkingDay(String, DateTime)"/>
        [HttpGet]
        public Task<DateTime> CheckIsWorkingDayOrGetNextWorkingDay([FromUri] String countryCode, [FromUri] DateTime date)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.CheckIsWorkingDayOrGetNextWorkingDay(countryCode, date));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetFirstWorkingDayOfMonthFromDate(String, DateTime)"/>
        [HttpGet]
        public Task<DateTime> GetFirstWorkingDayOfMonthFromDate([FromUri] String countryCode, [FromUri] DateTime date)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetFirstWorkingDayOfMonth(countryCode, date));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetFirstWorkingDayOfMonth(String, Int32, Int32)"/>
        [HttpGet]
        public Task<DateTime> GetFirstWorkingDayOfMonth([FromUri] String countryCode, [FromUri] Int32 year, [FromUri] Int32 month)
        {
            LoggingHelpers.TraceCallEnter(countryCode, year, month);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetFirstWorkingDayOfMonth(countryCode, year, month));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetLastWorkingDayOfMonthFromDate(String, DateTime)"/>
        [HttpGet]
        public Task<DateTime> GetLastWorkingDayOfMonthFromDate([FromUri] String countryCode, [FromUri] DateTime date)
        {
            LoggingHelpers.TraceCallEnter(countryCode, date);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetLastWorkingDayOfMonth(countryCode, date));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        /// <inheritdoc cref="ICalendarController.GetLastWorkingDayOfMonth(String, Int32, Int32)"/>
        [HttpGet]
        public Task<DateTime> GetLastWorkingDayOfMonth([FromUri] String countryCode, [FromUri] Int32 year, [FromUri] Int32 month)
        {
            LoggingHelpers.TraceCallEnter(countryCode, year, month);

            Task<DateTime> retVal = Task.Run(() => CalendarProcess.GetLastWorkingDayOfMonth(countryCode, year, month));

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}