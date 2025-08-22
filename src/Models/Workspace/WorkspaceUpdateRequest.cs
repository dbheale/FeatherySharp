using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Workspace;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class WorkspaceUpdateRequest
{

    /// <summary>
    /// Gets or sets BrandUrl.
    /// </summary>
    [JsonPropertyName("brand_url")] public string? BrandUrl { get; set; }

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; }
}