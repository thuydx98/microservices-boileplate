using IdentityServer4.Services;
using MBP.Identity.Data;
using MBP.Identity.Data.Entities;
using MBP.Identity.Service.IdentityServer4;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MBP.Identity.Infrastructure.Configures
{
	public static class IdentityServer4
	{
		public static IServiceCollection AddIdentityServer4(this IServiceCollection services)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var connectionString = Environment.GetEnvironmentVariable("IDENTITY_CONNECTION_STRING");
			var signingCredential = Environment.GetEnvironmentVariable("SIGNING_CREDENTIAL");
			var protectKeyPath = Environment.GetEnvironmentVariable("PROTECT_KEY_PATH");
			var tokenLifespanInHours = Environment.GetEnvironmentVariable("CODE_EXPIRE_TIME_IN_HOURS");
			var tokenLifespan = TimeSpan.FromHours(double.Parse(tokenLifespanInHours));

			services.AddTransient<IProfileService, ProfileService>();

			services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(protectKeyPath));

			services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = tokenLifespan);

			services.AddIdentity<MbpUser, MbpRole>(options =>
			{
				options.User.RequireUniqueEmail = false;
				options.Password.RequiredLength = 0;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.User.AllowedUserNameCharacters = "abcdefghiıjklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+'#!/^%{}*";
			})
			.AddEntityFrameworkStores<IdentityDbContext>()
			.AddSignInManager<SignInValidator<MbpUser>>()
			.AddDefaultTokenProviders();

			var serverBuilder = environment == "Development"
				? services.AddIdentityServer().AddDeveloperSigningCredential()
				: services.AddIdentityServer().AddSigningCredential(new X509Certificate2(signingCredential));

			serverBuilder.AddOperationalStore(options =>
			{
				options.EnableTokenCleanup = true;
				options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString);
			});

			serverBuilder.AddConfigurationStore(options =>
			{
				options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString);
			});

			serverBuilder.AddAspNetIdentity<MbpUser>();

			return services;
		}
	}
}
