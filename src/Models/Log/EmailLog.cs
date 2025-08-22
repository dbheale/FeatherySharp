using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Log;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class EmailLog
{

    /// <summary>
    /// Gets or sets TemplateId.
    /// </summary>
    [JsonPropertyName("template_id")] public string TemplateId { get; set; } = "";

    /// <summary>
    /// Gets or sets Recipients.
    /// </summary>
    [JsonPropertyName("recipients")] public List<string> Recipients { get; set; } = new();

    /// <summary>
    /// Gets or sets Subject.
    /// </summary>
    [JsonPropertyName("subject")] public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }
}