using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService;
using UserService.Data;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthService>(); // Register AuthService

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Allow any origin
               .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // Allow any header
    });
});

// Configure JWT authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Specify your JWT Issuer
            ValidAudience = builder.Configuration["Jwt:Audience"], // Specify your JWT Audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Signing key
        };
    });

builder.Services.AddSwaggerGen(); // Add Swagger for API documentation
builder.Services.AddControllers(); // Add MVC controllers
builder.Services.AddConsul(builder.Configuration);
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseCors("AllowAllOrigins"); // Enable CORS with the defined policy
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes
app.UseConsul(app.Configuration);
app.Run(); // Run the application
