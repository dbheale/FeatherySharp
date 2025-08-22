namespace Feathery.Unofficial.Client;

/// <summary>
/// 
/// </summary>
public class FeatheryOptions
{
    /// <summary>
    /// API Key that can be found within feathery.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
    /// <summary>
    /// Endpoint used for feathery. By default, the value is 'https://api.feathery.io/'
    /// </summary>
    public Uri BaseUri { get; set; } = new Uri("https://api.feathery.io/");
    /// <summary>
    /// Number of times to poll the PDF endpoint
    /// </summary>
    public int PdfPollAttempts { get; set; } = 5;
    /// <summary>
    /// Delay between Polling
    /// </summary>
    public TimeSpan PdfPollDelay { get; set; } = TimeSpan.FromSeconds(1);
}