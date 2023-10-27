using Refit;

namespace RefitClient
{
    public interface IServiceClient
    {
        [Get("/data/hello")]
        Task<string> GetHello();

        [Get("/data/long")]
        Task<string> GetLong(CancellationToken cancellationToken);

        [Get("/data/tee")]
        Task GetTee();

        [Get("/data/rnd")]
        Task<IApiResponse> GetRandom();
    }
}
