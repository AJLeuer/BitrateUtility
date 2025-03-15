using BitrateUtility.Source.Client;
using BitrateUtility.Source.Client.Utility;
using BitrateUtility.Source.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BitrateUtility.Source;

public class UniFiTrafficMonitorApplication
{
    private UniFiTrafficMonitor? UniFiTrafficMonitor { get; }
    private ServiceProvider ServiceProvider { get; }
    private ServiceCollection ServiceCollection { get; } = new();
    private HttpClientHandler HTTPHandler { get; } = new()
    {
        ServerCertificateCustomValidationCallback = (_, _, _, _) => true
    };
    private LoggingHandler LoggingHandler { get; }

    public UniFiTrafficMonitorApplication()
    {
        LoggingHandler = new LoggingHandler(innerHandler: HTTPHandler);
        ServiceCollection.AddHttpClient<UniFiAPIClient>().ConfigurePrimaryHttpMessageHandler(() => LoggingHandler);
        ServiceCollection.AddTransient<UniFiTrafficMonitor>();
        ServiceProvider = ServiceCollection.BuildServiceProvider();
        UniFiTrafficMonitor = ServiceProvider.GetService<UniFiTrafficMonitor>();
    }
    
    public async Task Run()
    {
        await (UniFiTrafficMonitor?.Run() ?? Task.CompletedTask);
    }
}