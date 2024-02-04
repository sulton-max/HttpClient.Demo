using N2.DelegateHandlers.Models;

namespace N2.DelegateHandlers.Brokers;

public class IdentityApiBroker(HttpClient httpClient) : IIdentityApiBroker
{
    public async ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<User>($"api/auth/users/{userId}", cancellationToken: cancellationToken);
    }
}