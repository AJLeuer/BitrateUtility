using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitrateUtility.Source.Model;

[SuppressMessage("ReSharper", "InconsistentNaming"), 
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Statistics(
    long uptimeSec,
    string lastHeartbeatAt,
    string nextHeartbeatAt,
    double loadAverage1Min,
    double loadAverage5Min,
    double loadAverage15Min,
    double cpuUtilizationPct,
    double memoryUtilizationPct,
    Uplink uplink,
    Interfaces interfaces
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
}



