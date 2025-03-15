using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace BitrateUtility.Source.Configuration;

public static class DomainConfiguration
{
    public const string GatewayURL = "https://192.168.1.1";
    public static string APIKey { get; private set; }

    static DomainConfiguration()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        string? uniFiAPIKey = configuration[Constants.UniFiAPIKeySecretKey];
        APIKey = uniFiAPIKey!;
    }
}