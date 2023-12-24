using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.TimeProviders;
using Quesify.SharedKernel.Utilities.TimeProviders;
using Quesify.SharedKernel.Utilities.TimeZones;
using Xunit;

namespace Quesify.SharedKernel.TimerProviders.Tests;

public class MachineDateTimeProvidersTests
{
    public IServiceCollection Services { get; set; }

    public MachineDateTimeProvidersTests()
    {
        Services = new ServiceCollection();
        Services.AddTimeProvider(options =>
        {
            options.UseMachineDateTime(Services);
        });
        Services.AddSingleton<ITimeZoneService, TimeZoneConverterService>();
    }

    [Fact]
    public void Now_ShouldReturnMachineDateTime()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var dateTime = serviceProvider.GetRequiredService<IDateTime>();
        Assert.Equal(dateTime.Now.ToString("dd/MM/yyyy HHmm"), DateTime.Now.ToString("dd/MM/yyyy HHmm"));
    }

    [Fact]
    public void TimeZoneInfo_ShouldReturnMachineTimeZoneInfo()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var dateTime = serviceProvider.GetRequiredService<IDateTime>();
        Assert.Equal(dateTime.TimeZoneInfo.Id, TimeZoneInfo.Local.Id);
    }

    [Fact]
    public void ConvertToSource_ShouldConvertToSourceDateTimeBySourceTimeZoneInfo()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var dateTime = serviceProvider.GetRequiredService<IDateTime>();
        var timeZoneService = serviceProvider.GetRequiredService<ITimeZoneService>();
        var sourceTimeZoneInfo = timeZoneService.ConvertTimeZoneById("UTC");
        Assert.Equal(
            dateTime.ConvertToSource(DateTime.Now, sourceTimeZoneInfo).ToString("dd/MM/yyyy HHmm"),
            DateTime.UtcNow.ToString("dd/MM/yyyy HHmm")
        );
    }

    [Fact]
    public void ConvertToSource_ShouldConvertToMachineDateTimeBySourceTimeZoneInfo()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var dateTime = serviceProvider.GetRequiredService<IDateTime>();
        var timeZoneService = serviceProvider.GetRequiredService<ITimeZoneService>();
        var sourceTimeZoneInfo = timeZoneService.ConvertTimeZoneById("UTC");
        Assert.Equal(
            dateTime.ConvertFromSource(DateTime.UtcNow, sourceTimeZoneInfo).ToString("dd/MM/yyyy HHmm"),
            DateTime.Now.ToString("dd/MM/yyyy HHmm")
        );
    }
}
