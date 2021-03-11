using MBP.Common.ApiResult;
using MBP.Common.ApiResult.ErrorResult;
using MBP.Common.Constants;
using MBP.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MBP.Contracts.Middlewares.Authentication
{
	public class Authenticate : Attribute, IAuthorizationFilter
	{
		/// <summary>  
		/// Authenticate the user
		/// </summary>  
		/// <returns></returns> 
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			// Allow internal call by access path api
			if (context.HttpContext.Request.Path.Value.Contains("/api"))
			{
				return;
			}

			var header = context.HttpContext.Request.Headers;
			var userId = header[HeaderInfo.USER_ID].ToString();
			if (userId.IsNotEmpty())
			{
				return;
			}

			HandleAuthorizeFailAsync(context);
		}

		/// <summary>
		/// Add response (Unauthorized) when authenticate failed
		/// </summary>
		/// <returns></returns>
		private void HandleAuthorizeFailAsync(AuthorizationFilterContext context)
		{
			var unauthorized = new ApiJsonResult<object>((int)HttpCode.Unauthorized, HttpCode.Unauthorized.GetDescription());

			context.HttpContext.Response.StatusCode = (int)HttpCode.Unauthorized;
			context.Result = new JsonResult(unauthorized);
		}
	}
}
