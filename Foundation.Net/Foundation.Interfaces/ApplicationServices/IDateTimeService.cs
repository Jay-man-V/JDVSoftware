﻿//-----------------------------------------------------------------------
// <copyright file="IDateTimeService.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Defines the behaviour of the Date/Time Service
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Gets the start of week. This is currently Monday
        /// </summary>
        /// <value>
        /// The start of week.
        /// </value>
        DayOfWeek StartOfWeek { get; }

        /// <summary>
        /// Gets the system date time now.
        /// </summary>
        /// <value>
        /// The system date time now.
        /// </value>
        DateTime SystemDateTimeNow { get; }

        /// <summary>
        /// Gets the system date time now.
        /// </summary>
        /// <value>
        /// The system date time now.
        /// </value>
        DateTime SystemDateTimeNowWithoutMilliseconds { get; }

        /// <summary>
        /// Gets the start of current month/year.
        /// </summary>
        /// <value>
        /// The start of month.
        /// </value>
        DateTime StartOfMonth { get; }

        /// <summary>
        /// Gets the end of current month/year.
        /// </summary>
        /// <value>
        /// The end of month.
        /// </value>
        DateTime EndOfMonth { get; }

        /// <summary>
        /// Gets the start of last month.
        /// </summary>
        /// <value>
        /// The start of last month.
        /// </value>
        DateTime StartOfLastMonth { get; }

        /// <summary>
        /// Gets the end of last month.
        /// </summary>
        /// <value>
        /// The end of last month.
        /// </value>
        DateTime EndOfLastMonth { get; }

        /// <summary>
        /// Gets the start of the <paramref name="targetMonth"/>
        /// </summary>
        /// <param name="targetMonth"></param>
        /// <returns></returns>
        DateTime GetStartOfMonth(DateTime targetMonth);

        /// <summary>
        /// Gets the start of the month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        DateTime GetStartOfMonth(Int32 year, Int32 month);

        /// <summary>
        /// Gets the end of the <paramref name="targetMonth"/>
        /// </summary>
        /// <param name="targetMonth"></param>
        /// <returns></returns>
        DateTime GetEndOfMonth(DateTime targetMonth);

        /// <summary>
        /// Gets the end of the month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        DateTime GetEndOfMonth(Int32 year, Int32 month);

        /// <summary>
        /// Makes a Utc version of the supplied <paramref name="date"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime MakeUtcDateTime(DateTime date);

        /// <summary>
        /// Makes a Utc version of the supplied <paramref name="date"/> and <paramref name="time"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        DateTime MakeUtcDateTime(DateTime date, TimeSpan time);

        /// <summary>
        /// Makes a Utc version of the supplied <paramref name="date"/> and <paramref name="hours"/>, <paramref name="minutes"/>, <paramref name="seconds"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        DateTime MakeUtcDateTime(DateTime date, Int32 hours, Int32 minutes, Int32 seconds);

        /// <summary>
        /// Gets the first date of the previous quarter
        /// <para>
        /// Month = 01...03 => Previous Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// <para>
        /// Month = 04...06 => Previous Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// <para>
        /// Month = 07...09 => Previous Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 10...12 => Previous Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetStartOfPreviousQuarter();

        /// <summary>
        /// Gets the last date of the previous quarter
        /// <para>
        /// Month = 01...03 => Previous Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// <para>
        /// Month = 04...06 => Previous Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// <para>
        /// Month = 07...09 => Previous Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 10...12 => Previous Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetEndOfPreviousQuarter();

        /// <summary>
        /// Gets the first date of the current quarter
        /// <para>
        /// Month = 01...03 => Current Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// <para>
        /// Month = 04...06 => Current Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 07...09 => Current Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// <para>
        /// Month = 10...12 => Current Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetStartOfCurrentQuarter();

        /// <summary>
        /// Gets the last date of the current quarter
        /// <para>
        /// Month = 01...03 => Current Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// <para>
        /// Month = 04...06 => Current Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 07...09 => Current Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// <para>
        /// Month = 10...12 => Current Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetEndOfCurrentQuarter();

        /// <summary>
        /// Gets the first date of the next quarter
        /// <para>
        /// Month = 01...03 => Next Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 04...06 => Next Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// <para>
        /// Month = 07...09 => Next Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// <para>
        /// Month = 10...12 => Next Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetStartOfNextQuarter();

        /// <summary>
        /// Gets the last date of the next quarter
        /// <para>
        /// Month = 01...03 => Next Quarter = 04->06, Apr, May, Jun
        /// </para>
        /// <para>
        /// Month = 04...06 => Next Quarter = 07->09, Jul, Aug, Sep
        /// </para>
        /// <para>
        /// Month = 07...09 => Next Quarter = 10->12, Oct, Nov, Dec
        /// </para>
        /// <para>
        /// Month = 10...12 => Next Quarter = 01->03, Jan, Feb, Mar
        /// </para>
        /// </summary>
        /// <returns></returns>
        DateTime GetEndOfNextQuarter();
    }
}
