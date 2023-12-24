namespace Quesify.SharedKernel.EventBus;

public class EventBusOptions
{
    public string ProjectName { get; set; }

    public string ServiceName { get; set; }

    public string EventNamePrefixToRemove { get; set; }

    public string EventNameSuffixToRemove { get; set; }

    public EventBusOptions()
    {
        ProjectName = string.Empty;
        ServiceName = string.Empty;
        EventNamePrefixToRemove = string.Empty;
        EventNameSuffixToRemove = "IntegrationEvent";
    }
}
