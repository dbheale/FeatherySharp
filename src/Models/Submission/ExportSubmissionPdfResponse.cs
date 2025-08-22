using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Submission;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class ExportSubmissionPdfResponse
{

    /// <summary>
    /// Gets or sets PdfUrl.
    /// </summary>
    [JsonPropertyName("pdf_url")] public string PdfUrl { get; set; } = "";
}