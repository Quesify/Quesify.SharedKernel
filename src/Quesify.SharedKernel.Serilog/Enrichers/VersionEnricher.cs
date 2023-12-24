using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace Quesify.SharedKernel.Serilog.Enrichers;

public class VersionEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var version = propertyFactory.CreateProperty("Version", Assembly.GetEntryAssembly()?.GetName().Version?.ToString());
        logEvent.AddOrUpdateProperty(version);
    }
}
