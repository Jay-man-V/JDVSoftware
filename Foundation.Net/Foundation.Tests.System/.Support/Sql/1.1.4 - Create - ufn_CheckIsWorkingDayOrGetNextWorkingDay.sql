-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE OR ALTER FUNCTION [dbo].[ufn_CheckIsWorkingDayOrGetNextWorkingDay]
(
	@startDate DATE
)
RETURNS DATE
AS
BEGIN
	-- Declare the return variable here
    DECLARE @returnValue AS DATE

    DECLARE @workingDate AS DATE = @StartDate
	DECLARE @windowStartDate AS DATE
    DECLARE @windowEndDate AS DATE

    SELECT
        @windowStartDate = StartDate,
        @windowEndDate = NextEndDate
    FROM
    (
        SELECT
            @workingDate x,
            [Date] StartDate,
            GREATEST(LEAD ([Date], 1, [Date]) OVER (ORDER BY [Date]),
            LEAD ([Date], 2, [Date]) OVER (ORDER BY [Date])) AS NextEndDate
        FROM
            core.NonWorkingDay
        WHERE
            DATEPART(YEAR, [Date]) >= DATEPART(YEAR, @startDate)
    ) TargetRange
    WHERE
        @workingDate BETWEEN StartDate AND NextEndDate;

    SELECT @windowStartDate = COALESCE(@windowStartDate, @startDate)
    SELECT @windowEndDate = COALESCE(@windowEndDate, @workingDate)

    SELECT
        @returnValue = MIN(dates.DATE)
    FROM
        dbo.ufn_GetListOfCalendarDates (@windowStartDate, @windowEndDate) dates
            left outer join core.NonWorkingDay nwd on
            (
                nwd.Date = dates.Date
            )
    WHERE
        dates.Date >= @StartDate AND
        DayOfWeekIndex NOT IN (1, 7) AND
        nwd.Id is null
    OPTION (MAXRECURSION 2000)

	-- Return the result of the function
	RETURN @returnValue

END
