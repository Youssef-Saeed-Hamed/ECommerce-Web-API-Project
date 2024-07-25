using E_Commerce_Website_Core.Models.Identity;
using E_Commerce_Website_Repository.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce_Website_Persentation.Extensions
{
	public static class IdentityServicesExtensions
	{
		public static IServiceCollection AddIdentityServices (this IServiceCollection services , IConfiguration configuration)
		{
			services.AddIdentityCore<ApplicationUser>()
				.AddEntityFrameworkStores<IdentityDataContext>()
				.AddSignInManager<SignInManager<ApplicationUser>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(async options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["Token:Issuer"],
						ValidateAudience = true,
						ValidAudience = configuration["Token:Audiance"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration["Token:Key"])),
						ValidateLifetime = true,
					};
					
				});
			return services;
		}
	}
}
