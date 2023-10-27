using Microsoft.Extensions.DependencyInjection;

namespace RefitClient.Scenarios
{
    internal class Timeout
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            using var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var response = await Helper.WithTimeout(
                    TimeSpan.FromSeconds(5),
                    cancellationTokenSource.Token,
                    client.GetLong);

                Console.WriteLine(response);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Timeout");
            }
        }
    }
}
