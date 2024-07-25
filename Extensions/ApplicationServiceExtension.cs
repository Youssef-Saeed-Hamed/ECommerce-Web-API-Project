using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Repository.Context;
using E_Commerce_Website_Repository.Repositories;
using E_Commerce_Website_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce_Website_Persentation.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddDbContext<DataContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("MyConnection"));
			});
			services.AddDbContext<IdentityDataContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("MyIdentityConnection"));
			});

			

			services.AddScoped<IProductServices, ProductServices>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IBasketRepsitory, BasketRepository>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IPaymentService, PaymentService>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<ICashService, CashService>();
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddSingleton<IConnectionMultiplexer>(opt =>
			{
				var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
				return ConnectionMultiplexer.Connect(config);
			});
			

			return services;
		}
	}
}
