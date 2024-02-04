using N2.DelegateHandlers.Models;

namespace N2.DelegateHandlers.Brokers;

public interface IIdentityApiBroker
{
    ValueTask<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}