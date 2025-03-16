using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.Json.Serialization;
using BitrateUtility.Source.Model.Serialization;

namespace BitrateUtility.Source.Model;

[SuppressMessage("ReSharper", "InconsistentNaming"), 
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Device(
    Guid id,
    string name,
    string model,
    bool supported,
    [property: JsonConverter(typeof(PhysicalAddressConverter))]
    PhysicalAddress macAddress,
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
    Radio[] radios,
    Port[] ports
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

[SuppressMessage("ReSharper", "InconsistentNaming"),
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Radio(
    decimal frequencyGHz,
    double txRetriesPct
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}

public record Port(
    int idx,
    Port.State state,
    string connector,
    int maxSpeedMbps,
    int speedMbps
)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum State
    {
        UP,
        DOWN
    }
}
