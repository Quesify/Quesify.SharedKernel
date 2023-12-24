namespace System;

public static class DateTimeExtensions
{
    public static DateTime FirstDayOfTheMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }

    public static DateTime LastDayOfTheMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    }

    public static DateOnly ToDateOnly(this DateTime date)
    {
        return DateOnly.FromDateTime(date);
    }
}
