using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Account;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class EditAccountResponse
{

    /// <summary>
    /// Gets or sets Email.
    /// </summary>
    [JsonPropertyName("email")] public string? Email { get; set; }

    /// <summary>
    /// Gets or sets Role.
    /// </summary>
    [JsonPropertyName("role")] public string? Role { get; set; }

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string? Id { get; set; }
}