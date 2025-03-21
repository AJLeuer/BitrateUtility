using System.Diagnostics.CodeAnalysis;
using System.Net;
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
    string state,
    [property: JsonConverter(typeof(IPAddressConverter))]
    IPAddress ipAddress,
    [property: JsonConverter(typeof(PhysicalAddressConverter))]
    PhysicalAddress macAddress,
    long uptimeSec,
    DateTime lastHeartbeatAt,
    DateTime nextHeartbeatAt,
    DateTime provisionedAt,
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