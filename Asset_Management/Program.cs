//1 The first few lines of the code import various namespaces from the .NET framework and external libraries that will be used in the application.
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Asset_Management.Models;
using Asset_Management.Services;
using Asset_Management;
using Asset_Management.CustomMiddleware;
using Microsoft.Extensions.Logging;

//2 The WebApplication.CreateBuilder(args) method is called to create a new instance of the WebApplicationBuilder class, which is used to configure and build the application.
var builder = WebApplication.CreateBuilder(args);

// 3 The builder.Services.AddAuthentication method is called to configure authentication in the application. Specifically, it sets up JWT bearer authentication using the JwtBearerDefaults.AuthenticationScheme, and specifies that authentication will be handled by Azure AD using the configuration settings in the "AzureAd" section of the application's configuration file..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


//4 The builder.Services.AddDbContext method is called to register the asset_managementContext class in the dependency injection (DI) container. This class is a database context class that is used to interact with a SQL Server database.
builder.Services.AddDbContext<asset_managementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnStr"));
});


//5 The builder.Services.AddScoped method is called several times to register various service classes in the DI container.
//These service classes implement the IService interface, which provides basic CRUD operations for working with data in the application.
//There are also two service classes that implement custom interfaces (IAssetDetailService and IAssetTransactionService) that provide additional functionality for working with asset detail and asset transaction data.
builder.Services.AddScoped<IService<User, int>, UserService>();
builder.Services.AddScoped<IService<AssetDetail, int>, AssetDetailService>();
builder.Services.AddScoped<IService<AssetTransaction, int>, AssetTransactionService>();
builder.Services.AddScoped<IService<Vendor, int>, VendorService>();
builder.Services.AddScoped<IAssetDetailService<AssetDetail,string>, AssetDetailService>();
builder.Services.AddScoped<IAssetTransactionService<AssetTransaction,string>, AssetTransactionService>();
builder.Services.AddScoped<IAuthService<User,int>,AuthService>();
builder.Services.AddScoped<IAssetTransaction, AssetTransactionService>();
builder.Services.AddScoped<LogFilterAttribute>();

//6 The builder.Services.AddControllers method is called to register the ASP.NET Core MVC controllers in the DI container.
//The options parameter is used to configure various options for the controller feature, such as filters and formatters.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogFilterAttribute>();
});

// 7 The builder.Services.AddEndpointsApiExplorer method is called to enable the generation of Swagger/OpenAPI documentation for the application's API endpoints.
builder.Services.AddEndpointsApiExplorer();

//8 The builder.Services.AddSwaggerGen method is called to configure the Swagger/OpenAPI generator.
builder.Services.AddSwaggerGen();

//9 The builder.Build() method is called to build the application and create an instance of the WebApplication class.
var app = builder.Build();

//10 The app.Environment.IsDevelopment() method is called to determine if the application is running in development mode.
//If so, the Swagger/OpenAPI documentation is enabled using the app.UseSwagger() and app.UseSwaggerUI() methods.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//11 The app.UseHttpsRedirection() method is called to enable HTTP to HTTPS redirection.
app.UseHttpsRedirection();

//12 Use Custom Exception
app.UseAppException();

//13 The app.UseAuthentication() and app.UseAuthorization() methods are called to enable authentication and authorization in the application.
app.UseAuthentication();
app.UseAuthorization();

//14 The app.MapControllers() method is called to map the application's API controllers to their respective routes.
app.MapControllers();

//15 Finally, the app.Run() method is called to start the application and begin listening for incoming requests.
app.Run();
