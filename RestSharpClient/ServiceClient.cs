using RestSharp;

namespace RestSharpClient
{
    public interface IServiceClient
    {
        Task<string?> GetHello();
        Task<string?> GetHelloFrom(string baseAddress);
        Task<string?> GetLong(CancellationToken cancellationToken);
        Task<string?> GetLongWithTimeout(TimeSpan timeout, CancellationToken cancellationToken = default);
        Task GetTee();
        Task<IReadOnlyList<int>> GetRandom();
    }

    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        public ServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetHello()
        {
            var client = new RestClient(_httpClient);

            var request = new RestRequest("http://localhost:5001/data/hello");

            return await client.GetAsync<string>(request);
        }

        public async Task<string?> GetHelloFrom(string baseAddress)
        {
            var client = new RestClient(_httpClient);

            var request = new RestRequest($"{baseAddress.TrimEnd('/')}/data/hello");

            return await client.GetAsync<string>(request);
        }

        public async Task<string?> GetLong(CancellationToken cancellationToken)
        {
            var client = new RestClient(_httpClient);

            var request = new RestRequest("http://localhost:5001/data/long") {  };

            return await client.GetAsync<string>(request, cancellationToken);
        }

        public async Task<string?> GetLongWithTimeout(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = new RestClient(_httpClient, new RestClientOptions { MaxTimeout = (int)timeout.TotalMilliseconds });

                var request = new RestRequest("http://localhost:5001/data/long");

                return await client.GetAsync<string>(request, cancellationToken);
            }
            catch (TimeoutException)
            {
                return "Timeout";
            }
        }

        public async Task GetTee()
        {
            var client = new RestClient(_httpClient);

            var request = new RestRequest("http://localhost:5001/data/tee");

            await client.GetAsync<string>(request);
        }

        public async Task<IReadOnlyList<int>> GetRandom()
        {
            var client = new RestClient(_httpClient);

            var request = new RestRequest("http://localhost:5001/data/rnd");

            var returnCodes = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                var response = await client.ExecuteAsync(request);

                returnCodes.AddLast((int)response.StatusCode);
            }

            return returnCodes.ToArray();
        }
    }
}
