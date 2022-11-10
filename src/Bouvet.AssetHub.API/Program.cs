using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Repositories;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Enable cors
// Add cors

var MyCorsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .WithHeaders("*")
            .WithMethods("PUT", "DELETE", "GET", "POST");
    });
});
// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SupportNonNullableReferenceTypes();
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
{
    var conStrBuilder = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("DataContext"));
    conStrBuilder.Password = builder.Configuration["SqlPassword"];
    conStrBuilder.UserID = builder.Configuration["SqlUser"];
    var connection = conStrBuilder.ConnectionString;


    //var connectionString = builder.Configuration.GetConnectionString("DataContext");
    options.UseSqlServer(connection);
});
//var assembly = AppDomain.CurrentDomain.Load("Bouvet.AssetHub.Domain");
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILoanHistoryRepository, LoanHistoryRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
