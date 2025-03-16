using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitrateUtility.Source.Model;

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