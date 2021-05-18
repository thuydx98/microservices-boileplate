using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace MBP.ApiGateway
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile($"ocelot.{env.EnvironmentName}.json")
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddOcelot(Configuration);

			services.AddAuthentication().AddIdentityServerAuthentication("identity-server", options =>
			{
				options.Authority = Environment.GetEnvironmentVariable("IDENTITY_SERVICE_URL");
				options.SupportedTokens = SupportedTokens.Jwt;
				options.RequireHttpsMetadata = false;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("OK");
				});
			});

			app.UseOcelot().Wait();
		}
	}
}
