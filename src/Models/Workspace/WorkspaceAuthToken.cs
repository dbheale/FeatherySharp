using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Workspace;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class WorkspaceAuthToken
{

    /// <summary>
    /// Gets or sets AccountId.
    /// </summary>
    [JsonPropertyName("account_id")] public string AccountId { get; set; } = "";

    /// <summary>
    /// Gets or sets Token.
    /// </summary>
    [JsonPropertyName("token")] public string Token { get; set; } = "";
}