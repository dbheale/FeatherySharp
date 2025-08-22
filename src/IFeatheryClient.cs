using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Feathery.Unofficial.Client.Models;
using Feathery.Unofficial.Client.Models.Account;
using Feathery.Unofficial.Client.Models.AI;
using Feathery.Unofficial.Client.Models.Document;
using Feathery.Unofficial.Client.Models.Form;
using Feathery.Unofficial.Client.Models.Log;
using Feathery.Unofficial.Client.Models.Submission;
using Feathery.Unofficial.Client.Models.User;
using Feathery.Unofficial.Client.Models.Workspace;

namespace Feathery.Unofficial.Client
{
    /// <summary>
    /// Interface for the Feathery API calls.
    /// </summary>
    public interface IFeatheryClient
    {
        // -------------------- Account --------------------

        /// <summary>
        /// Retrieves account information, including team and collaborator details.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Account information.</returns>
        /// <remarks>GET /api/account/</remarks>
        Task<AccountInfo> GetAccountAsync(CancellationToken ct = default);

        /// <summary>
        /// Rotates the admin API key for the current account.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Old and new API keys.</returns>
        /// <remarks>PATCH /api/account/rotate_key/</remarks>
        Task<RotateKeyResponse> RotateApiKeyAsync(CancellationToken ct = default);

        /// <summary>
        /// Invites one or more collaborators to the account (optionally with roles/permissions).
        /// </summary>
        /// <param name="invites">Invite requests containing email, role, and permissions.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Updated account info including invited collaborators.</returns>
        /// <remarks>POST /api/account/invite/</remarks>
        Task<AccountInfo> InviteAccountsAsync(IEnumerable<InviteAccountRequest> invites, CancellationToken ct = default);

        /// <summary>
        /// Edits collaborator access on the account (role/permissions).
        /// </summary>
        /// <param name="request">Edit request specifying target collaborator and updates.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Updated collaborator details.</returns>
        /// <remarks>PATCH /api/account/</remarks>
        Task<EditAccountResponse> EditAccountAsync(EditAccountRequest request, CancellationToken ct = default);

        /// <summary>
        /// Revokes access for a collaborator by email or account id.
        /// </summary>
        /// <param name="request">Uninvite request identifying the collaborator.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Updated account info after removal.</returns>
        /// <remarks>PATCH /api/account/uninvite/</remarks>
        Task<AccountInfo> UninviteAccountAsync(UninviteRequest request, CancellationToken ct = default);

        // -------------------- Forms --------------------

