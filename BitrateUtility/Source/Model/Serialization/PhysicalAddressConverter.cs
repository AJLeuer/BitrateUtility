namespace BitrateUtility.Source.Model.Serialization;

using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PhysicalAddressConverter : JsonConverter<PhysicalAddress>
{
    public override PhysicalAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the string value from JSON.
        string? macString = reader.GetString();
        if (string.IsNullOrWhiteSpace(macString))
            return null;

        // Remove delimiters such as colons, dashes, or periods.
        string cleaned = macString.Replace(":", string.Empty)
            .Replace("-", string.Empty)
            .Replace(".", string.Empty);
        
        if (cleaned.Length % 2 != 0)
            throw new JsonException("Invalid MAC address format.");

        byte[] bytes = new byte[cleaned.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = byte.Parse(cleaned.Substring(i * 2, 2), NumberStyles.HexNumber);
        }
        return new PhysicalAddress(bytes);
    }

    public override void Write(Utf8JsonWriter writer, PhysicalAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
