using DevExpress.Data;
using DevExpress.Models.Generated;
using DevExpress.Models.ViewModels;
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
    modelBuilder.EntitySet<ProductsCategory1View>("ProductsCategories1View");
    modelBuilder.EntitySet<ProductsCategories2>("ProductsCategories2");
    modelBuilder.EntitySet<ProductsQuantityUnit>("ProductsQuantityUnits");
    modelBuilder.EntitySet<ProductsVatRate>("ProductsVatRates");
    modelBuilder.EntitySet<ProductsBarcode>("ProductsBarcodes");
    modelBuilder.EntitySet<ProductsRecipe>("ProductsRecipes");
    modelBuilder.EntitySet<ProductsRecipesItem>("ProductsRecipesItems");

    // --- Rabaty ---
    modelBuilder.EntitySet<Discount>("Discounts");
    modelBuilder.EntitySet<DiscountsProduct>("DiscountsProducts");
    modelBuilder.EntitySet<DiscountsStore>("DiscountsStores");

    // --- Zamówienia ---
    modelBuilder.EntitySet<Order>("Orders");
    modelBuilder.EntitySet<OrdersItem>("OrdersItems");
    modelBuilder.EntitySet<OrdersInvoice>("OrdersInvoices");
    modelBuilder.EntitySet<OrdersInvoicesType>("OrdersInvoicesTypes");
    modelBuilder.EntitySet<OrdersPaymentsStatus>("OrdersPaymentsStatuses");
    modelBuilder.EntitySet<OrdersRealizationsStatus>("OrdersRealizationsStatuses");
    modelBuilder.EntitySet<OrdersRealizationsType>("OrdersRealizationsTypes");
    modelBuilder.EntitySet<OrdersVatSummary>("OrdersVatSummarys");
    modelBuilder.EntitySet<OrdersOperationsLog>("OrdersOperationsLogs");
    modelBuilder.EntitySet<OrdersItemsOperationsLog>("OrdersItemsOperationsLogs");

    // --- Finanse / sprzedaż ---
    modelBuilder.EntitySet<Outcome>("Outcomes");
    modelBuilder.EntitySet<OutcomesItem>("OutcomesItems");
    modelBuilder.EntitySet<OutcomesStatus>("OutcomesStatuses");
    modelBuilder.EntitySet<OutcomesFinancialDocument>("OutcomesFinancialDocuments");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsItem>("OutcomesFinancialDocumentsItems");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsStatus>("OutcomesFinancialDocumentsStatuses");
    modelBuilder.EntitySet<OutcomesFinancialDocumentsVatSummary>("OutcomesFinancialDocumentsVatSummaries");
    modelBuilder.EntitySet<FinancialDocumentsType>("FinancialDocumentsTypes");

    // --- Magazyn i dostawy ---
    modelBuilder.EntitySet<Stock>("Stocks");
    modelBuilder.EntitySet<StockHistory>("StockHistorys");
    modelBuilder.EntitySet<Inorder>("Inorders");
    modelBuilder.EntitySet<InordersItem>("InordersItems");

    // --- Sklepy, POS, zestawy ---
    modelBuilder.EntitySet<Shop>("Shops");
    modelBuilder.EntitySet<Store>("Stores");
    modelBuilder.EntitySet<StoresDocumentsType>("StoresDocumentsTypes");
    modelBuilder.EntitySet<StoresOrdersType>("StoresOrdersTypes");
    modelBuilder.EntitySet<StoresDocumentsTypesCategory>("StoresDocumentsTypesCategorys");
    modelBuilder.EntitySet<Pose>("Poses");
    modelBuilder.EntitySet<PosesType>("PosesTypes");
    modelBuilder.EntitySet<Set>("Sets");
    modelBuilder.EntitySet<SetsItem>("SetsItems");
    modelBuilder.EntitySet<SetsItemsProduct>("SetsItemsProducts");
    modelBuilder.EntitySet<SetsCategory>("SetsCategorys");
    modelBuilder.EntitySet<SetsSchedule>("SetsSchedules");
    modelBuilder.EntitySet<ShopsSet>("ShopsSets");

    // --- Pracownicy i kontrahenci ---
    modelBuilder.EntitySet<Employee>("Employees");
    modelBuilder.EntitySet<Contractor>("Contractors");
    modelBuilder.EntitySet<Country>("Countries");
    modelBuilder.EntitySet<LoyaltiesWallet>("LoyaltiesWallets");

    // --- Płatności i słowniki ---
    modelBuilder.EntitySet<PaymentsMethod>("PaymentsMethods");
    modelBuilder.EntitySet<Year>("Years");
    modelBuilder.EntitySet<SyncVersion>("SyncVersions");
    modelBuilder.EntitySet<UserContext>("UserContexts");

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