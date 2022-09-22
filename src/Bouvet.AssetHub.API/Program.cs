using Bouvet.AssetHub.Domain.Data;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

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
