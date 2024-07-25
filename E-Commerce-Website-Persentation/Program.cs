using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models.Identity;
using E_Commerce_Website_Persentation.Errors;
using E_Commerce_Website_Persentation.Extensions;
using E_Commerce_Website_Repository.Context;
using E_Commerce_Website_Repository.Context.SeedData;
using E_Commerce_Website_Repository.Repositories;
using E_Commerce_Website_Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce_Website_Persentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState.Where(m => m.Value.Errors.Any())
					.SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage);

					return new BadRequestObjectResult(new ApiVaidationErrorResponse()
					{
						Errors = errors
					});
				};
			});
			var app = builder.Build();
            await DbIntializer.IntializeDb(app);
			app.UseMiddleware<CustomeExceptionHandller>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }

       
    }
}
 