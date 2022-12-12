using Bouvet.AssetHub.API.Domain.Loan.Repositories;
using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Repositories;
using Bouvet.AssetHub.Repositories.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Enable and add cors
var MyCorsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCorsPolicy, policy =>
    {
        policy
            .WithOrigins("https://localhost:3000", "http://localhost:3000" )
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
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
});
var assemblyHandlers = AppDomain.CurrentDomain.Load("Bouvet.AssetHub.Handlers");
builder.Services.AddAutoMapper(assemblyHandlers, Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<DataContext>(options =>
{
    // // Retrive connectionstring from User Secrets 
    // var conStrBuilder = new SqlConnectionStringBuilder(
    //     builder.Configuration.GetConnectionString("DataContext"));
    // conStrBuilder.Password = builder.Configuration["SqlPassword"];
    // conStrBuilder.UserID = builder.Configuration["SqlUser"];
    // var connectionString = conStrBuilder.ConnectionString;
    var connectionString = builder.Configuration.GetConnectionString("DataContext");
    options.UseSqlServer(connectionString);
});
var assemblyContracts = AppDomain.CurrentDomain.Load("Bouvet.AssetHub.Contracts");
builder.Services.AddMediatR(assemblyContracts, assemblyHandlers);

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