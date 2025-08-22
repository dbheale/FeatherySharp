using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.AI;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class AiRunResponse
{

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user_id")] public string UserId { get; set; } = "";
}