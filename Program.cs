using System.Data;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWebApi.Application.Interfaces;
using MyWebApi.Application.Services;
using MyWebApi.Infrastructure.Models;
namespace MyWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        //jwt config
        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();

        var key = Encoding.ASCII.GetBytes(appSettings.AccessTokenSecret);
        // Add services to the container.
        Console.WriteLine(appSettings.AccessTokenSecret);


        builder.Services.AddScoped<IDbConnection>(cnn => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IHotelService, HotelService>();
        builder.Services.AddScoped<IRoomTypeSerivce, RoomTypeService>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IFileUploadService, FileUploadService>();

        builder.Services.AddEndpointsApiExplorer();
        //thêm security cho swagger
        builder.Services.AddSwaggerGen((config) =>
        {
            //AddSecurityDefinition
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer ey...')",
            });

            //AddSecurityRequirement
            config.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                   Array.Empty<string>()
                }
            });
        });
        //add authentication b7
        builder.Services.AddAuthentication(ops =>
        {
            ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(ops =>
        {
            ops.SaveToken = true;// lưu token
            ops.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = appSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = appSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
        builder.Services.AddAuthorization();
        //add authentication b7

        builder.Services.AddControllers();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //middleware
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();

        app.MapControllers();
        app.Run();
    }


}