using Microsoft.Extensions.DependencyInjection;

namespace RefitClient.Scenarios
{
    internal class Several
    {
        public static async Task Execute(IServiceProvider provider)
        {
            var client = provider.GetRequiredService<IServiceClient>();

            var returnCodes = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                var response = await client.GetRandom();

                returnCodes.AddLast((int)response.StatusCode);
            }

            Console.WriteLine(string.Join(", ", returnCodes));
        }
    }
}
