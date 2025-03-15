using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json.Nodes;

namespace BitrateUtility;

/// <summary>
/// A client that handles all communication with the UniFi Controller
/// (logging in, fetching DPI stats, etc.).
/// </summary>
public class UnifiAPIClient
{
    private static readonly string ApplicationInfoEndpoint = "proxy/network/integrations/v1/info";
    private static readonly string SitesEndpoint = "proxy/network/integrations/v1/sites";
    private static readonly string StatisticsEndpoint = "proxy/network/integrations/v1/sites/{siteId}/devices/{deviceId}/statistics/latest";

    private readonly HttpClient    httpClient;

    public UnifiAPIClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        
        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: MediaTypeNames.Application.Json));
        this.httpClient.DefaultRequestHeaders.Add(name: "X-API-KEY", value: Configuration.APIKey);
    }


    public async Task<JsonNode?> RetrieveApplicationInfo()
    {
        var applicationInfoURL = $"{Configuration.GatewayURL}/{ApplicationInfoEndpoint}";

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: applicationInfoURL);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to connect to UniFi. Status: {response.StatusCode}");
        }

        JsonNode? applicationInfo = JsonNode.Parse(json: await response.Content.ReadAsStringAsync());
        return applicationInfo;
    }
    
    public async Task<JsonNode?> RetrieveSites()
    {
        var applicationInfoURL = $"{Configuration.GatewayURL}/{SitesEndpoint}";

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: applicationInfoURL);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to connect to UniFi. Status: {response.StatusCode}");
        }

        JsonNode? sitesData = JsonNode.Parse(json: await response.Content.ReadAsStringAsync());
        return sitesData;
    }
    
    public async Task<JsonNode?> RetrieveStatistics()
    {
        var applicationInfoURL = $"{Configuration.GatewayURL}/{StatisticsEndpoint}";

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: applicationInfoURL);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to Retrieve Statistics. Status: {response.StatusCode}");
        }

        JsonNode? applicationInfo = JsonNode.Parse(json: await response.Content.ReadAsStringAsync());
        return applicationInfo;
    }
}