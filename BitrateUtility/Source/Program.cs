using BitrateUtility.Source.Client;
using BitrateUtility.Source.Client.Utility;
using BitrateUtility.Source.Service;

namespace BitrateUtility.Source;

class Program
{
    public static async Task Main()
    {
        HttpClientHandler httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };

        var loggingHandler = new LoggingHandler(innerHandler: httpHandler);
        
        var httpClient = new HttpClient(handler: loggingHandler);

        var unifiClient = new UnifiAPIClient(httpClient: httpClient);

        var monitor = new UniFiTrafficMonitor(unifiClient: unifiClient);

        await monitor.RunAsync();
    }
}