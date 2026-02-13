using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

public sealed class InfisicalSecret
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("_id")]
    public Guid InternalId { get; set; }

    [JsonPropertyName("workspace")]
    public Guid Workspace { get; set; }

    [JsonPropertyName("environment")]
    public required string Environment { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("secretKey")]
    public required string SecretKey { get; set; }

    [JsonPropertyName("secretValue")]
    public required string SecretValue { get; set; }

    [JsonPropertyName("secretComment")]
    public required string SecretComment { get; set; }

    [JsonPropertyName("secretReminderNote")]
    public required string SecretReminderNote { get; set; }

    [JsonPropertyName("secretReminderRepeatDays")]
    public int? SecretReminderRepeatDays { get; set; }

    [JsonPropertyName("skipMultilineEncoding")]
    public bool SkipMultilineEncoding { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("secretValueHidden")]
    public bool SecretValueHidden { get; set; }

    [JsonPropertyName("secretPath")]
    public required string SecretPath { get; set; }

    [JsonPropertyName("tags")]
    public required List<string> Tags { get; set; }

    [JsonPropertyName("secretMetadata")]
    public required List<object> SecretMetadata { get; set; }
}
