using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManger;
		private readonly SignInManager<ApplicationUser> _signInManager; 
		private readonly ITokenService _tokenService;

		public UserService(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager
			, ITokenService tokenService)
		{
			_userManger = userManger;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public async Task<UserDto?> LoginAsync(LoginDto dto)
		{
			var user = await _userManger.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
				var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
				if(result.Succeeded)
				{
					return new UserDto
					{
						Email = user.Email,
						DisplayName = user.DisplayName,
						Token = _tokenService.GenerateToken(user)
					};
				}
            }
			return null;
        }

		public async Task<UserDto> RegisterAsync(RegisterDto dto)
		{
			var user = await _userManger.FindByEmailAsync(dto.Email);
			if (user is not null) throw new Exception("This Email Is Already Exist !");
			var appUser = new ApplicationUser
			{
				UserName = dto.DispayName,
				Email = dto.Email,
				DisplayName = dto.DispayName
			};
			var result = await _userManger.CreateAsync(appUser , dto.Password);
			if (!result.Succeeded) throw new Exception("Incorrect Password");
			return new UserDto
			{
				DisplayName = appUser.DisplayName,
				Email = appUser.Email,
				Token = _tokenService.GenerateToken(appUser)
			};
		}
	}
}
