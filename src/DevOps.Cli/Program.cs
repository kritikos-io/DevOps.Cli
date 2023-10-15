using System.Text;

using Kritikos.DevOps.Cli;
using Kritikos.SpectreCli.Infrastructure;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

return await CommandBuilder.CreateDefaultBuilder()
  .UseStartup<Startup>()
  .RegisterSettingsFromAssemblyContaining<Startup>()
  .Build()
  .RunAsync(args);
