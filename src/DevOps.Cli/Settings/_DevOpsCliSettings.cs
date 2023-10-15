namespace Kritikos.DevOps.Cli.Settings;

using System.ComponentModel;

public abstract class DevOpsCliSettings : CommandSettings
{
  [CommandOption("--instance")]
  [Description("Base uri of Azure DevOps Server (TFS) instance to connect to")]
  [DefaultValue("https://dev.azure.com")]
  public string DevOpsInstanceServer { get; init; } = "dev.azure.com";

  [CommandOption("--interactive")]
  [Description("Whether to use interactive mode or not")]
  [DefaultValue(false)]
  public bool IsInteractive { get; init; }

  [CommandOption("--nologo", IsHidden = true)]
  [Description("Hides logo and pre-release banners")]
  [DefaultValue(false)]
  public bool NoLogo { get; set; }
}
