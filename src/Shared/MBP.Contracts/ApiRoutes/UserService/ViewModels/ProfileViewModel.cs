using System;

namespace MBP.Contracts.ApiRoutes.UserService.ViewModels
{
	public class ProfileViewModel
	{
		public string Id { get; set; }
		public string[] Permissions { get; set; } = Array.Empty<string>();
	}
}
