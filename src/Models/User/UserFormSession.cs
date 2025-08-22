using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.User;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UserFormSession
{

    /// <summary>
    /// Gets or sets CurrentStepId.
    /// </summary>
    [JsonPropertyName("current_step_id")] public string? CurrentStepId { get; set; }

    /// <summary>
    /// Gets or sets CompletedAt.
    /// </summary>
    [JsonPropertyName("completed_at")] public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets TrackLocation.
    /// </summary>
    [JsonPropertyName("track_location")] public bool TrackLocation { get; set; }
}