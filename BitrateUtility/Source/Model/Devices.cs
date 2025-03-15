using System.Diagnostics.CodeAnalysis;

namespace BitrateUtility.Source.Model;

[SuppressMessage("ReSharper", "InconsistentNaming"), SuppressMessage("ReSharper", "UnusedMember.Global"), SuppressMessage("ReSharper", "CollectionNeverQueried.Global")]
public class Devices : UniFiResponse
{
    public new required Data[] data { get; set; }

    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global"), SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public record Data(
        string model,
        string macAddress,
        string ipAddress,
        string state,
        string[] features,
        string[] interfaces
    ) : BitrateUtility.Source.Model.Data;
}

