using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

internal sealed class InfisicalSecretEnvelope
{
    [JsonPropertyName("secret")]
    public required InfisicalSecret Secret { get; set; }
}
