using MBP.Contracts.Configures;
using MBP.Contracts.Middlewares.Authorization.Configures;
using MBP.Identity.Infrastructure.Configures;
using MBP.User.Infrastructure.Configures;
using MBP.User.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace MBP.User
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMediator();
			services.AddAutoMapper();
			services.AddUnitOfWork();
			//services.AddAuthorize();
			services.AddServices();
			services.AddSwagger();

			services.AddHealthChecks();
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDeveloperSwagger();
			}

			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseRouting();
			app.UseMiddlewares();
			app.UseLocalization().UseHealthChecks();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