        /// <summary>
        /// Lists forms in the account, optionally filtered by tags.
        /// </summary>
        /// <param name="tagsCsv">Optional comma-separated tags to filter (e.g., "sales,intake").</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of form summaries.</returns>
        /// <remarks>GET /api/form/</remarks>
        Task<IReadOnlyList<FormSummary>> ListFormsAsync(string? tagsCsv = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the raw schema JSON for a specific form by id or name.
        /// </summary>
        /// <param name="formIdOrName">Form ID or unique form name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="JsonDocument"/> representing the form schema.</returns>
        /// <remarks>GET /api/form/{id}/</remarks>
        Task<JsonDocument> GetFormSchemaAsync(string formIdOrName, CancellationToken ct = default);

        /// <summary>
        /// Creates a form using the provided definition (and optional template).
        /// </summary>
        /// <param name="request">Form creation request.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Created form metadata.</returns>
        /// <remarks>POST /api/form/</remarks>
        Task<CreateFormResponse> CreateFormAsync(CreateFormRequest request, CancellationToken ct = default);

        /// <summary>
        /// Updates a form's metadata or content (e.g., enable/disable, name, translations).
        /// </summary>
        /// <param name="formId">Form ID to update.</param>
        /// <param name="request">Update payload.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Update result with current values.</returns>
        /// <remarks>PATCH /api/form/{id}/</remarks>
        Task<UpdateFormResponse> UpdateFormAsync(string formId, UpdateFormRequest request, CancellationToken ct = default);

        /// <summary>
        /// Deletes a form permanently (requires explicit confirmation).
        /// </summary>
        /// <param name="formId">Form ID to delete.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A completed task when deletion succeeds.</returns>
        /// <remarks>DELETE /api/form/{id}/ (with body confirm_delete=true)</remarks>
        Task DeleteFormAsync(string formId, CancellationToken ct = default);

        /// <summary>
        /// Copies an existing form to a new form name.
        /// </summary>
        /// <param name="request">Copy request including source form and new name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Newly created form metadata.</returns>
        /// <remarks>POST /api/form/copy/</remarks>
        Task<CopyFormResponse> CopyFormAsync(CopyFormRequest request, CancellationToken ct = default);

        // -------------------- Submissions & Hidden Fields --------------------

        /// <summary>
        /// Creates or updates a submission for one or more forms for a given user.
        /// </summary>
        /// <param name="request">Submission upsert request (fields, user id, forms, completion flag).</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The resulting submission payload.</returns>
        /// <remarks>POST /api/form/submission/</remarks>
        Task<CreateOrUpdateSubmissionResponse> CreateOrUpdateSubmissionAsync(CreateOrUpdateSubmissionRequest request, CancellationToken ct = default);

        /// <summary>
        /// Lists submission records for a form within an optional time window.
        /// </summary>
        /// <param name="formId">Form ID whose submissions to list.</param>
        /// <param name="start">Optional inclusive start timestamp (UTC ISO-8601).</param>
        /// <param name="end">Optional inclusive end timestamp (UTC ISO-8601).</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of submission records.</returns>
        /// <remarks>GET /api/form/submission/batch/{formId}/</remarks>
        Task<IReadOnlyList<SubmissionRecord>> ListFormSubmissionsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);

        /// <summary>
        /// Creates a hidden field on the account (by field id).
        /// </summary>
        /// <param name="fieldId">Field identifier to create as a hidden field.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Hidden field creation response.</returns>
        /// <remarks>POST /api/form/hidden_field/</remarks>
        Task<HiddenFieldResponse> CreateHiddenFieldAsync(string fieldId, CancellationToken ct = default);

        /// <summary>
        /// Requests a PDF export for a user's submission on a form (optionally waits until available).
        /// </summary>
        /// <param name="formId">Form ID.</param>
        /// <param name="userId">User identifier (e.g., email) for the submission.</param>
        /// <param name="waitForReady">If true, polls the exported URL until ready.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>PDF export result with a URL.</returns>
        /// <remarks>POST /api/form/submission/pdf/</remarks>
        Task<ExportSubmissionPdfResponse> ExportSubmissionPdfAsync(string formId, string userId, bool waitForReady = true, CancellationToken ct = default);

        // -------------------- Users --------------------

