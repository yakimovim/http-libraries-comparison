using Microsoft.Extensions.DependencyInjection;

namespace PlainHttpClient
{
    public static class Config
    {
        public static IServiceScope GetScope()
        {
            var services = new ServiceCollection();

            services.AddHttpClient();
            services.AddHttpClient<IServiceClient, ServiceClient>();

            IServiceProvider provider = services.BuildServiceProvider();

            return provider.CreateScope();
        }
    }
}
