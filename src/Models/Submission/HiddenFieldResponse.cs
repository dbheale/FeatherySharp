using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Submission;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class HiddenFieldResponse
{

    /// <summary>
    /// Gets or sets FieldId.
    /// </summary>
    [JsonPropertyName("field_id")] public string FieldId { get; set; } = "";
}