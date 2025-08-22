using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public class CreateFormResponse
{

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets Active.
    /// </summary>
    [JsonPropertyName("active")] public bool Active { get; set; }

    /// <summary>
    /// Gets or sets CreatedAt.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets UpdatedAt.
    /// </summary>
    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}