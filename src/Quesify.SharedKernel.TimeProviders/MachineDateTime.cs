namespace Quesify.SharedKernel.Utilities.TimeProviders;

public class MachineDateTime : DateTimeBase
{
    public override DateTime Now => DateTime.Now;

    public override TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
}
