using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using MBP.Contracts.User;
using MBP.Identity.Data;
using MBP.Identity.Data.Entities;
using MBP.Identity.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MBP.Identity.Infrastructure.Migrations
{
	public static class SeedData
	{
		public static async Task<IHost> UpdateSeedDataAsync(this IHost host)
		{
			var provider = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;

			provider.GetRequiredService<IdentityContext>().Database.Migrate();
			provider.GetRequiredService<ConfigurationContext>().Database.Migrate();
			provider.GetRequiredService<PersistedGrantContext>().Database.Migrate();

			var context = provider.GetRequiredService<ConfigurationContext>();
			if (!context.Clients.Any())
			{
				var clients = new List<Client>()
				{
					new Client
					{
						ClientName = "Web Portal",
						ClientId = ClientID.WEB_PORTAL,
						Description = "Web Portal for administrators",
						AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
						ClientSecrets = new Secret[]
						{
							new Secret("I/hHiZKtYVT5U0C303fr4fwC41aj0vuteFXh1RvS6T8=")	//a21zQDIwMjA=
						},
						AccessTokenType = AccessTokenType.Jwt,
						AllowAccessTokensViaBrowser = true,
						AccessTokenLifetime = 86400,
						AllowOfflineAccess = true,
						RequireClientSecret = true,
						AlwaysIncludeUserClaimsInIdToken = true,
						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							IdentityServerConstants.StandardScopes.Phone,
							IdentityServerConstants.StandardScopes.OfflineAccess,
						},
						IdentityTokenLifetime = 86400,
						AlwaysSendClientClaims = true,
					},
					new Client
					{
						ClientName = "Web Blog",
						ClientId = ClientID.WEB_BLOG,
						Description = "Website for blog",
						AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, "external" },
						AccessTokenType = AccessTokenType.Jwt,
						AllowAccessTokensViaBrowser = true,
						AccessTokenLifetime = 86400,
						AllowOfflineAccess = true,
						RequireClientSecret = false,
						AlwaysIncludeUserClaimsInIdToken = true,
						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							IdentityServerConstants.StandardScopes.Phone,
							IdentityServerConstants.StandardScopes.OfflineAccess,
						},
						IdentityTokenLifetime = 86400,
						AlwaysSendClientClaims = true,
					}
				};

				foreach (var client in clients)
				{
					context.Clients.Add(client.ToEntity());
				}

				await context.SaveChangesAsync();
			}

			if (!context.ApiResources.Any())
			{
				var apiResources = new List<ApiResource>()
				{
					new ApiResource("user-api")
					{
						Name = "user-api",
						Description="User API Resource",
						DisplayName = "User Resource",
					},
				};

				foreach (var apiResource in apiResources)
				{
					context.ApiResources.Add(apiResource.ToEntity());
				}

				context.SaveChanges();
			}
			if (!context.IdentityResources.Any())
			{
				var identityResources = new List<IdentityResource>()
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Profile(),
					new IdentityResources.Phone(),
				};

				foreach (var identityResource in identityResources)
				{
					context.IdentityResources.Add(identityResource.ToEntity());
				}

				await context.SaveChangesAsync();
			}

			await UpdateAdminAsync(provider);

			return host;
		}

		private static async Task UpdateAdminAsync(IServiceProvider services)
		{
			var userManager = services.GetRequiredService<UserManager<MbpUser>>();
			var admin = await userManager.FindByNameAsync("Admin");
			if (admin == null)
			{
				admin = new MbpUser
				{
					UserName = "Admin",
					FullName = "Admin",
					Email = "admin@gmail.com",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					Status = Status.ACTIVE,
				};

				await userManager.CreateAsync(admin, "Pass@123#");
			}
		}
	}
}
