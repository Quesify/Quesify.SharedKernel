namespace Quesify.SharedKernel.Utilities.TimeZones;

public interface ITimeZoneService
{
    TimeZoneInfo ConvertTimeZoneById(string timeZoneId);
}
