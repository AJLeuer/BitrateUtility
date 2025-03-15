using System.Text.Json;
using System.Text.Json.Nodes;
using BitrateUtility.Source.Client;
using BitrateUtility.Source.Model;
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
        Sites? sites = await uniFiClient.RetrieveSites();
        Devices? devices = null;
        
        if (sites?.data[0].id is { } siteID)
        {
            devices = await uniFiClient.RetrieveDevices(siteID: siteID);
        }
        
        Console.WriteLine(value: "UniFi application info:");
        Console.WriteLine(value: JsonSerializer.Serialize(info, DefaultJSONSerializerOptions));
        Console.WriteLine(value: "Sites info:");
        Console.WriteLine(value: JsonSerializer.Serialize(sites, DefaultJSONSerializerOptions));
        Console.WriteLine(value: "Devices info:");
        Console.WriteLine(value: JsonSerializer.Serialize(devices, DefaultJSONSerializerOptions));
    }
}
