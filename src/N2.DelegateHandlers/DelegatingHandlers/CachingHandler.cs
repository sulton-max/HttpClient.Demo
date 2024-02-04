using Microsoft.Extensions.Caching.Memory;

namespace N2.DelegateHandlers.DelegatingHandlers;

public class CachingHandler(IMemoryCache memoryCache) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Method != HttpMethod.Get) 
            return await base.SendAsync(request, cancellationToken);
        
        // Generate a cache key based on the request URL
        var cacheKey = request.RequestUri!.ToString();
        if (memoryCache.TryGetValue(cacheKey, out string? responseBody))
            return new HttpResponseMessage
            {
                Content = new StringContent(responseBody!)
            };

        // If not cached, send the request
        var response = await base.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        memoryCache.Set(cacheKey, responseBody, TimeSpan.FromSeconds(30));
        
        // Return the response
        var clonedResponse = await CloneHttpResponseMessage(response);
        return clonedResponse;
    }
    
    private static async Task<HttpResponseMessage> CloneHttpResponseMessage(HttpResponseMessage response)
    {
        var clonedResponse = new HttpResponseMessage(response.StatusCode)
        {
            Content = await CloneHttpContent(response.Content),
            ReasonPhrase = response.ReasonPhrase,
            RequestMessage = response.RequestMessage,
            Version = response.Version
        };

        foreach (var header in response.Headers)
            clonedResponse.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clonedResponse;
    }

    private static async Task<HttpContent> CloneHttpContent(HttpContent content)
    {
        var memoryStream = new MemoryStream();
        await content.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var clonedContent = new StreamContent(memoryStream);
        foreach (var header in content.Headers)
        {
            clonedContent.Headers.Add(header.Key, header.Value);
        }

        return clonedContent;
    }
}