using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.AI;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class AiRunDatum
{

    /// <summary>
    /// Gets or sets FieldInternalId.
    /// </summary>
    [JsonPropertyName("field_internal_id")] public string FieldInternalId { get; set; } = "";

    /// <summary>
    /// Gets or sets Value.
    /// </summary>
    [JsonPropertyName("value")] public object? Value { get; set; }
}