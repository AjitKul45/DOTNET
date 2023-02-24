using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Asset_Management.Models;
using Asset_Management.Services;
using Asset_Management;
using Asset_Management.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


//Register the CompanyContext in DI Container
builder.Services.AddDbContext<asset_managementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnStr"));
});


//Register Service Classes in DI
builder.Services.AddScoped<IService<User, int>, UserService>();
builder.Services.AddScoped<IService<AssetDetail, int>, AssetDetailService>();
builder.Services.AddScoped<IService<AssetTransaction, int>, AssetTransactionService>();
builder.Services.AddScoped<IService<Vendor, int>, VendorService>();
builder.Services.AddScoped<IAssetDetailService<AssetDetail,string>, AssetDetailService>();
builder.Services.AddScoped<IAssetTransactionService<AssetTransaction,string>, AssetTransactionService>();
builder.Services.AddScoped<IAuthService<User,int>,AuthService>();


builder.Services.AddControllers( options =>
{
    //options.Filters.Add(new LogFilterAttribute());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Use Custom Exception
// app.UseAppException();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
