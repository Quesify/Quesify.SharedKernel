﻿using Serilog.Core;
using Serilog.Events;

namespace Quesify.SharedKernel.Serilog.Enrichers;

public class IdLogEventEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var id = propertyFactory.CreateProperty("Id", Guid.NewGuid());
        logEvent.AddOrUpdateProperty(id);
    }
}
