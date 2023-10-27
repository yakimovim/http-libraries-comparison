using Microsoft.Extensions.DependencyInjection;

namespace PlainHttpClient.Scenarios
{
    internal class Several
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            var response = await client.GetRandom();

            Console.WriteLine(string.Join(", ", response));
        }
    }
}
