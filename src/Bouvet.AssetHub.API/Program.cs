using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Data.Interfaces;
using Bouvet.AssetHub.API.Entities;
using Bouvet.AssetHub.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<TestDatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ITestContext, TestContext>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
