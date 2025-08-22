using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Submission;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class CreateOrUpdateSubmissionResponse
{

    /// <summary>
    /// Gets or sets Fields.
    /// </summary>
    [JsonPropertyName("fields")] public Dictionary<string, object> Fields { get; set; } = new();

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user_id")] public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets Forms.
    /// </summary>
    [JsonPropertyName("forms")] public List<string>? Forms { get; set; }

    /// <summary>
    /// Gets or sets Complete.
    /// </summary>
    [JsonPropertyName("complete")] public bool? Complete { get; set; }
}