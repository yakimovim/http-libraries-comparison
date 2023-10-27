using Spectre.Console;

namespace RefitClient
{
    // https://stackoverflow.com/questions/64103349/add-http-message-handler-to-all-http-clients

    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                AnsiConsole.MarkupLine($"[yellow]Sending {request.Method} request to {request.RequestUri}[/]");

                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[yellow]{request.Method} request to {request.RequestUri} is failed: {ex.Message}[/]");
                throw;
            }
            finally
            {
                AnsiConsole.MarkupLine($"[yellow]{request.Method} request to {request.RequestUri} is finished[/]");
            }
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                AnsiConsole.MarkupLine($"[yellow]Sending {request.Method} request to {request.RequestUri}[/]");

                return base.Send(request, cancellationToken);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[yellow]{request.Method} request to {request.RequestUri} is failed: {ex.Message}[/]");
                throw;
            }
            finally
            {
                AnsiConsole.MarkupLine($"[yellow]{request.Method} request to {request.RequestUri} is finished[/]");
            }
        }
    }
}
