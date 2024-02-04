using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N2.DelegateHandlers.Brokers;

namespace N2.DelegateHandlers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IIdentityApiBroker identityApiBroker) : ControllerBase
{
    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var user = await identityApiBroker.GetUserByIdAsync(userId, cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }
}