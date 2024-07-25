using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Persentation.Errors;
using E_Commerce_Website_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website_Persentation.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<ActionResult<UserDto>> Login (LoginDto input)
		{
			var user = await _userService.LoginAsync(input);
			return user is not null ? Ok(user) : Unauthorized(new APIResponse(401, "In Correct Email Or Password"));
		}
		[HttpPost]
		
		public async Task<ActionResult<UserDto>> Register (RegisterDto input)
		{
			return Ok (await _userService.RegisterAsync(input));
		}
	}
}
