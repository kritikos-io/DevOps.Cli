namespace Kritikos.DevOps.Cli;

using System.Text;
using System.Text.Json;

using Kritikos.DevOps.Api;
using Kritikos.DevOps.Cli.Settings;
using Kritikos.Extensions.Version;
using Kritikos.SpectreCli.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

using Refit;

using Spectre.Console.Cli;

public class Startup : ICommandAppStartup
{
  private static readonly RefitSettings RefitSettings = new RefitSettings()
  {
    ContentSerializer =
      new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true,
      }),
  };

  /// <inheritdoc />
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton<BranchNameValidationSettings>();
    services.AddRefitClient<IAzureDevOpsApi>(_ => RefitSettings, string.Empty);
    services.AddScoped<VssConnection>(sp =>
    {
      var azureSettings = sp.GetServices<AzureDevOpsSettings>()
        .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Organization));
      if (azureSettings is null)
      {
        throw new InvalidOperationException("Could not find proper settings for Azure DevOps");
      }

      var uri = new Uri($"{azureSettings.DevOpsInstanceServer}/{azureSettings.Organization}");
      var credentials = azureSettings.IntegratedAuthentication
        ? new VssCredentials(new WindowsCredential())
        : new VssBasicCredential(string.Empty, azureSettings.ParseAccessToken());

      return new VssConnection(
        uri,
        credentials);
    });

    services.AddScoped<TaskAgentHttpClient>(sp =>
      sp.GetRequiredService<VssConnection>().GetClient<TaskAgentHttpClient>());
    services.AddScoped<ProjectHttpClient>(sp =>
      sp.GetRequiredService<VssConnection>().GetClient<ProjectHttpClient>());
  }

  /// <inheritdoc />
  public void Configure(IConfigurator appConfiguration)
  {
    ArgumentNullException.ThrowIfNull(appConfiguration);
    var version = SemanticVersionDescriptor.FromAssembly(typeof(Startup).Assembly);
    var displayedVersion = new StringBuilder($"{version.Major}.{version.Minor}.{version.Patch}");
    if (!string.IsNullOrWhiteSpace(version.PreReleaseTag))
    {
      displayedVersion.Append('-');
      displayedVersion.Append(version.PreReleaseTag);
    }

    appConfiguration
      .SetApplicationName("devops.cli")
      .SetApplicationVersion(displayedVersion.ToString())
      .SetInterceptor(new VersionInterceptor(version))
      .CaseSensitivity(CaseSensitivity.None)
      .UseStrictParsing()
      .ValidateExamples();
  }
}
