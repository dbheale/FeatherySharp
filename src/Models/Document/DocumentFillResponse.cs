using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Document;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class DocumentFillResponse
{

    /// <summary>
    /// Gets or sets FileUrl.
    /// </summary>
    [JsonPropertyName("file_url")] public string FileUrl { get; set; } = "";
}