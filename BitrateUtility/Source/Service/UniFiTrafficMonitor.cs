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
        DeviceList? devices = null;
        Device? device = null;
        
        if (sites?.data[0].id is { } siteID)
        {
            devices = await uniFiClient.RetrieveDevices(siteID: siteID);
            if (devices?.data[0].id is { } deviceID)
            {
                device = await uniFiClient.RetrieveDevice(siteID: siteID, deviceID: deviceID);
            }
        }
        
        Console.WriteLine(value: "UniFi application info:");
        Console.WriteLine(value: JsonSerializer.Serialize(info, DefaultJSONSerializerOptions));
        Console.WriteLine();
        Console.WriteLine();
        
        Console.WriteLine(value: "Sites info:");
        Console.WriteLine(value: JsonSerializer.Serialize(sites, DefaultJSONSerializerOptions));
        Console.WriteLine();
        Console.WriteLine();
        
        
        Console.WriteLine(value: "Devices info:");
        Console.WriteLine(value: JsonSerializer.Serialize(devices, DefaultJSONSerializerOptions));
        Console.WriteLine();
        Console.WriteLine();
        
        Console.WriteLine(value: "Primary device info:");
        Console.WriteLine(value: JsonSerializer.Serialize(device, DefaultJSONSerializerOptions));
        Console.WriteLine();
        Console.WriteLine();
    }
}
