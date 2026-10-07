using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TradeStream.Domain.Interfaces;
using TradeStream.Domain.Services;
using TradeStream.Infra.Database;
using TradeStream.Infra.Exceptions;
using TradeStream.Infra.Repositories;
using TradeStream.Application.Validators;
using FluentValidation;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    
builder.Services.AddValidatorsFromAssemblyContaining<OrderRequestValidator>();

builder.Services.AddDbContext<TradeDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddExceptionHandler<GlobalHandlerException>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
