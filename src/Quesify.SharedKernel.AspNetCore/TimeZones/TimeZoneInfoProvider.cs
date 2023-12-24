using Quesify.SharedKernel.AspNetCore.Constants;
using Quesify.SharedKernel.Utilities.TimeZones;

namespace Quesify.SharedKernel.AspNetCore.TimeZones;

public class TimeZoneInfoProvider : ITimeZoneInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITimeZoneService _timeZoneService;

    public TimeZoneInfoProvider(
        IHttpContextAccessor httpContextAccessor,
        ITimeZoneService timeZoneService)
    {
        _httpContextAccessor = httpContextAccessor;
        _timeZoneService = timeZoneService;
    }

    public TimeZoneInfo Get()
    {
        var timeZoneIdentifier = _httpContextAccessor.HttpContext?.Request.GetHeader(HeaderConstants.TimeZoneIdentifier);
        return timeZoneIdentifier == null
            ? TimeZoneInfo.Local
            : _timeZoneService.ConvertTimeZoneById(timeZoneIdentifier);
    }
}
