namespace Kritikos.DevOps.Cli.Commands;

using Kritikos.DevOps.Cli.Settings;
using Kritikos.SpectreCli.Infrastructure;

using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

using Spectre.Console;

public class ExportVariableGroupAsyncCommand(TaskAgentHttpClient taskAgent, ProjectHttpClient projectAgent)
  : CancellableAsyncCommand<ExportVariableGroupSettings>
{
  /// <inheritdoc />
  public override async Task<int> ExecuteAsync(
    CommandContext context,
    ExportVariableGroupSettings settings,
    CancellationToken cancellationToken)
  {
    await AnsiConsole.Status()
      .AutoRefresh(true)
      .Spinner(Spinner.Known.Monkey)
      .StartAsync("Thinking...", async ctx =>
      {
      });
    return 0;
  }
}
