namespace PlainHttpClient
{
    public interface IServiceClient
    {
        Task<string> GetHello();

        Task<string> GetHelloFrom(string baseAddress);

        Task<string> GetLong(CancellationToken cancellationToken);

        Task<string> GetLongWithTimeout(TimeSpan timeout, CancellationToken cancellationToken = default);

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

        public async Task<string> GetHello()
        {
            var response = await _httpClient.GetAsync("http://localhost:5001/data/hello");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetHelloFrom(string baseAddress)
        {
            var response = await _httpClient.GetAsync($"{baseAddress.TrimEnd('/')}/data/hello");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetLong(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("http://localhost:5001/data/long", cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetLongWithTimeout(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            // _httpClient.Timeout = timeout;

            try
            {
                using var tokenSource = new CancellationTokenSource(timeout);

                using var registration = cancellationToken.Register(tokenSource.Cancel);

                var response = await _httpClient.GetAsync("http://localhost:5001/data/long", tokenSource.Token);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException)
            {
                return "Timeout";
            }
        }

        public async Task GetTee()
        {
            var response = await _httpClient.GetAsync("http://localhost:5001/data/tee");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IReadOnlyList<int>> GetRandom()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5001/data/rnd");

            var returnCodes = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                var response = await _httpClient.SendAsync(request);

                returnCodes.AddLast((int)response.StatusCode);
            }

            return returnCodes.ToArray();
        }
    }
}
