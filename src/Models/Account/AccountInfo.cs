using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Account;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class AccountInfo
{

    /// <summary>
    /// Gets or sets Team.
    /// </summary>
    [JsonPropertyName("team")] public string Team { get; set; } = "";

    /// <summary>
    /// Gets or sets Accounts.
    /// </summary>
    [JsonPropertyName("accounts")] public List<AccountEntry> Accounts { get; set; } = new();
}