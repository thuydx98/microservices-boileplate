using MBP.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MBP.Identity.Infrastructure.Configures
{
	public static class Databases
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services)
		{
			var connectionString = Environment.GetEnvironmentVariable("IDENTITY_CONNECTION_STRING");
			var configurationConnectionString = Environment.GetEnvironmentVariable("IDENTITY_CONFIGURATION_CONNECTION_STRING");

			services.AddDbContext<IdentityContext>(options => options.UseNpgsql(connectionString));
			services.AddDbContext<ConfigurationContext>(options => options.UseNpgsql(configurationConnectionString));
			services.AddDbContext<PersistedGrantContext>(options => options.UseNpgsql(configurationConnectionString));

			return services;
		}
	}
}
