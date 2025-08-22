using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Submission;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class SubmissionRecord
{

    /// <summary>
    /// Gets or sets Values.
    /// </summary>
    [JsonPropertyName("values")] public List<SubmissionValue> Values { get; set; } = new();

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user_id")] public string UserId { get; set; } = "";

    /// <summary>
    /// Gets or sets SubmissionStart.
    /// </summary>
    [JsonPropertyName("submission_start")] public DateTimeOffset? SubmissionStart { get; set; }

    /// <summary>
    /// Gets or sets SubmissionEnd.
    /// </summary>
    [JsonPropertyName("submission_end")] public DateTimeOffset? SubmissionEnd { get; set; }
}