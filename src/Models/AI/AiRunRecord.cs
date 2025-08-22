using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.AI;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class AiRunRecord
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user_id")] public string UserId { get; set; } = "";

    /// <summary>
    /// Gets or sets FileName.
    /// </summary>
    [JsonPropertyName("file_name")] public string FileName { get; set; } = "";

    /// <summary>
    /// Gets or sets Success.
    /// </summary>
    [JsonPropertyName("success")] public bool Success { get; set; }

    /// <summary>
    /// Gets or sets Approved.
    /// </summary>
    [JsonPropertyName("approved")] public bool? Approved { get; set; }

    /// <summary>
    /// Gets or sets Approver.
    /// </summary>
    [JsonPropertyName("approver")] public string? Approver { get; set; }

    /// <summary>
    /// Gets or sets EmailExtractedAt.
    /// </summary>
    [JsonPropertyName("email_extracted_at")] public DateTimeOffset? EmailExtractedAt { get; set; }

    /// <summary>
    /// Gets or sets DocumentExtractedAt.
    /// </summary>
    [JsonPropertyName("document_extracted_at")] public DateTimeOffset? DocumentExtractedAt { get; set; }

    /// <summary>
    /// Gets or sets Data.
    /// </summary>
    [JsonPropertyName("data")] public List<AiRunDatum>? Data { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets UpdatedAt.
    /// </summary>
    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}