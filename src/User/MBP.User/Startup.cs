using MBP.Contracts.Middlewares.Authorization.Configurations;
using MBP.User.Infrastructure.Configures;
using MBP.User.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MBP.User
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMediator();
			services.AddAutoMapper();
			services.AddUnitOfWork();
			services.AddAuthorize();
			services.AddControllers();
			services.AddServices();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MBP.User", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MBP.User v1"));
			}

			app.UseRouting();
			app.UseMiddlewares();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
