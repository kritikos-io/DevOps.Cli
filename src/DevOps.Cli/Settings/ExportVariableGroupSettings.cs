namespace Kritikos.DevOps.Cli.Settings;

using System.ComponentModel;

public sealed class ExportVariableGroupSettings : AzureDevOpsSettings
{
  [CommandOption("--output")]
  [Description("File to output variables to")]
  [DefaultValue("variables.xlsx")]
  public FileInfo OutputFile { get; set; }

  [CommandOption("--groups")]
  [DefaultValue(4)]
  public int VariableGroupsPerRow { get; set; }
}
