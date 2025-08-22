using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.User;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UserField
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets Hidden.
    /// </summary>
    [JsonPropertyName("hidden")] public bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets Type.
    /// </summary>
    [JsonPropertyName("type")] public string? Type { get; set; }

    /// <summary>
    /// Gets or sets DisplayText.
    /// </summary>
    [JsonPropertyName("display_text")] public string? DisplayText { get; set; }

    /// <summary>
    /// Gets or sets Value.
    /// </summary>
    [JsonPropertyName("value")] public object? Value { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets InternalId.
    /// </summary>
    [JsonPropertyName("internal_id")] public string? InternalId { get; set; }
}