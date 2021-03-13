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

			services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

			return services;
		}
	}
}
