using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitrateUtility.Source.Model;

[SuppressMessage("ReSharper", "InconsistentNaming"), 
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public record Device(
    long uptimeSec,
    DateTime lastHeartbeatAt,
    DateTime nextHeartbeatAt,
    double loadAverage1Min,
    double loadAverage5Min,
    double loadAverage15Min,
    double cpuUtilizationPct,
    double memoryUtilizationPct,
    Uplink uplink,
    Interfaces interfaces)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

[SuppressMessage("ReSharper", "InconsistentNaming"),
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Uplink(
    long txRateBps,
    long rxRateBps
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

[SuppressMessage("ReSharper", "InconsistentNaming"),
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Interfaces(
    Radios[] radios
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

[SuppressMessage("ReSharper", "InconsistentNaming"),
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Radios(
    decimal frequencyGHz,
    double txRetriesPct
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

