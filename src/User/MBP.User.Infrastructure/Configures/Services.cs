using MBP.User.Service.User.Queries;
using MBP.User.Service.User.Queries.GetUserInfo;
using Microsoft.Extensions.DependencyInjection;

namespace MBP.User.Infrastructure.Configures
{
	public static class Services
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddUserServices();

			return services;
		}

		private static void AddUserServices(this IServiceCollection services)
		{
			services.AddService<GetUserInfoQuery, GetUserInfoHandler>();
		}
	}
}
