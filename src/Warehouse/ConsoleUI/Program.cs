using Application;
using ConsoleUI;
using Infrastructure;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder => builder.ClearProviders())
    .ConfigureServices(services =>
    {
        var provider = services.BuildServiceProvider();

        services.AddHostedService<Worker>();
        services.AddInfrastructureServices(provider.GetRequiredService<IConfiguration>());
        services.AddApplicationServices();
    });

await builder.RunConsoleAsync();