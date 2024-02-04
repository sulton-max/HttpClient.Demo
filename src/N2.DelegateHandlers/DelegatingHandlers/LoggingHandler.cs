namespace N2.DelegateHandlers.DelegatingHandlers;

public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Log the request information
        logger.LogInformation("Sending request to {Url}", request.RequestUri);

        // Send the request
        var response = await base.SendAsync(request, cancellationToken);

        // Log the response information
        logger.LogInformation("Received response with status code {StatusCode} from {Url}", response.StatusCode, request.RequestUri);

        return response;
    }
}