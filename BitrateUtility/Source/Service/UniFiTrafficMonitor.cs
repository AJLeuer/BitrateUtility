using System.Text.Json;
using BitrateUtility.Source.Client;

namespace BitrateUtility.Source.Service;

public class UniFiTrafficMonitor
{
    private readonly UnifiAPIClient unifiClient;

    public UniFiTrafficMonitor(UnifiAPIClient unifiClient)
    {
        this.unifiClient  = unifiClient;
    }
    
    public async Task RunAsync()
    {
        Console.WriteLine(value: "Checking connection to UniFi...");
        var info = await unifiClient.RetrieveApplicationInfo();
        var sites = await unifiClient.RetrieveSites();
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        Console.WriteLine(value: JsonSerializer.Serialize(info, options));
        Console.WriteLine(value: JsonSerializer.Serialize(sites, options));
    }
}