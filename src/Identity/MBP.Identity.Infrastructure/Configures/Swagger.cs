using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MBP.Identity.Infrastructure.Configures
{
	public static class Swagger
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MBP.Identity", Version = "v1" });
			});

			return services;
		}

		public static IApplicationBuilder UseDeveloperSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MBP.Identity v1"));

			return app;
		}
	}
}
