using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using PlainConfig = PlainHttpClient.Config;
using PlainService = PlainHttpClient.IServiceClient;
using RefitConfig = RefitClient.Config;
using RefitService = RefitClient.IServiceClient;
using RestSharpConfig = RestSharpClient.Config;
using RestSharpService = RestSharpClient.IServiceClient;

namespace Performance
{
    [SimpleJob(RuntimeMoniker.Net70)]
    [MinColumn, MaxColumn]
    public class HttpClientsComparison
    {
        private PlainService _plainService;
        private IServiceScope _plainScope;
        private RefitService _refitService;
        private IServiceScope _refitScope;
        private RestSharpService _restSharpService;
        private IServiceScope _restSharpScope;

        [GlobalSetup]
        public void Setup()
        {
            _plainScope = PlainConfig.GetScope();

            _plainService = _plainScope.ServiceProvider.GetRequiredService<PlainService>();

            _refitScope = RefitConfig.GetScope();

            _refitService = _refitScope.ServiceProvider.GetRequiredService<RefitService>();

            _restSharpScope = RestSharpConfig.GetScope();

            _restSharpService = _restSharpScope.ServiceProvider.GetRequiredService<RestSharpService>();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _plainScope?.Dispose();

            _refitScope?.Dispose();

            _restSharpScope?.Dispose();
        }

        [Benchmark]
        public async Task Plain()
        {
            await _plainService.GetHello();
        }

        [Benchmark]
        public async Task Refit()
        {
            await _refitService.GetHello();
        }

        [Benchmark]
        public async Task RestSharp()
        {
            await _restSharpService.GetHello();
        }
    }
}
