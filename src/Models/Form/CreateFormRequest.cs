using System.Text.Json;
using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class CreateFormRequest
{
    // The "Create a Form" body can be large; we keep it flexible

    /// <summary>
    /// Gets or sets FormName.
    /// </summary>
    [JsonPropertyName("form_name")] public string FormName { get; set; } = "";

    /// <summary>
    /// Gets or sets TemplateFormId.
    /// </summary>
    [JsonPropertyName("template_form_id")] public string TemplateFormId { get; set; } = "";

    /// <summary>
    /// Gets or sets Enabled.
    /// </summary>
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }

    /// <summary>
    /// Gets or sets Steps.
    /// </summary>
    [JsonPropertyName("steps")] public JsonElement? Steps { get; set; }

    /// <summary>
    /// Gets or sets NavigationRules.
    /// </summary>
    [JsonPropertyName("navigation_rules")] public JsonElement? NavigationRules { get; set; }

    /// <summary>
    /// Gets or sets LogicRules.
    /// </summary>
    [JsonPropertyName("logic_rules")] public JsonElement? LogicRules { get; set; }
}