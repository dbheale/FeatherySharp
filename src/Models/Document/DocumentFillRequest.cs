using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Document;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class DocumentFillRequest
{

    /// <summary>
    /// Gets or sets DocumentId.
    /// </summary>
    [JsonPropertyName("document")] public string DocumentId { get; set; } = "";

    /// <summary>
    /// Gets or sets FieldValues.
    /// </summary>
    [JsonPropertyName("field_values")] public Dictionary<string, object>? FieldValues { get; set; }

    /// <summary>
    /// Gets or sets SignerEmail.
    /// </summary>
    [JsonPropertyName("signer_email")] public string? SignerEmail { get; set; }

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
}