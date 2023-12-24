namespace Quesify.SharedKernel.AspNetCore.HttpFeatures;

public interface IHttpRequestTimeFeature
{
    DateTime RequestDate { get; }
}
