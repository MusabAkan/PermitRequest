using Microsoft.EntityFrameworkCore;
using PermitRequest.Application;
using PermitRequest.Infrastructure.Contexts;
using PermitRequest.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PermitRequestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

DataSeeding.Seed(app);
app.UseMiddleware<ExceptionMidleware>();

app.Run();
