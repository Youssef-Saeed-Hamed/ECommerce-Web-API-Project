using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public  string GenerateToken(ApplicationUser user)
		{
			var Claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email , user.Email),
				new Claim(ClaimTypes.Name , user.DisplayName)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

			var Cerdentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


			var tokenDescriptors = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(Claims),
				Issuer = _configuration["Token:Issuer"],
				Audience = _configuration["Token:Audiance"],
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = Cerdentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptors);
			return tokenHandler.WriteToken(token);
		}
	}
}
