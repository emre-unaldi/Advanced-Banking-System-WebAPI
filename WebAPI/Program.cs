using Application;
using Persistence;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Service DI
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Redis Cache
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");

builder.Services.AddHttpContextAccessor();

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

// Global Exception
if (!app.Environment.IsProduction())
{
    app.ConfigureCustomExcepitonMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();