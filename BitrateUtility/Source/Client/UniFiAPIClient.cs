using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json.Nodes;
using BitrateUtility.Source.Configuration;

namespace BitrateUtility.Source.Client;

/// <summary>
/// A client that handles all communication with the UniFi Controller
/// (logging in, fetching DPI stats, etc.).
/// </summary>
public class UniFiAPIClient
{
    private static class Endpoints
    {
        public static readonly string ApplicationInfo = "proxy/network/integrations/v1/info";
        public static readonly string Sites = "proxy/network/integrations/v1/sites";
        private static readonly string statistics = "proxy/network/integrations/v1/sites/{siteID}/devices/{deviceID}/statistics/latest";
        
        public static string Statistics(string siteID, string deviceID)
        {
            string statisticsEndpoint = statistics
                .Replace("{siteID}", siteID)
                .Replace("{deviceID}", deviceID);

            return statisticsEndpoint;
        }
    }

    private readonly HttpClient httpClient;

    public UniFiAPIClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: MediaTypeNames.Application.Json));
        this.httpClient.DefaultRequestHeaders.Add(name: Constants.APIKeyHeaderKey, value: Configuration.DomainConfiguration.APIKey);
    }
    
    public async Task<JsonNode?> RetrieveApplicationInfo()
    {
        var applicationInfoURL = new Uri($"{Configuration.DomainConfiguration.GatewayURL}/{Endpoints.ApplicationInfo}");

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
        var applicationInfoURL = new Uri($"{Configuration.DomainConfiguration.GatewayURL}/{Endpoints.Sites}");

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
        var applicationInfoURL = new Uri($"{Configuration.DomainConfiguration.GatewayURL}/{Endpoints.Statistics("", "")}");

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: applicationInfoURL);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to Retrieve Statistics. Status: {response.StatusCode}");
        }

        JsonNode? applicationInfo = JsonNode.Parse(json: await response.Content.ReadAsStringAsync());
        return applicationInfo;
    }
}