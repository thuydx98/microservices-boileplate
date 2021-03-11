using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MBP.Common.Extensions;
using MBP.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MBP.Identity.Service.IdentityServer4
{
	public class ProfileService : IProfileService
	{
		private readonly IUserClaimsPrincipalFactory<MbpUser> _claimsFactory;
		private readonly UserManager<MbpUser> _userManager;

		public ProfileService(IUserClaimsPrincipalFactory<MbpUser> claimsFactory, UserManager<MbpUser> userManager)
		{
			_claimsFactory = claimsFactory;
			_userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await _userManager.FindByIdAsync(sub);
			var principal = await _claimsFactory.CreateAsync(user);
			var claims = principal.Claims.ToList();
			var roles = await _userManager.GetRolesAsync(user);

			claims = claims.Where(c => context.RequestedClaimTypes.Contains(c.Type)).ToList();

			if (user.FullName.IsNotEmpty())
			{
				claims.Add(new Claim(JwtClaimTypes.GivenName, user.FullName));
			}
			if (user.Email.IsNotEmpty())
			{
				claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
			}

			foreach (var role in roles)
			{
				claims.Add(new Claim(JwtClaimTypes.Role, role));
			}

			context.IssuedClaims = claims;
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			context.IsActive = true;

			return Task.CompletedTask;
		}
	}
}
