using Microsoft.Extensions.Configuration;

namespace BitrateUtility.Source.Configuration;

public static class Configuration
{
    public const string GatewayURL = "https://192.168.1.1";
    public static string APIKey { get; private set; }

    static Configuration()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        string? uniFiAPIKey = configuration[Constants.UniFiAPIKeySecretKey];
        APIKey = uniFiAPIKey!;
    }
}