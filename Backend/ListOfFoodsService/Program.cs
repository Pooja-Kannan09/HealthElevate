
using Microsoft.EntityFrameworkCore;
using NutritionDb_Approach.Models;
using NutritionDb_Approach.Repository;
using NutritionDb_Approach.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NutritionDb_Approach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Register DbContext
            builder.Services.AddDbContext<NutritionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

            // Register repositories and services
            builder.Services.AddScoped<INutritionRepository, NutritionRepository>();
            builder.Services.AddScoped<INutritionService, NutritionService>();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            builder.Services.AddCors(op => op.AddPolicy("Policy", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
            builder.Services.AddConsul(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors("Policy");


            app.MapControllers();
            app.UseConsul(app.Configuration);
            app.Run();
        }
    }
}
