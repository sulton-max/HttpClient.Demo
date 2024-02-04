using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using N2.DelegateHandlers.Models;

namespace N2.DelegateHandlers.DelegatingHandlers;

public class AuthenticationHandler(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get access token
        var cacheKey = "AccessToken";
        if (!memoryCache.TryGetValue(cacheKey, out string? accessToken))
        {
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync(
                new Uri(new Uri(request.RequestUri!.GetLeftPart(UriPartial.Authority)), "api/auth/sign-in").ToString(),
                new SignInDetails
                {
                    EmailAddress = "john.doe@gmail.com",
                    Password = "JohnDoe_1"
                },
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            accessToken = await response.Content.ReadAsStringAsync(cancellationToken);
            memoryCache.Set(cacheKey, accessToken, TimeSpan.FromSeconds(30));
        }

        // Add authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Proceed with the request
        return await base.SendAsync(request, cancellationToken);
    }
}