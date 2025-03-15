using System.Text.Json;
using System.Text.Json.Nodes;
using BitrateUtility.Source.Client;
using static BitrateUtility.Source.Configuration.ApplicationConfiguration;

namespace BitrateUtility.Source.Service;

public class UniFiTrafficMonitor
{
    private readonly UniFiAPIClient uniFiClient;

    public UniFiTrafficMonitor(UniFiAPIClient uniFiClient)
    {
        this.uniFiClient  = uniFiClient;
    }
    
    public async Task Run()
    {
        Console.WriteLine(value: "Checking connection to UniFi...");
        JsonNode? info = await uniFiClient.RetrieveApplicationInfo();
        JsonNode? sites = await uniFiClient.RetrieveSites();
        
        Console.WriteLine(value: JsonSerializer.Serialize(info, DefaultJSONSerializerOptions));
        Console.WriteLine(value: JsonSerializer.Serialize(sites, DefaultJSONSerializerOptions));
    }
}