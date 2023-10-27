using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace RefitClient.Scenarios
{
    internal class BaseAddress
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var factory = provider.GetRequiredService<RefitClientFactory>();

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What server do you want to contact?")
                        .AddChoices(
                            "Server 1",
                            "Server 2",
                            "Exit"
                        )
                );

                var client = choice switch
                {
                    "Server 1" => factory.GetClientFor<IServiceClient>("http://localhost:5001"),
                    "Server 2" => factory.GetClientFor<IServiceClient>("http://localhost:5002"),
                    "Exit" => null,
                    _ => throw new InvalidOperationException()
                };

                if (client == null)
                    return;

                var response = await client.GetHello();

                Console.WriteLine(response);
            }

        }
    }
}
