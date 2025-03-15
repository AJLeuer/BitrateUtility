using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitrateUtility.Source.Model;

[SuppressMessage("ReSharper", "InconsistentNaming"), SuppressMessage("ReSharper", "UnusedMember.Global"), SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class UniFiResponse
{
    public int offset { get; set; } 
    public int limit { get; set; }
    public int count { get; set; }
    public int totalCount { get; set; }
    public Data[] data { get; set; } = null!;

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

[SuppressMessage("ReSharper", "InconsistentNaming"), SuppressMessage("ReSharper", "UnusedMember.Global"), SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public record Data
{
    public required Guid id { get; set; }
    public required string name { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

