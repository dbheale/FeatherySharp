namespace Feathery.Unofficial.Client.Models.Document;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class DocumentEnvelopeQuery
{
    /// <summary>"document" or "user"</summary>
    public string Type { get; init; } = "document";

    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    public string Id { get; init; } = "";
}