        /// <summary>
        /// Lists users, optionally filtering by a specific field/value.
        /// </summary>
        /// <param name="filterFieldId">Optional user field internal id to filter by.</param>
        /// <param name="filterFieldValue">Optional value for the filter field.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of user summaries.</returns>
        /// <remarks>GET /api/user/</remarks>
        Task<IReadOnlyList<UserSummary>> ListUsersAsync(string? filterFieldId = null, string? filterFieldValue = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves user fields/values for a specific user id.
        /// </summary>
        /// <param name="userId">User identifier (e.g., email).</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of user fields and values.</returns>
        /// <remarks>GET /api/field/?id={userId}</remarks>
        Task<IReadOnlyList<UserField>> GetUserFieldsAsync(string userId, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the current session state for a user (per form).
        /// </summary>
        /// <param name="userId">User identifier (e.g., email).</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>User session information.</returns>
        /// <remarks>GET /api/user/{id}/session/</remarks>
        Task<UserSession> GetUserSessionAsync(string userId, CancellationToken ct = default);

        /// <summary>
        /// Creates or updates a user by id.
        /// </summary>
        /// <param name="id">User identifier to create/update.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Created or fetched user details.</returns>
        /// <remarks>POST /api/user/</remarks>
        Task<UserCreatedOrFetched> CreateOrUpdateUserAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Deletes a user by id.
        /// </summary>
        /// <param name="id">User identifier to delete.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A completed task when deletion succeeds.</returns>
        /// <remarks>DELETE /api/user/{id}/</remarks>
        Task DeleteUserAsync(string id, CancellationToken ct = default);

        // -------------------- Logs --------------------

        /// <summary>
        /// Retrieves API connector error logs for a form within an optional time range.
        /// </summary>
        /// <param name="formId">Form ID whose connector logs to fetch.</param>
        /// <param name="start">Optional inclusive start timestamp.</param>
        /// <param name="end">Optional inclusive end timestamp.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of API connector error items.</returns>
        /// <remarks>GET /api/logs/api-connector/{Form ID}/</remarks>
        Task<IReadOnlyList<ApiConnectorError>> GetApiConnectorErrorsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves recent email logs for a form within an optional time range.
        /// </summary>
        /// <param name="formId">Form ID to query.</param>
        /// <param name="start">Optional inclusive start timestamp.</param>
        /// <param name="end">Optional inclusive end timestamp.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of email log entries.</returns>
        /// <remarks>GET /api/logs/email/{Form ID}/</remarks>
        Task<IReadOnlyList<EmailLog>> GetRecentEmailsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves account-level email issues (e.g., bounces) within an optional time range.
        /// </summary>
        /// <param name="eventType">Optional event type filter.</param>
        /// <param name="start">Optional inclusive start timestamp.</param>
        /// <param name="end">Optional inclusive end timestamp.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of email issue entries.</returns>
        /// <remarks>GET /api/logs/email/issues/</remarks>
        Task<IReadOnlyList<EmailIssue>> GetEmailIssuesAsync(string? eventType = null, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves recent Quik request logs for a form within an optional time range.
        /// </summary>
        /// <param name="formId">Form ID to query.</param>
        /// <param name="start">Optional inclusive start timestamp.</param>
        /// <param name="end">Optional inclusive end timestamp.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of Quik log entries.</returns>
        /// <remarks>GET /api/logs/quik/{Form ID}/</remarks>
        Task<IReadOnlyList<QuikLog>> GetRecentQuikRequestsAsync(string formId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);

        // -------------------- Workspaces (white-label) --------------------

        /// <summary>
        /// Lists workspaces available to the account.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of workspace summaries.</returns>
        /// <remarks>GET /api/workspace/</remarks>
        Task<IReadOnlyList<WorkspaceSummary>> ListWorkspacesAsync(CancellationToken ct = default);

        /// <summary>
        /// Creates a new workspace with the specified name.
        /// </summary>
        /// <param name="name">Workspace name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Workspace details for the created workspace.</returns>
        /// <remarks>POST /api/workspace/</remarks>
        Task<WorkspaceDetail> CreateWorkspaceAsync(string name, CancellationToken ct = default);

        /// <summary>
        /// Retrieves details for a specific workspace.
        /// </summary>
        /// <param name="workspaceId">Workspace identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Workspace details.</returns>
        /// <remarks>GET /api/workspace/{id}/</remarks>
        Task<WorkspaceDetail> GetWorkspaceAsync(string workspaceId, CancellationToken ct = default);

        /// <summary>
        /// Updates workspace properties such as name or brand URL.
        /// </summary>
        /// <param name="workspaceId">Workspace identifier.</param>
        /// <param name="request">Update payload.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Updated workspace details.</returns>
        /// <remarks>PATCH /api/workspace/{id}/</remarks>
        Task<WorkspaceDetail> UpdateWorkspaceAsync(string workspaceId, WorkspaceUpdateRequest request, CancellationToken ct = default);

        /// <summary>
        /// Deletes a workspace.
        /// </summary>
        /// <param name="workspaceId">Workspace identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A completed task when deletion succeeds.</returns>
        /// <remarks>DELETE /api/workspace/{id}/</remarks>
        Task DeleteWorkspaceAsync(string workspaceId, CancellationToken ct = default);

        /// <summary>
        /// Generates a one-time login token for a workspace on behalf of an account id.
        /// </summary>
        /// <param name="workspaceId">Workspace identifier.</param>
        /// <param name="accountId">Account identifier to authenticate.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Workspace auth token.</returns>
        /// <remarks>POST /api/workspace/{id}/auth/</remarks>
        Task<WorkspaceAuthToken> GenerateWorkspaceLoginTokenAsync(string workspaceId, string accountId, CancellationToken ct = default);

        /// <summary>
        /// Creates a new form in a workspace using a template id and desired form name.
        /// </summary>
        /// <param name="workspaceId">Workspace identifier.</param>
        /// <param name="templateId">Template form identifier.</param>
        /// <param name="formName">New form name to create.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Created form metadata.</returns>
        /// <remarks>POST /api/workspace/{id}/create-template-form/</remarks>
        Task<CreateFormResponse> PopulateWorkspaceWithTemplateFormAsync(string workspaceId, string templateId, string formName, CancellationToken ct = default);

        // -------------------- Document templates --------------------

        /// <summary>
        /// Fills or sends a document template for signing.
        /// </summary>
        /// <param name="request">Document fill/sign request.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Document fill response (e.g., file URL).</returns>
        /// <remarks>POST /api/document/fill/</remarks>
        Task<DocumentFillResponse> FillOrSignDocumentTemplateAsync(DocumentFillRequest request, CancellationToken ct = default);

        /// <summary>
        /// Lists document envelopes by document id or user id.
        /// </summary>
        /// <param name="query">Envelope query specifying type ("document" or "user") and id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of document envelopes.</returns>
        /// <remarks>GET /api/document/envelope/</remarks>
        Task<IReadOnlyList<DocumentEnvelope>> ListDocumentEnvelopesAsync(DocumentEnvelopeQuery query, CancellationToken ct = default);

        /// <summary>
        /// Deletes a document envelope by id.
        /// </summary>
        /// <param name="envelopeId">Envelope identifier to delete.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A completed task when deletion succeeds.</returns>
        /// <remarks>DELETE /api/document/envelope/{id}/</remarks>
        Task DeleteDocumentEnvelopeAsync(string envelopeId, CancellationToken ct = default);

        // -------------------- AI Document Intelligence --------------------

        /// <summary>
        /// Runs a document extraction (AI) for the specified extraction id with one or more files.
        /// </summary>
        /// <param name="extractionId">Extraction configuration identifier.</param>
        /// <param name="files">
        /// Files to upload as a sequence of tuples: (FileName, ContentType, Content stream).
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Run response containing user id or run metadata.</returns>
        /// <remarks>POST /api/ai/run/{extraction_id}/</remarks>
        Task<AiRunResponse> RunExtractionAsync(string extractionId, IEnumerable<(string FileName, string ContentType, Stream Content)> files, CancellationToken ct = default);

        /// <summary>
        /// Lists past AI extraction runs for a given extraction id within an optional time window.
        /// </summary>
        /// <param name="extractionId">Extraction configuration identifier.</param>
        /// <param name="start">Optional inclusive start timestamp.</param>
        /// <param name="end">Optional inclusive end timestamp.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Read-only list of extraction run records.</returns>
        /// <remarks>GET /api/ai/run/batch/{extraction_id}/</remarks>
        Task<IReadOnlyList<AiRunRecord>> ListExtractionRunsAsync(string extractionId, DateTimeOffset? start = null, DateTimeOffset? end = null, CancellationToken ct = default);
    }
}
