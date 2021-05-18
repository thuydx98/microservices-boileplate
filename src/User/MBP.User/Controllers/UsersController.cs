using Microsoft.AspNetCore.Mvc;
using System;

namespace MBP.User.Controllers
{
	[ApiController]
	public class UsersController : ControllerBase
	{
		[HttpGet("api/users")]
		public IActionResult Get()
		{
			return new ObjectResult(new
			{
				Now = DateTime.Now,
				UTC = DateTime.UtcNow,
				NameName = DateTime.Today,
			});
		}

		[HttpPost("api/users")]
		public IActionResult Post(Request request)
		{
			return new ObjectResult(request);
		}
	}

	public class Request
	{
		public TEST? TestEnum { get; set; }
	}

	public enum TEST { A, B, C }
}
