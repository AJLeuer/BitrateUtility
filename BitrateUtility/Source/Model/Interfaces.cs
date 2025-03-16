using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitrateUtility.Source.Model;

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

[SuppressMessage("ReSharper", "InconsistentNaming"),
 SuppressMessage("ReSharper", "UnusedMember.Global"),
 SuppressMessage("ReSharper", "CollectionNeverUpdated.Global"),
 SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record Port(
    int idx,
    Port.State state,
    string connector,
    int maxSpeedMbps,
    int speedMbps
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum State
    {
        UP,
        DOWN
    }
}