using IdentityServer4;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using MBP.Identity.Data;
using MBP.Identity.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBP.Identity.Infrastructure.Migrations
{
	public static class SeedData
	{
		public static IHost UpdateSeedDataAsync(this IHost host)
		{
			var provider = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;

			provider.GetRequiredService<IdentityContext>().Database.Migrate();
			provider.GetRequiredService<ConfigurationContext>().Database.Migrate();
			//provider.GetRequiredService<PersistedGrantContext>().Database.Migrate();

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
						AllowOfflineAccess = false,
						RequireClientSecret = false,
						AlwaysIncludeUserClaimsInIdToken = true,
						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							IdentityServerConstants.StandardScopes.Phone,
						}
					},
					new Client
					{
						ClientName = "Web Blog",
						ClientId = ClientID.WEB_BLOG,
						Description = "Website for blog",
						AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, "external" },
						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							IdentityServerConstants.StandardScopes.Phone,
						},
						AccessTokenType = AccessTokenType.Jwt,
						AlwaysIncludeUserClaimsInIdToken = true,
						AccessTokenLifetime = 86400,
						AllowOfflineAccess = true,
						IdentityTokenLifetime = 86400,
						AlwaysSendClientClaims = true,
						Enabled = true,
					}
				};

				foreach (var client in clients)
				{
					context.Clients.Add(client.ToEntity());
				}

				context.SaveChanges();
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

				context.SaveChanges();
			}

			return host;
		}
	}
}
