namespace Kritikos.DevOps.Cli;

using Kritikos.Extensions.Version;

using Spectre.Console;

public class VersionInterceptor(SemanticVersionDescriptor version) : ICommandInterceptor
{
  /// <inheritdoc />
  public void Intercept(CommandContext context, CommandSettings settings)
  {
    if (string.IsNullOrWhiteSpace(version.PreReleaseTag))
    {
      return;
    }

    AnsiConsole.MarkupLine(@"[teal]________              ________                   _________ .__  .__ [/]");
    AnsiConsole.MarkupLine(@"[teal]\______ \   _______  _\_____  \ ______  ______   \_   ___ \|  | |__|[/]");
    AnsiConsole.MarkupLine(@"[teal] |    |  \_/ __ \  \/ //   |   \\____ \/  ___/   /    \  \/|  | |  |[/]");
    AnsiConsole.MarkupLine(@"[teal] |    `   \  ___/\   //    |    \  |_> >___ \    \     \___|  |_|  |[/]");
    AnsiConsole.MarkupLine(@"[teal]/_______  /\___  >\_/ \_______  /   __/____  > /\ \______  /____/__|[/]");
    AnsiConsole.MarkupLine(@"[teal]        \/     \/             \/|__|       \/  \/        \/         [/]");

    AnsiConsole.MarkupLine("Thank you for supporting our [deeppink3]pre-release[/] versions! Please, report any bugs in https://github.com/kritikos-io/DevOps.Cli/issues");
  }
}
