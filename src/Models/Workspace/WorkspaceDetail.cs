using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Workspace;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class WorkspaceDetail : WorkspaceSummary
{

    /// <summary>
    /// Gets or sets BrandUrl.
    /// </summary>
    [JsonPropertyName("brand_url")] public string? BrandUrl { get; set; }

    /// <summary>
    /// Gets or sets ApiKey.
    /// </summary>
    [JsonPropertyName("api_key")] public string? ApiKey { get; set; } // doc: includes keys for white-label queries
}