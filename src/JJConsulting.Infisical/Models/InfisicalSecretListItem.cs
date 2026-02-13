using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

public sealed class InfisicalSecretListItem
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("_id")]
    public required Guid InternalId { get; init; }

    [JsonPropertyName("workspace")]
    public required Guid Workspace { get; init; }

    [JsonPropertyName("environment")]
    public required string Environment { get; init; }

    [JsonPropertyName("version")]
    public required int Version { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("secretKey")]
    public required string Key { get; init; }

    [JsonPropertyName("secretValue")]
    public required string Value { get; init; }

    [JsonPropertyName("secretComment")]
    public required string SecretComment { get; init; }

    [JsonPropertyName("secretReminderNote")]
    public string? SecretReminderNote { get; init; }

    [JsonPropertyName("secretReminderRepeatDays")]
    public int? SecretReminderRepeatDays { get; init; }

    [JsonPropertyName("skipMultilineEncoding")]
    public required bool SkipMultilineEncoding { get; init; }

    [JsonPropertyName("createdAt")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("isRotatedSecret")]
    public required bool IsRotatedSecret { get; init; }

    [JsonPropertyName("rotationId")]
    public string? RotationId { get; init; }

    [JsonPropertyName("secretPath")]
    public required string SecretPath { get; init; }

    [JsonPropertyName("secretValueHidden")]
    public required bool SecretValueHidden { get; init; }

    [JsonPropertyName("secretMetadata")]
    public required List<object> SecretMetadata { get; init; }

    [JsonPropertyName("tags")]
    public required List<object> Tags { get; init; }
}