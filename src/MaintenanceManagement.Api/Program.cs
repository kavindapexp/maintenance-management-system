using Microsoft.EntityFrameworkCore;
using MaintenanceManagement.Api.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MaintenanceDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
policy.WithOrigins(
    "http://localhost:5173",
    "http://localhost:3000");
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseCors("ReactFrontend");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/test", () => "API is working");

app.Run();