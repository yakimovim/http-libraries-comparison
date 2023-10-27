using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;
using Refit;
using RefitClient;
using RefitClient.Scenarios;
using Spectre.Console;
using Policy = RefitClient.Scenarios.Policy;
using Timeout = RefitClient.Scenarios.Timeout;

var policy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(response => (int)response.StatusCode == 418)
    .RetryAsync(3, (_, retry) =>
    {
        AnsiConsole.MarkupLine($"[fuchsia]Retry number {retry}[/]");
    });

var services = new ServiceCollection();

services
    .AddRefitClient<IServiceClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("http://localhost:5001");
    })
    .AddPolicyHandler(policy);

services.AddScoped<RefitClientFactory>();

services.AddTransient<LoggingHandler>();
services.ConfigureAll<HttpClientFactoryOptions>(options =>
{
    options.HttpMessageHandlerBuilderActions.Add(builder =>
    {
        builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<LoggingHandler>());
    });
});


IServiceProvider provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

provider = scope.ServiceProvider;

while(true)
{
    var choice = AnsiConsole.Prompt(
        (new SelectionPrompt<string>())
            .Title("What do you want to do with Refit client?")
            .AddChoices(
                "Simple",
                "Several",
                "Cancel",
                "Base Address",
                "Timeout",
                "Policy",
                "Exit"
            )
    );

    AnsiConsole.Clear();

    switch (choice)
    {
        case "Simple":
            {
                await Simple.Execute(provider);
                break;
            }
        case "Several":
            {
                await Several.Execute(provider);
                break;
            }
        case "Cancel":
            {
                await Cancel.Execute(provider);
                break;
            }
        case "Base Address":
            {
                await BaseAddress.Execute(provider);
                break;
            }
        case "Timeout":
            {
                await Timeout.Execute(provider);
                break;
            }
        case "Policy":
            {
                await Policy.Execute(provider);
                break;
            }
        default:
            {
                return;
            }
    }
}