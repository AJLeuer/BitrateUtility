using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;
using BitrateUtility.Source.Configuration;
using BitrateUtility.Source.Model;

namespace BitrateUtility.Source.Client;

/// <summary>
/// A client that handles all communication with the UniFi Controller
/// (logging in, fetching DPI stats, etc.).
/// </summary>
public class UniFiAPIClient
{
    private static class Endpoints
    {
        private const string Prefix = "proxy/network/integrations";
        public static readonly Uri ApplicationInfo = new ($"{Prefix}/v1/info", UriKind.Relative);
        public static readonly Uri Sites = new ($"{Prefix}/v1/sites", UriKind.Relative);
        private static readonly string devices = $"{Prefix}/v1/sites/{{siteID}}/devices";
        private static readonly string statistics = $"{Prefix}/v1/sites/{{siteID}}/devices/{{deviceID}}/statistics/latest";
        
        public static Uri Devices(Guid siteID)
        {
            string statisticsEndpoint = devices.Replace("{siteID}", siteID.ToString());
            return new Uri(statisticsEndpoint, UriKind.Relative);
        }
        
        public static Uri Statistics(Guid siteID, Guid deviceID)
        {
            string statisticsEndpoint = statistics
                .Replace("{siteID}", siteID.ToString())
                .Replace("{deviceID}", deviceID.ToString());

            return new Uri(statisticsEndpoint, UriKind.Relative);
        }
    }

    private readonly HttpClient httpClient;

    public UniFiAPIClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: MediaTypeNames.Application.Json));
        this.httpClient.DefaultRequestHeaders.Add(name: Constants.APIKeyHeaderKey, value: DomainConfiguration.APIKey);
    }
    
    public async Task<JsonNode?> RetrieveApplicationInfo()
    {
        var applicationInfoURL = new Uri(baseUri: DomainConfiguration.GatewayURL, relativeUri: Endpoints.ApplicationInfo);

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: applicationInfoURL);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to connect to UniFi. Status: {response.StatusCode}");
        }

        JsonNode? applicationInfo = JsonNode.Parse(json: await response.Content.ReadAsStringAsync());
        return applicationInfo;
    }
    
    public async Task<Sites?> RetrieveSites()
    {
        var sitesURL = new Uri(baseUri: DomainConfiguration.GatewayURL, relativeUri: Endpoints.Sites);

        HttpResponseMessage response = await httpClient.GetAsync(requestUri: sitesURL);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to connect to UniFi. Status: {response.StatusCode}");
        }

        return await JsonSerializer.DeserializeAsync<Sites>(utf8Json: await response.Content.ReadAsStreamAsync());
    }
    
    public async Task<Devices?> RetrieveDevices(Guid siteID)
    {
        var devicesURL = new Uri(baseUri: DomainConfiguration.GatewayURL, relativeUri: Endpoints.Devices(siteID: siteID));
        
        HttpResponseMessage response = await httpClient.GetAsync(requestUri: devicesURL);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to connect to UniFi. Status: {response.StatusCode}");
        }

        return await JsonSerializer.DeserializeAsync<Devices>(utf8Json: await response.Content.ReadAsStreamAsync());
    }
}