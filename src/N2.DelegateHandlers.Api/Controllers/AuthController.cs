using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using N2.DelegateHandlers.Api.DataContexts;
using N2.DelegateHandlers.Api.Models;
using N2.DelegateHandlers.Api.Settings;

namespace N2.DelegateHandlers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext appDbContext, IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDetails signInDetails, CancellationToken cancellationToken)
    {
        var users = appDbContext.Users.ToList();
        var user = await appDbContext.Users.FirstOrDefaultAsync(
            user => user.EmailAddress == signInDetails.EmailAddress && user.Password == signInDetails.Password,
            cancellationToken
        ) ?? throw new AuthenticationException("Sign in failed. Contact support");

        return Ok(GenerateToken(user));
    }

    [Authorize]
    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        // Create SigningCredentials using the security key and sha 256 algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create and return a new JwtSecurityToken with the specified parameters.
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: new List<Claim>
            {
                new(ClaimTypes.Email, user.EmailAddress),
                new(ClaimTypes.Role, RoleType.User.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}