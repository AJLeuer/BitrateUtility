using System.Text.Json;

namespace BitrateUtility.Source.Configuration;

public static class ApplicationConfiguration
{
    public static JsonSerializerOptions DefaultJSONSerializerOptions { get; } = new JsonSerializerOptions
    {
        WriteIndented = true
    };
}