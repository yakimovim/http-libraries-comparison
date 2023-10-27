using Microsoft.Extensions.DependencyInjection;

namespace RefitClient.Scenarios
{
    internal class Cancel
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            using var cancellationTokenSource = new CancellationTokenSource();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            client.GetLong(cancellationTokenSource.Token)
                .ContinueWith(task =>
                {
                    Console.WriteLine(task.Result);
                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            Console.WriteLine("Press any key to cancel...");
            Console.ReadKey();

            cancellationTokenSource.Cancel();
        }
    }
}
