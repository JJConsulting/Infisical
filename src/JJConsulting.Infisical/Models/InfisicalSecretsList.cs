using System.Text.Json.Serialization;

namespace JJConsulting.Infisical.Models;

/// <summary>
/// Represents the Infisical response that contains secret items and imports.
/// </summary>
public sealed class InfisicalSecretsList
{
    /// <summary>
    /// Gets the secret items returned by Infisical.
    /// </summary>
    [JsonPropertyName("secrets")]
    public required List<InfisicalSecretListItem> Secrets { get; init; }

    /// <summary>
    /// Gets imported secret collections returned by Infisical.
    /// </summary>
    [JsonPropertyName("imports")]
    public required List<object> Imports { get; init; }
    
}
