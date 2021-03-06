using MBP.Contracts.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MBP.User.Infrastructure.Configures
{
	public static class Middleware
	{
		public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<ExceptionMiddleware>();

			return builder;
		}
	}
}
