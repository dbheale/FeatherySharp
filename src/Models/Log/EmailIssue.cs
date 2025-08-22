using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Log;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class EmailIssue
{

    /// <summary>
    /// Gets or sets EventType.
    /// </summary>
    [JsonPropertyName("event_type")] public string EventType { get; set; } = "";

    /// <summary>
    /// Gets or sets Recipients.
    /// </summary>
    [JsonPropertyName("recipients")] public List<string> Recipients { get; set; } = new();

    /// <summary>
    /// Gets or sets RejectedRecipients.
    /// </summary>
    [JsonPropertyName("rejected_recipients")] public List<string>? RejectedRecipients { get; set; }

    /// <summary>
    /// Gets or sets Subject.
    /// </summary>
    [JsonPropertyName("subject")] public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets Sender.
    /// </summary>
    [JsonPropertyName("sender")] public string? Sender { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }
}