
using HouseUnitAPI.Data;
using HouseUnitAPI.Helpers.Extensions;
using HouseUnitAPI.Middleware;
using HouseUnitAPI.Repositories;
using HouseUnitAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Text;
using System.Text.Json.Serialization;

namespace HouseUnitAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Configure NLog
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Logging.AddNLog();
            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //Add DB Context
            builder.Services.AddDbContext<HouseUnitDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Add repositories and services
            builder.Services.AddScoped<IHouseUnitRepository, HouseUnitRepository>();
            builder.Services.AddScoped<IHouseUnitService, HouseUnitService>();

            //Add logging
            builder.Services.AddLogging();

            // CORS Configuration
            var allowedOrigins = builder.Configuration.GetSection("AllowedHosts").Value;
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(allowedOrigins.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Add custom middlewares
            builder.Services.AddCustomAuthentication(builder.Configuration)
                            .AddCustomAuthorization()
                            .AddCustomSwagger(builder.Configuration);

            var app = builder.Build();

            //Implement logging middleware
            app.UseMiddleware<LoggingMiddleware>();

            //Implement global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCustomSwaggerUI(builder.Configuration);


            app.MapControllers();

            app.Run();
        }
    }
}
