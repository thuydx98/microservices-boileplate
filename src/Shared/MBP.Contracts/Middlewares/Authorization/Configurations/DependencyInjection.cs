using MBP.Contracts.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MBP.Contracts.Middlewares.Authorization.Configurations
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAuthorize(this IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<Lazy<HttpClient>>();
			services.AddScoped<IAuthorizationHandler, PermissionHandler>();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			foreach (var permission in Permisison.ListPermission)
			{
				services.AddAuthorization(options =>
					options.AddPolicy(permission, policy =>
						policy.Requirements.Add(new PermissionRequirement(permission))));
			}

			return services;
		}
	}
}
