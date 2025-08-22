using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Form;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class CopyFormRequest
{

    /// <summary>
    /// Gets or sets FormName.
    /// </summary>
    [JsonPropertyName("form_name")] public string FormName { get; set; } = "";

    /// <summary>
    /// Gets or sets CopyFormId.
    /// </summary>
    [JsonPropertyName("copy_form_id")] public string CopyFormId { get; set; } = "";
}