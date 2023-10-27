using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace PlainHttpClient.Scenarios
{
    internal class BaseAddress
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            while(true)
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

                var response = choice switch
                {
                    "Server 1" => await client.GetHelloFrom("http://localhost:5001"),
                    "Server 2" => await client.GetHelloFrom("http://localhost:5002"),
                    "Exit" => null,
                    _ => throw new InvalidOperationException()
                };

                if (response == null)
                    return;

                Console.WriteLine(response);
            }

        }
    }
}
