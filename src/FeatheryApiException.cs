using System.Net;

namespace Feathery.Unofficial.Client;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class FeatheryApiException : Exception
{

    /// <summary>
    /// Gets or sets Status.
    /// </summary>
    public HttpStatusCode Status { get; }

    /// <summary>
    /// Gets or sets Body.
    /// </summary>
    public string? Body { get; }

    /// <summary>
    /// Gets or sets Request.
    /// </summary>
    public HttpRequestMessage? Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatheryApiException"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="body">The body.</param>
    /// <param name="request">The request.</param>
    /// <param name="message">The message.</param>
    public FeatheryApiException(HttpStatusCode status, string? body, HttpRequestMessage? request, string message) : base(message)
    {
        Status = status;
        Body = body;
        Request = request;
    }
}