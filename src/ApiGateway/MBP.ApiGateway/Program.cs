using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace MBP.ApiGateway
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(builder =>
				{
					builder.UseKestrel()
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseStartup<Startup>();
				});
	}
}
