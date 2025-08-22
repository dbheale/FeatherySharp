using System.Text.Json;
using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UpdateFormRequest
{

    /// <summary>
    /// Gets or sets Enabled.
    /// </summary>
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }

    /// <summary>
    /// Gets or sets FormName.
    /// </summary>
    [JsonPropertyName("form_name")] public string? FormName { get; set; }

    /// <summary>
    /// Gets or sets Translations.
    /// </summary>
    [JsonPropertyName("translations")] public JsonElement? Translations { get; set; }
}