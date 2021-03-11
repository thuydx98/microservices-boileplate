using MBP.Contracts.User;
using MBP.Identity.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBP.Identity.Service.IdentityServer4
{
	public class SignInValidator<TUser> : SignInManager<MbpUser>
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public SignInValidator(
			UserManager<MbpUser> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<MbpUser> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<SignInManager<MbpUser>> logger,
			IAuthenticationSchemeProvider schemes,
			IUserConfirmation<MbpUser> confirmation) : 
			base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
			_contextAccessor = contextAccessor;
		}


		/// <summary>
		/// Custom validate when sign in
		/// </summary>
		public override Task<bool> CanSignInAsync(MbpUser user)
		{
			var isConfirmedAccount = user.PhoneNumberConfirmed || user.EmailConfirmed;
			var isActiveAccount = user.Status == Status.ACTIVE;
			var canSignIn = isConfirmedAccount && isActiveAccount;

			return Task.FromResult(canSignIn);
		}

		private IDictionary<string, string> GetRequestForm()
		{
			var form = _contextAccessor.HttpContext.Request.Form;
			var result = form.ToDictionary(x => x.Key, x => x.Value.ToString());

			return result;
		}
	}
}
