using DevExpress.Data;
using DevExpress.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_12345";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "http://localhost:5160";

var connectionString = builder.Configuration.GetConnectionString("Supabase");
Console.WriteLine(">>> Connection string: " + connectionString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connectionString, b => b.CommandTimeout(120)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddControllers().AddOData(opt =>
{
    var modelBuilder = new ODataConventionModelBuilder();
    modelBuilder.EntitySet<Product>("Products");
    modelBuilder.EntitySet<Shop>("Shops");
    modelBuilder.EntitySet<Device>("Devices");
    modelBuilder.EntitySet<User>("Users");

    opt.AddRouteComponents("odata", modelBuilder.GetEdmModel())
       .Select()
       .Filter()
       .OrderBy()
       .Count()
       .Expand()
       .SetMaxTop(1000);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine(">>> Checking database connection...");
        db.Database.OpenConnection();
        Console.WriteLine("Connected successfully to database.");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database connection failed: " + ex.Message);
    }
}

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("Server started using HTTP/1.1");
});

app.UseRouting();
app.UseCors("AllowFrontend");

app.Use(async (context, next) =>
{
    try
    {
        context.Response.Headers.Remove("Content-Encoding");
        await next();
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Request został anulowany przez klienta.");
        context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex.Message}");
        throw;
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
