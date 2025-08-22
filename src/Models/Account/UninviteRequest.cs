using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Account;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UninviteRequest
{

    /// <summary>
    /// Gets or sets Email.
    /// </summary>
    [JsonPropertyName("email")] public string? Email { get; set; }

    /// <summary>
    /// Gets or sets AccountId.
    /// </summary>
    [JsonPropertyName("account_id")] public string? AccountId { get; set; }
}