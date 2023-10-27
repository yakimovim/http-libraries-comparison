using Microsoft.Extensions.DependencyInjection;

namespace RestSharpClient.Scenarios
{
    internal class Policy
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            try
            {
                await client.GetTee();

                Console.WriteLine("Tee is ready");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

    }
}
