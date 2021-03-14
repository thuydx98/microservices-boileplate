using MBP.Identity.Infrastructure.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MBP.Identity
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//CreateHostBuilder(args).Build().Run();
			CreateHostBuilder(args).Build().UpdateSeedDataAsync().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
