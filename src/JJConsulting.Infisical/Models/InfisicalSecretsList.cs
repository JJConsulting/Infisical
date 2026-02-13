using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

public sealed class InfisicalSecretsList
{
    [JsonPropertyName("secrets")]
    public required List<InfisicalSecretListItem> Secrets { get; init; }

    [JsonPropertyName("imports")]
    public required List<object> Imports { get; init; }
    
}
