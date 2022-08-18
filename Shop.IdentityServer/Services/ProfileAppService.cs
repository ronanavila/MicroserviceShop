using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shop.IdentityServer.Data;
using System.Security.Claims;

namespace Shop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfileAppService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //Busca ID no IdentityServer
        string userId = context.Subject.GetSubjectId();

        //busca usuario pelo ID
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        //Cria ClaimsPrincipal para o usuario
        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        //cria colecao de claims e adiciona nome e sobrenome
        List<Claim> claims = userClaims.Claims.ToList();
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

        if (_userManager.SupportsUserRole)
        {
            //busca lista de roles
            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));

                if (_roleManager.SupportsRoleClaims)
                {
                    IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                    if (identityRole is not null)
                        claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                }
            }

            context.IssuedClaims = claims;
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string userId = context.Subject.GetSubjectId();

        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        context.IsActive = user is not null;
    }
}
