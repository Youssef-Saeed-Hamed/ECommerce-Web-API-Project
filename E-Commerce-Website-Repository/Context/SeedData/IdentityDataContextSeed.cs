using E_Commerce_Website_Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Context.SeedData
{
	public static class IdentityDataContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new ApplicationUser
				{
					UserName = "YoussefSaeed22",
					Email = "Youssef@gnail.com",
					DisplayName = "Youssef Saeed",
					Address = new Address
					{
						Countery = "Egypt",
						City = "Cairo",
						State = "Dokki",
						PostalCode = "12345",
						Street = "11"
					}
				};
				await userManager.CreateAsync(user , "You@1234");
			}
		}
	}
}
