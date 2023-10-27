using Microsoft.Extensions.DependencyInjection;

namespace RefitClient.Scenarios
{
    internal class Policy
    {
        public static async Task Execute(IServiceProvider provider)
        {
            // var client = provider.GetRequiredService<IServiceClient>();

            var client = provider.GetRequiredService<RefitClientFactory>().GetClientFor<IServiceClient>("http://localhost:5001");

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
