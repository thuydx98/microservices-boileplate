using MBP.Identity.Infrastructure.Configures;
using MBP.Contracts.Configures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MBP.Identity
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddHealthChecks();
			services.AddDbContext().AddIdentityServer4().AddSwagger();
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
			app.UseIdentityServer();
			app.UseLocalization().UseHealthChecks();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
