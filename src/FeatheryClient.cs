using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Feathery.Unofficial.Client.Models;
using Feathery.Unofficial.Client.Models.Account;
using Feathery.Unofficial.Client.Models.AI;
using Feathery.Unofficial.Client.Models.Document;
using Feathery.Unofficial.Client.Models.Form;
using Feathery.Unofficial.Client.Models.Log;
using Feathery.Unofficial.Client.Models.Submission;
using Feathery.Unofficial.Client.Models.User;
using Feathery.Unofficial.Client.Models.Workspace;

namespace Feathery.Unofficial.Client;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class FeatheryClient : IFeatheryClient
{
    private readonly HttpClient _http;
    private readonly FeatheryOptions _options;
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = null, // we use explicit JsonPropertyName to keep snake_case
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatheryClient"/> class.
    /// </summary>
    /// <param name="httpClient">The httpClient.</param>
    /// <param name="options">The options.</param>
    public FeatheryClient(HttpClient httpClient, FeatheryOptions options)
    {
        _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _http.BaseAddress ??= options.BaseUri;
        if (!_http.DefaultRequestHeaders.Contains("Authorization"))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _options.ApiKey);
    }

    // ---------- helpers ----------
    private static StringContent Json(object body) =>
        new StringContent(System.Text.Json.JsonSerializer.Serialize(body, JsonOpts), Encoding.UTF8, "application/json");

    private async Task<T> ReadAsAsync<T>(HttpResponseMessage resp)
    {
        var text = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw new FeatheryApiException(resp.StatusCode, text, resp.RequestMessage, $"Feathery API error ({(int)resp.StatusCode})");
        return typeof(T) == typeof(JsonDocument)
            ? (T)(object)JsonDocument.Parse(text)
            : System.Text.Json.JsonSerializer.Deserialize<T>(text, JsonOpts)!;
    }

    private static string Q(params (string, string?)[] kvps)
    {
        var sb = new StringBuilder();
        var first = true;
        foreach (var (k, v) in kvps)
        {
            if (string.IsNullOrWhiteSpace(v)) continue;
            sb.Append(first ? "?" : "&");
            sb.Append(Uri.EscapeDataString(k));
            sb.Append('=');
            sb.Append(Uri.EscapeDataString(v!));
            first = false;
        }
        return sb.ToString();
    }

    private static string Iso(DateTimeOffset dto) => dto.ToUniversalTime().ToString("o");

    // ---------- Account ----------
    /// <inheritdoc />
    public Task<AccountInfo> GetAccountAsync(CancellationToken ct = default)
        => SendGet<AccountInfo>("api/account/", ct);

    /// <inheritdoc />
    public Task<RotateKeyResponse> RotateApiKeyAsync(CancellationToken ct = default)
        => SendPatch<RotateKeyResponse>("api/account/rotate_key/", body: new { }, ct);

    /// <inheritdoc />
    public Task<AccountInfo> InviteAccountsAsync(IEnumerable<InviteAccountRequest> invites, CancellationToken ct = default)
        => SendPost<AccountInfo>("api/account/invite/", invites, ct);

    /// <inheritdoc />
    public Task<EditAccountResponse> EditAccountAsync(EditAccountRequest request, CancellationToken ct = default)
        => SendPatch<EditAccountResponse>("api/account/", request, ct);

    /// <inheritdoc />
    public Task<AccountInfo> UninviteAccountAsync(UninviteRequest request, CancellationToken ct = default)
        => SendPatch<AccountInfo>("api/account/uninvite/", request, ct);

    // ---------- Forms ----------
    /// <inheritdoc />
    public Task<IReadOnlyList<FormSummary>> ListFormsAsync(string? tagsCsv = null, CancellationToken ct = default)
        => SendGet<List<FormSummary>>("api/form/" + Q(("tags", tagsCsv)), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<JsonDocument> GetFormSchemaAsync(string formIdOrName, CancellationToken ct = default)
        => SendGet<JsonDocument>($"api/form/{Uri.EscapeDataString(formIdOrName)}/", ct); // schema shape is large

    /// <inheritdoc />
    public Task<CreateFormResponse> CreateFormAsync(CreateFormRequest request, CancellationToken ct = default)
        => SendPost<CreateFormResponse>("api/form/", request, ct);

    /// <inheritdoc />
    public Task<UpdateFormResponse> UpdateFormAsync(string formId, UpdateFormRequest request, CancellationToken ct = default)
        => SendPatch<UpdateFormResponse>($"api/form/{Uri.EscapeDataString(formId)}/", request, ct);

    /// <inheritdoc />
    public async Task DeleteFormAsync(string formId, CancellationToken ct = default)
    {
        var body = Json(new { confirm_delete = true });
        using var req = new HttpRequestMessage(HttpMethod.Delete, $"api/form/{Uri.EscapeDataString(formId)}/") { Content = body };
        using var resp = await _http.SendAsync(req, ct).ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw await CreateException(resp).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<CopyFormResponse> CopyFormAsync(CopyFormRequest request, CancellationToken ct = default)
        => SendPost<CopyFormResponse>("api/form/copy/", request, ct);

    // ---------- Submissions & hidden fields ----------
    /// <inheritdoc />
    public Task<CreateOrUpdateSubmissionResponse> CreateOrUpdateSubmissionAsync(CreateOrUpdateSubmissionRequest request, CancellationToken ct = default)
        => SendPost<CreateOrUpdateSubmissionResponse>("api/form/submission/", request, ct);

    /// <inheritdoc />
    public Task<IReadOnlyList<SubmissionRecord>> ListFormSubmissionsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<SubmissionRecord>>($"api/form/submission/batch/{Uri.EscapeDataString(formId)}/" + Q(("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<HiddenFieldResponse> CreateHiddenFieldAsync(string fieldId, CancellationToken ct = default)
        => SendPost<HiddenFieldResponse>("api/form/hidden_field/", new { field_id = fieldId }, ct);

    /// <inheritdoc />
    public async Task<ExportSubmissionPdfResponse> ExportSubmissionPdfAsync(string formId, string userId, bool waitForReady = true, CancellationToken ct = default)
    {
        var res = await SendPost<ExportSubmissionPdfResponse>("api/form/submission/pdf/", new { form_id = formId, user_id = userId }, ct).ConfigureAwait(false);
        if (!waitForReady) return res;

        // The docs note the PDF may not be immediately available; poll up to 5x per second. :contentReference[oaicite:1]{index=1}
        for (int i = 0; i < _options.PdfPollAttempts; i++)
        {
            using var head = new HttpRequestMessage(HttpMethod.Head, res.PdfUrl);
            var ping = await _http.SendAsync(head, ct).ConfigureAwait(false);
            if (ping.IsSuccessStatusCode) break;
            await Task.Delay(_options.PdfPollDelay, ct).ConfigureAwait(false);
        }
        return res;
    }

    // ---------- Users ----------
    /// <inheritdoc />
    public Task<IReadOnlyList<UserSummary>> ListUsersAsync(string? filterFieldId = null, string? filterFieldValue = null, CancellationToken ct = default)
        => SendGet<List<UserSummary>>("api/user/" + Q(("filter_field_id", filterFieldId), ("filter_field_value", filterFieldValue)), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<IReadOnlyList<UserField>> GetUserFieldsAsync(string userId, CancellationToken ct = default)
        => SendGet<List<UserField>>("api/field/" + Q(("id", userId)), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<UserSession> GetUserSessionAsync(string userId, CancellationToken ct = default)
        => SendGet<UserSession>($"api/user/{Uri.EscapeDataString(userId)}/session/", ct);

    /// <inheritdoc />
    public Task<UserCreatedOrFetched> CreateOrUpdateUserAsync(string id, CancellationToken ct = default)
        => SendPost<UserCreatedOrFetched>("api/user/", new { id }, ct);

    /// <inheritdoc />
    public async Task DeleteUserAsync(string id, CancellationToken ct = default)
    {
        using var resp = await _http.DeleteAsync($"api/user/{Uri.EscapeDataString(id)}/", ct).ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw await CreateException(resp).ConfigureAwait(false);
    }

    // ---------- Logs ----------
    /// <inheritdoc />
    public Task<IReadOnlyList<ApiConnectorError>> GetApiConnectorErrorsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<ApiConnectorError>>($"api/logs/api-connector/{Uri.EscapeDataString(formId)}/" + Q(("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<IReadOnlyList<EmailLog>> GetRecentEmailsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<EmailLog>>($"api/logs/email/{Uri.EscapeDataString(formId)}/" + Q(("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<IReadOnlyList<EmailIssue>> GetEmailIssuesAsync(string? eventType = null, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<EmailIssue>>("api/logs/email/issues/" + Q(("event_type", eventType), ("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    /// <inheritdoc />
    public Task<IReadOnlyList<QuikLog>> GetRecentQuikRequestsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<QuikLog>>($"api/logs/quik/{Uri.EscapeDataString(formId)}/" + Q(("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    // ---------- Workspaces ----------
    /// <inheritdoc />
    public Task<IReadOnlyList<WorkspaceSummary>> ListWorkspacesAsync(CancellationToken ct = default)
        => SendGet<List<WorkspaceSummary>>("api/workspace/", ct).AsReadOnly();

    /// <inheritdoc />
    public Task<WorkspaceDetail> CreateWorkspaceAsync(string name, CancellationToken ct = default)
        => SendPost<WorkspaceDetail>("api/workspace/", new { name }, ct);

    /// <inheritdoc />
    public Task<WorkspaceDetail> GetWorkspaceAsync(string workspaceId, CancellationToken ct = default)
        => SendGet<WorkspaceDetail>($"api/workspace/{Uri.EscapeDataString(workspaceId)}/", ct);

    /// <inheritdoc />
    public Task<WorkspaceDetail> UpdateWorkspaceAsync(string workspaceId, WorkspaceUpdateRequest request, CancellationToken ct = default)
        => SendPatch<WorkspaceDetail>($"api/workspace/{Uri.EscapeDataString(workspaceId)}/", request, ct);

    /// <inheritdoc />
    public async Task DeleteWorkspaceAsync(string workspaceId, CancellationToken ct = default)
    {
        using var resp = await _http.DeleteAsync($"api/workspace/{Uri.EscapeDataString(workspaceId)}/", ct).ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw await CreateException(resp).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<WorkspaceAuthToken> GenerateWorkspaceLoginTokenAsync(string workspaceId, string accountId, CancellationToken ct = default)
        => SendPost<WorkspaceAuthToken>($"api/workspace/{Uri.EscapeDataString(workspaceId)}/auth/", new { account_id = accountId }, ct);

    /// <inheritdoc />
    public Task<CreateFormResponse> PopulateWorkspaceWithTemplateFormAsync(string workspaceId, string templateId, string formName, CancellationToken ct = default)
        => SendPost<CreateFormResponse>($"api/workspace/{Uri.EscapeDataString(workspaceId)}/create-template-form/", new { template_id = templateId, form_name = formName }, ct);

    // ---------- Document templates ----------
    /// <inheritdoc />
    public Task<DocumentFillResponse> FillOrSignDocumentTemplateAsync(DocumentFillRequest request, CancellationToken ct = default)
        => SendPost<DocumentFillResponse>("api/document/fill/", request, ct);

    /// <inheritdoc />
    public Task<IReadOnlyList<DocumentEnvelope>> ListDocumentEnvelopesAsync(DocumentEnvelopeQuery query, CancellationToken ct = default)
        => SendGet<List<DocumentEnvelope>>("api/document/envelope/" + Q(("type", query.Type), ("id", query.Id)), ct).AsReadOnly();

    /// <inheritdoc />
    public async Task DeleteDocumentEnvelopeAsync(string envelopeId, CancellationToken ct = default)
    {
        using var resp = await _http.DeleteAsync($"api/document/envelope/{Uri.EscapeDataString(envelopeId)}/", ct).ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw await CreateException(resp).ConfigureAwait(false);
    }

    // ---------- AI Document Intelligence ----------
    /// <inheritdoc />
    public async Task<AiRunResponse> RunExtractionAsync(string extractionId, IEnumerable<(string FileName, string ContentType, Stream Content)> files, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        foreach (var (name, contentType, stream) in files)
        {
            var sc = new StreamContent(stream);
            sc.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Add(sc, "files", name);
        }
        using var resp = await _http.PostAsync($"api/ai/run/{Uri.EscapeDataString(extractionId)}/", content, ct).ConfigureAwait(false);
        return await ReadAsAsync<AiRunResponse>(resp).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<AiRunRecord>> ListExtractionRunsAsync(string extractionId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default)
        => SendGet<List<AiRunRecord>>($"api/ai/run/batch/{Uri.EscapeDataString(extractionId)}/" + Q(("start_time", start is null ? null : Iso(start.Value)), ("end_time", end is null ? null : Iso(end.Value))), ct).AsReadOnly();

    // ---------- low-level senders ----------
    private async Task<T> SendGet<T>(string url, CancellationToken ct)
    {
        using var resp = await _http.GetAsync(url, ct).ConfigureAwait(false);
        return await ReadAsAsync<T>(resp).ConfigureAwait(false);
    }

    private async Task<T> SendPost<T>(string url, object body, CancellationToken ct)
    {
        using var resp = await _http.PostAsync(url, Json(body), ct).ConfigureAwait(false);
        return await ReadAsAsync<T>(resp).ConfigureAwait(false);
    }

    private async Task<T> SendPatch<T>(string url, object body, CancellationToken ct)
    {
        var req = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = Json(body) };
        using var resp = await _http.SendAsync(req, ct).ConfigureAwait(false);
        return await ReadAsAsync<T>(resp).ConfigureAwait(false);
    }

    private static async Task<FeatheryApiException> CreateException(HttpResponseMessage resp)
    {
        var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new FeatheryApiException(resp.StatusCode, body, resp.RequestMessage, $"Feathery API error ({(int)resp.StatusCode})");
    }
}