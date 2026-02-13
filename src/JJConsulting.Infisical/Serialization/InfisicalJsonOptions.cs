using System.Text.Json;

namespace JJConsulting.Infisical.Serialization;

internal static class InfisicalJsonOptions
{
    public static readonly JsonSerializerOptions Value = new()
    {
        PropertyNameCaseInsensitive = true
    };
}