using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

/// <summary>
/// Represents a secret entry returned by Infisical when listing secrets.
/// </summary>
public sealed class InfisicalSecretListItem
{
    /// <summary>
    /// Gets the external identifier of the secret.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the internal MongoDB identifier of the secret.
    /// </summary>
    [JsonPropertyName("_id")]
    public required Guid InternalId { get; init; }

    /// <summary>
    /// Gets the workspace identifier the secret belongs to.
    /// </summary>
    [JsonPropertyName("workspace")]
    public required Guid Workspace { get; init; }

    /// <summary>
    /// Gets the environment where the secret is defined.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; init; }

    /// <summary>
    /// Gets the secret version number.
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; init; }

    /// <summary>
    /// Gets the secret type.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// Gets the secret key.
    /// </summary>
    [JsonPropertyName("secretKey")]
    public required string Key { get; init; }

    /// <summary>
    /// Gets the secret value.
    /// </summary>
    [JsonPropertyName("secretValue")]
    public required string Value { get; init; }

    /// <summary>
    /// Gets the user-defined comment for the secret.
    /// </summary>
    [JsonPropertyName("secretComment")]
    public required string SecretComment { get; init; }

    /// <summary>
    /// Gets the reminder note configured for the secret.
    /// </summary>
    [JsonPropertyName("secretReminderNote")]
    public string? SecretReminderNote { get; init; }

    /// <summary>
    /// Gets the reminder repeat interval in days.
    /// </summary>
    [JsonPropertyName("secretReminderRepeatDays")]
    public int? SecretReminderRepeatDays { get; init; }

    /// <summary>
    /// Gets a value indicating whether multiline encoding is skipped.
    /// </summary>
    [JsonPropertyName("skipMultilineEncoding")]
    public required bool SkipMultilineEncoding { get; init; }

    /// <summary>
    /// Gets when the secret was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets when the secret was last updated.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public required DateTime UpdatedAt { get; init; }

    /// <summary>
    /// Gets a value indicating whether this is a rotated secret.
    /// </summary>
    [JsonPropertyName("isRotatedSecret")]
    public required bool IsRotatedSecret { get; init; }

    /// <summary>
    /// Gets the rotation identifier, when rotation is enabled.
    /// </summary>
    [JsonPropertyName("rotationId")]
    public string? RotationId { get; init; }

    /// <summary>
    /// Gets the path where the secret is stored.
    /// </summary>
    [JsonPropertyName("secretPath")]
    public required string SecretPath { get; init; }

    /// <summary>
    /// Gets a value indicating whether the secret value is hidden.
    /// </summary>
    [JsonPropertyName("secretValueHidden")]
    public required bool SecretValueHidden { get; init; }

    /// <summary>
    /// Gets additional metadata associated with the secret.
    /// </summary>
    [JsonPropertyName("secretMetadata")]
    public required List<object> SecretMetadata { get; init; }

    /// <summary>
    /// Gets tags associated with the secret.
    /// </summary>
    [JsonPropertyName("tags")]
    public required List<object> Tags { get; init; }
}
