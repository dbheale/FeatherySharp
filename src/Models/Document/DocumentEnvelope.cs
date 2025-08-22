using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Document;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class DocumentEnvelope
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets DocumentId.
    /// </summary>
    [JsonPropertyName("document")] public string? DocumentId { get; set; }

    /// <summary>
    /// Gets or sets UserId.
    /// </summary>
    [JsonPropertyName("user")] public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets Signer.
    /// </summary>
    [JsonPropertyName("signer")] public string? Signer { get; set; }

    /// <summary>
    /// Gets or sets Sender.
    /// </summary>
    [JsonPropertyName("sender")] public string? Sender { get; set; }

    /// <summary>
    /// Gets or sets FileUrl.
    /// </summary>
    [JsonPropertyName("file")] public string? FileUrl { get; set; }

    /// <summary>
    /// Gets or sets Type.
    /// </summary>
    [JsonPropertyName("type")] public string? Type { get; set; }

    /// <summary>
    /// Gets or sets Viewed.
    /// </summary>
    [JsonPropertyName("viewed")] public bool? Viewed { get; set; }

    /// <summary>
    /// Gets or sets Signed.
    /// </summary>
    [JsonPropertyName("signed")] public bool? Signed { get; set; }

    /// <summary>
    /// Gets or sets Tags.
    /// </summary>
    [JsonPropertyName("tags")] public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }
}