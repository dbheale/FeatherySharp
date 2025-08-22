using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.User;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UserSession
{

    /// <summary>
    /// Gets or sets AuthId.
    /// </summary>
    [JsonPropertyName("auth_id")] public string AuthId { get; set; } = "";

    /// <summary>
    /// Gets or sets InternalId.
    /// </summary>
    [JsonPropertyName("internal_id")] public string InternalId { get; set; } = "";

    /// <summary>
    /// Gets or sets Forms.
    /// </summary>
    [JsonPropertyName("forms")] public List<UserFormSession> Forms { get; set; } = new();
}