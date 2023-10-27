using Microsoft.Extensions.DependencyInjection;

namespace RestSharpClient.Scenarios
{
    internal class Timeout
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            var response = await client.GetLongWithTimeout(TimeSpan.FromSeconds(5));

            Console.WriteLine(response);
        }
    }
}
