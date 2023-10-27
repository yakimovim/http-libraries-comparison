using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace RefitClient
{
    public static class Config
    {
        public static IServiceScope GetScope()
        {
            var services = new ServiceCollection();

            services
                .AddRefitClient<IServiceClient>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("http://localhost:5001");
                });

            IServiceProvider provider = services.BuildServiceProvider();

            return provider.CreateScope();
        }
    }
}
