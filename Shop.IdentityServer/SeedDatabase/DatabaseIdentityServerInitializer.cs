using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shop.IdentityServer.Configuration;
using Shop.IdentityServer.Data;
using System.Security.Claims;

namespace Shop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }

        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        if (_userManager.FindByEmailAsync("admin1@admin.com.br").Result is null)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@admin.com.br",
                NormalizedEmail = "ADMIN1@ADMIN.COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (16) 99999-8888",
                FirstName = "Admin1",
                LastName = "Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Dono#2022").Result;
            if (resultAdmin.Succeeded)
            {
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)

                }).Result;
            }
        }

        if (_userManager.FindByEmailAsync("client1@client.com.br").Result is null)
        {
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@client.com.br",
                NormalizedEmail = "CLIENT1@CLIENT.COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (16) 88888-9999",
                FirstName = "Client1",
                LastName = "Client",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult resultClient = _userManager.CreateAsync(client, "Dono#2022").Result;
            if (resultClient.Succeeded)
            {
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                var adminClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)

                }).Result;
            }
        }
    }
}
