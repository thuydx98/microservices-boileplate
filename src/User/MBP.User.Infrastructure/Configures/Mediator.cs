using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace MBP.User.Infrastructure.Configures
{
	public static class Mediator
	{
		public static IServiceCollection AddMediator(this IServiceCollection services)
		{
			return services.AddMediatR(Assembly.GetExecutingAssembly());
		}
	}
}
