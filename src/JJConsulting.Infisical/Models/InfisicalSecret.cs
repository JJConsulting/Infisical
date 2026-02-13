using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

/// <summary>
/// Represents a single secret payload returned by Infisical.
/// </summary>
public sealed class InfisicalSecret
{
    /// <summary>
    /// Gets or sets the external identifier of the secret.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the internal MongoDB identifier of the secret.
    /// </summary>
    [JsonPropertyName("_id")]
    public Guid InternalId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier the secret belongs to.
    /// </summary>
    [JsonPropertyName("workspace")]
    public Guid Workspace { get; set; }

    /// <summary>
    /// Gets or sets the environment where the secret is defined.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; set; }

    /// <summary>
    /// Gets or sets the secret version number.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; }

    /// <summary>
    /// Gets or sets the secret type.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Gets or sets the secret key.
    /// </summary>
    [JsonPropertyName("secretKey")]
    public required string SecretKey { get; set; }

    /// <summary>
    /// Gets or sets the secret value.
    /// </summary>
    [JsonPropertyName("secretValue")]
    public required string SecretValue { get; set; }

    /// <summary>
    /// Gets or sets the user-defined comment for the secret.
    /// </summary>
    [JsonPropertyName("secretComment")]
    public required string SecretComment { get; set; }

    /// <summary>
    /// Gets or sets the reminder note configured for the secret.
    /// </summary>
    [JsonPropertyName("secretReminderNote")]
    public required string SecretReminderNote { get; set; }

    /// <summary>
    /// Gets or sets the reminder repeat interval in days.
    /// </summary>
    [JsonPropertyName("secretReminderRepeatDays")]
    public int? SecretReminderRepeatDays { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether multiline encoding is skipped.
    /// </summary>
    [JsonPropertyName("skipMultilineEncoding")]
    public bool SkipMultilineEncoding { get; set; }

    /// <summary>
    /// Gets or sets when the secret was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets when the secret was last updated.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the secret value is hidden.
    /// </summary>
    [JsonPropertyName("secretValueHidden")]
    public bool SecretValueHidden { get; set; }

    /// <summary>
    /// Gets or sets the path where the secret is stored.
    /// </summary>
    [JsonPropertyName("secretPath")]
    public required string SecretPath { get; set; }

    /// <summary>
    /// Gets or sets tags associated with the secret.
    /// </summary>
    [JsonPropertyName("tags")]
    public required List<string> Tags { get; set; }

    /// <summary>
    /// Gets or sets additional metadata associated with the secret.
    /// </summary>
    [JsonPropertyName("secretMetadata")]
    public required List<object> SecretMetadata { get; set; }
}
