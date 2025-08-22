using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class UpdateFormResponse
{

    /// <summary>
    /// Gets or sets Enabled.
    /// </summary>
    [JsonPropertyName("enabled")] public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets FormName.
    /// </summary>
    [JsonPropertyName("form_name")] public string FormName { get; set; } = "";
}