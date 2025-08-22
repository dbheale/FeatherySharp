using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Account;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class RotateKeyResponse
{

    /// <summary>
    /// Gets or sets OldApiKey.
    /// </summary>
    [JsonPropertyName("old_api_key")] public string OldApiKey { get; set; } = "";

    /// <summary>
    /// Gets or sets NewApiKey.
    /// </summary>
    [JsonPropertyName("new_api_key")] public string NewApiKey { get; set; } = "";
}