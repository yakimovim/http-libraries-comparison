namespace RefitClient
{
    internal class Helper
    {
        public static async Task<T> WithTimeout<T>(TimeSpan timeout, CancellationToken cancellationToken, Func<CancellationToken, Task<T>> action)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);

            using var registration = cancellationToken.Register(cancellationTokenSource.Cancel);

            return await action(cancellationTokenSource.Token);
        }
    }
}
