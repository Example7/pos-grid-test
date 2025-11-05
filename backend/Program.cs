using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_12345";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "http://localhost:5160";

var connectionString = builder.Configuration.GetConnectionString("Supabase");
Console.WriteLine(">>> Connection string: " + connectionString);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

builder.Services.AddDbContext<SupabaseDbContext>(opt =>
    opt.UseNpgsql(connectionString, b =>
    {
        b.CommandTimeout(30);
        b.EnableRetryOnFailure(2);
    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddControllers()
    .AddOData(opt =>
        opt.Select().Filter().OrderBy().Count().Expand().SetMaxTop(100)
           .AddRouteComponents("odata", GetEdmModel())
    );

IEdmModel GetEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();

    // --- Produkty i powiązania ---
    modelBuilder.EntitySet<Product>("Products");
    modelBuilder.EntitySet<ProductsCategories1>("ProductsCategories1");
    modelBuilder.EntitySet<ProductsCategories2>("ProductsCategories2");
    modelBuilder.EntitySet<ProductsQuantityUnit>("ProductsQuantityUnits");
    modelBuilder.EntitySet<ProductsVatRate>("ProductsVatRates");

    // --- Zamówienia ---
    modelBuilder.EntitySet<Order>("Orders");
    modelBuilder.EntitySet<OrdersItem>("OrdersItems");
    modelBuilder.EntitySet<OrdersPaymentsStatus>("OrdersPaymentsStatuses");
    modelBuilder.EntitySet<OrdersRealizationsStatus>("OrdersRealizationsStatuses");
    modelBuilder.EntitySet<OrdersRealizationsType>("OrdersRealizationsTypes");

    // --- Finanse / sprzedaż ---
    modelBuilder.EntitySet<Outcome>("Outcomes");
    modelBuilder.EntitySet<OutcomesItem>("OutcomesItems");
    modelBuilder.EntitySet<OutcomesStatus>("OutcomesStatuses");
    modelBuilder.EntitySet<OutcomesFinancialDocument>("OutcomesFinancialDocuments");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsItem>("OutcomesFinancialDocumentsItems");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsStatus>("OutcomesFinancialDocumentsStatuses");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsVatSummary>("OutcomesFinancialDocumentsVatSummaries");

    // --- Sklepy, magazyny, POS ---
    modelBuilder.EntitySet<Shop>("Shops");
    modelBuilder.EntitySet<Store>("Stores");
    modelBuilder.EntitySet<Pose>("Poses");
    modelBuilder.EntitySet<PosesType>("PosesTypes");

    // --- Pracownicy i kontrahenci ---
    modelBuilder.EntitySet<Employee>("Employees");
    modelBuilder.EntitySet<Contractor>("Contractors");
    modelBuilder.EntitySet<Country>("Countries");

    // --- Metody płatności ---
    modelBuilder.EntitySet<PaymentsMethod>("PaymentsMethods");

    return modelBuilder.GetEdmModel();
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SupabaseDbContext>();
    try
    {
        Console.WriteLine(">>> Checking database connection...");
        var canConnect = await db.Database.CanConnectAsync();
        Console.WriteLine(canConnect
            ? "Connected successfully to database."
            : "Database connection test failed (no response).");
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
        if (!context.Response.HasStarted)
            context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;

        Console.WriteLine("Request został anulowany przez klienta.");
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