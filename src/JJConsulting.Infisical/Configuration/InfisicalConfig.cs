namespace JJConsulting.Infisical.Configuration;

/// <summary>
/// Base configuration used to access Infisical secrets.
/// </summary>
public abstract class InfisicalConfig
{
    /// <summary>
    /// Gets the Infisical environment slug (for example <c>prod</c>).
    /// </summary>
    public string Environment { get; init; } = null!;

    /// <summary>
    /// Gets the Infisical project/workspace identifier.
    /// </summary>
    public string ProjectId { get; init; } = null!;

    /// <summary>
    /// Gets the secret path inside the Infisical project. Defaults to <c>/</c>.
    /// </summary>
    public string SecretPath
    {
        get;
        init
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException("SecretPath must be set");
            field = value;
        }
    } = "/";

    /// <summary>
    /// Gets the Infisical base URL. Defaults to <c>https://app.infisical.com</c>.
    /// </summary>
    public string Url
    {
        get;
        init
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException("InfisicalUrl must be set");

            field = value.EndsWith("/api")
                ? value[..^4]
                : value;
        }
    } = "https://app.infisical.com";

    /// <summary>
    /// Validates whether the authentication-specific configuration is usable.
    /// </summary>
    /// <returns><see langword="true"/> when the configuration is valid; otherwise <see langword="false"/>.</returns>
    public abstract bool IsValid();
}
