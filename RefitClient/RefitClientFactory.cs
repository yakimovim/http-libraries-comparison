using Refit;

namespace RefitClient
{
    internal class RefitClientFactory
    {
        public T GetClientFor<T>(string baseUrl)
        {
            RefitSettings settings = new RefitSettings();
            settings.HttpMessageHandlerFactory = () => new LoggingHandler
            {
                InnerHandler = new HttpClientHandler()
            };

            return RestService.For<T>(baseUrl, settings);
        }
    }
}
