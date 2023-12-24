using TimeZoneConverter;

namespace Quesify.SharedKernel.Utilities.TimeZones;

public class TimeZoneConverterService : ITimeZoneService
{
    public TimeZoneInfo ConvertTimeZoneById(string timeZoneId)
    {
        return TZConvert.GetTimeZoneInfo(timeZoneId); 
    }
}
