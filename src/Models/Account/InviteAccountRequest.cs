using System.Text.Json.Serialization;

namespace Feathery.Unofficial.Client.Models.Account;

/// <summary>
/// Represents the class.
/// </summary>
public sealed class InviteAccountRequest
{

    /// <summary>
    /// Gets or sets Email.
    /// </summary>
    [JsonPropertyName("email")] public string Email { get; set; } = "";

    /// <summary>
    /// Gets or sets Role.
    /// </summary>
    [JsonPropertyName("role")] public string? Role { get; set; }

    /// <summary>
    /// Gets or sets PermissionEditFormResults.
    /// </summary>
    [JsonPropertyName("permission_edit_form_results")] public bool? PermissionEditFormResults { get; set; }

    /// <summary>
    /// Gets or sets PermissionInviteCollaborators.
    /// </summary>
    [JsonPropertyName("permission_invite_collaborators")] public bool? PermissionInviteCollaborators { get; set; }

    /// <summary>
    /// Gets or sets PermissionEditCollaboratorTemplate.
    /// </summary>
    [JsonPropertyName("permission_edit_collaborator_template")] public bool? PermissionEditCollaboratorTemplate { get; set; }

    /// <summary>
    /// Gets or sets PermissionEditLogic.
    /// </summary>
    [JsonPropertyName("permission_edit_logic")] public bool? PermissionEditLogic { get; set; }

    /// <summary>
    /// Gets or sets PermissionEditTheme.
    /// </summary>
    [JsonPropertyName("permission_edit_theme")] public bool? PermissionEditTheme { get; set; }

    /// <summary>
    /// Gets or sets UserGroups.
    /// </summary>
    [JsonPropertyName("user_groups")] public List<string>? UserGroups { get; set; }
}