using System.Net;
using System.Text.Json;

namespace BitrateUtility;

public class UniFiTrafficMonitor
{
    private readonly UnifiAPIClient unifiClient;

    public UniFiTrafficMonitor(UnifiAPIClient unifiClient)
    {
        this.unifiClient  = unifiClient;
    }

    /// <summary>
    /// One-shot method: logs into UniFi, grabs Netflix byte counts twice,
    /// waits the configured interval between counts, then prints an approximate bitrate.
    /// </summary>
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