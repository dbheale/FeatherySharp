using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.User;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UserCreatedOrFetched
{

    /// <summary>
    /// Gets or sets SdkKey.
    /// </summary>
    [JsonPropertyName("sdk_key")] public string SdkKey { get; set; } = "";

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets UpdatedAt.
    /// </summary>
    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}