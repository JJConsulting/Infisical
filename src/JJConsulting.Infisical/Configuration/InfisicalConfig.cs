namespace JJConsulting.Infisical.Configuration;

public abstract class InfisicalConfig
{
    public string Environment { get; init; } = null!;
    public string ProjectId { get; init; } = null!;

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

    public abstract bool IsValid();
}
