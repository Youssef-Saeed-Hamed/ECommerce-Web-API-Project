using E_Commerce_Website_Core.Models.Identity;
using E_Commerce_Website_Repository.Context.SeedData;
using E_Commerce_Website_Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website_Persentation.Extensions
{
	public static class DbIntializer
	{
		public static async Task IntializeDb(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					var context = services.GetRequiredService<DataContext>();
					var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
					if ((await context.Database.GetPendingMigrationsAsync()).Any())
					{
						await context.Database.MigrateAsync();
					}
					await DataContextSeed.DataSeedAsync(context);
					await IdentityDataContextSeed.SeedUsersAsync(userManger);

				}
				catch (Exception ex)
				{
					var logger = LoggerFactory.CreateLogger<Program>();
					logger.LogError(ex.Message);
				}
			}
		}
	}
}
