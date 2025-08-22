using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class FormSummary
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets Active.
    /// </summary>
    [JsonPropertyName("active")] public bool Active { get; set; }

    /// <summary>
    /// Gets or sets Tags.
    /// </summary>
    [JsonPropertyName("tags")] public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets UpdatedAt.
    /// </summary>
    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets InternalId.
    /// </summary>
    [JsonPropertyName("internal_id")] public string? InternalId { get; set; }
}