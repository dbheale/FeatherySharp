using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Workspace;

/// <summary>
/// Represents the class.
/// </summary>
public class WorkspaceSummary
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = "";
}