using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Log;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class QuikLog
{

    /// <summary>
    /// Gets or sets Url.
    /// </summary>
    [JsonPropertyName("url")] public string Url { get; set; } = "";

    /// <summary>
    /// Gets or sets StatusCode.
    /// </summary>
    [JsonPropertyName("status_code")] public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets Request.
    /// </summary>
    [JsonPropertyName("request")] public object? Request { get; set; }

    /// <summary>
    /// Gets or sets Response.
    /// </summary>
    [JsonPropertyName("response")] public object? Response { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }
}