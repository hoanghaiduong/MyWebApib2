using System.Data;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWebApi.Application.Interfaces;
using MyWebApi.Application.Middlewares;
using MyWebApi.Application.Services;
using MyWebApi.Infrastructure.Models;
using Newtonsoft.Json;
namespace MyWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy(name: MyAllowSpecificOrigins,
        //                     policy =>
        //                     {

        //                         policy.AllowAnyOrigin()//access control allow origin
        //                             .AllowAnyMethod()//GET POST PUT DELETE
        //                             .AllowAnyHeader();//access control allow header
        //                     });
        // });

        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        //jwt config
        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();

        var key = Encoding.ASCII.GetBytes(appSettings.AccessTokenSecret);
        // Add services to the container.
        Console.WriteLine(appSettings.AccessTokenSecret);


        builder.Services.AddScoped<IDbConnection>(cnn => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddStackExchangeRedisCache(c =>
        {
            c.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
            c.InstanceName = "MyWebApi_";
        });
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IHotelService, HotelService>();
        builder.Services.AddScoped<IRoomTypeSerivce, RoomTypeService>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IFileUploadService, FileUploadService>();
        builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
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


        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature != null)
                {
                    var errorResponse = new { Message = "Đã xảy ra lỗi", Detail = errorFeature.Error.Message };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                }
            });
        });

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