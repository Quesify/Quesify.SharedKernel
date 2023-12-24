using Quesify.SharedKernel.Utilities.TimeProviders;
using Serilog.Core;
using Serilog.Events;

namespace Quesify.SharedKernel.Serilog.Enrichers;

public class ExecutionDurationEnricher : ILogEventEnricher
{
    private readonly IDateTime _dateTime;

    public ExecutionDurationEnricher(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.TryGetValue("RequestExecutionTime", out LogEventPropertyValue? value) &&
            value is ScalarValue scalarValue &&
            scalarValue.Value is DateTime rawValue)
        {
            var elapsedMilliseconds = (_dateTime.Now - rawValue).TotalMilliseconds;
            var property = propertyFactory.CreateProperty("ExecutionDuration", elapsedMilliseconds);
            logEvent.AddOrUpdateProperty(property);
        }
    }
}
