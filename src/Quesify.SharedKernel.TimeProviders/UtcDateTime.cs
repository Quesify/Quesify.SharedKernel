namespace Quesify.SharedKernel.Utilities.TimeProviders;

public class UtcDateTime : DateTimeBase
{
    public override DateTime Now => DateTime.UtcNow;

    public override TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Utc;
}
