using System.Security.Claims;
using FrightForce.Domain.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FrightForce.Infractructure.Services;

public class AppClaimsTransformer : IClaimsTransformation
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AppClaimsTransformer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var upn = principal.FindFirst(ClaimTypes.Upn);
        if (upn == null)
        {
            return principal;
        }

        var user = await _userManager.FindByNameAsync(upn.Value);

        var identity = (ClaimsIdentity)principal.Identity;
        if (user != null && identity is { IsAuthenticated: true })
        {
            var claims = await _userManager.GetClaimsAsync(user);
            identity.AddClaims(claims);

            var roles = await _userManager.GetRolesAsync(user);
            identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        }



        return principal;
    }
}