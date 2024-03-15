using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json");
var app = builder.Build();


await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();

//using System.IO;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;

//WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


//WebApplication app = builder.Build();
//app.UseRouting();
//await app.RunAsync();
