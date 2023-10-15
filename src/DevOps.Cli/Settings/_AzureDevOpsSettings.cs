namespace Kritikos.DevOps.Cli.Settings;

using Spectre.Console;

public abstract class AzureDevOpsSettings : DevOpsCliSettings
{
  [CommandOption("-o|--organization")]
  public string Organization { get; set; } = string.Empty;

  [CommandOption("-p|--project")]
  public string Project { get; set; } = string.Empty;

  [CommandOption("--integrated-authentication")]
  public bool IntegratedAuthentication { get; set; }

  [CommandOption("--pat")]
  public string AccessToken { get; set; }

  /// <inheritdoc />
  public override ValidationResult Validate()
  {
    if (!IntegratedAuthentication && string.IsNullOrWhiteSpace(ParseAccessToken()))
    {
      ValidationResult.Error(
        "Either Access Token parameter or Environment Variable is required, or  Windows Integrated Authentication!");
    }

    return base.Validate();
  }

  internal string? ParseAccessToken()
  {
    var accessToken = AccessToken;
    if (string.IsNullOrWhiteSpace(accessToken))
    {
      accessToken = Environment.GetEnvironmentVariable("ApiKeys__AzureDevOps");
    }

    return accessToken;
  }
}
