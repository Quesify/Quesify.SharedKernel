using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.SharedKernel.AspNetCore.HttpFeatures;

public class HttpRequestTimeFeature : IHttpRequestTimeFeature
{
    public DateTime RequestDate { get; }

    public HttpRequestTimeFeature(IDateTime dateTime)
    {
        RequestDate = dateTime.Now;
    }
}
