using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Infrastructure.EntityFramework;
using PermitRequest.Infrastructure.EntityFramework.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Guard.Against.Null(connectionString);
object value = builder.Services.AddInfrastructure(connectionString, typeof(Program));

 

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DataSeeding.Seed(app);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
