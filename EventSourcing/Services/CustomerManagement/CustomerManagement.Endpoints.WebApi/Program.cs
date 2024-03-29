using CustomerManagement.ApplicationService.Customers;
using CustomerManagement.Domain.Customers.Services;
using CustomerManagement.Infrastructure.Persistence.Customers;
using EventStore.ClientAPI;
using Framework.Core;
using Framework.EventStore.EventStoreDB;
using Framework.EventStore.SqlServer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var esConnection = EventStoreConnection.Create(builder.Configuration["eventStore:connectionString"], ConnectionSettings.Create().KeepReconnecting(), "MyApplication");
//builder.Services.AddSingleton(esConnection);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CustomersService>();
builder.Services.AddScoped<IEventStore, SqlServerEventStore>();
//builder.Services.AddScoped<IEventStore, EventStoreDb>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

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
