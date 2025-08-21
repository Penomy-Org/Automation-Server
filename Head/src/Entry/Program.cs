using System;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRouting();

    app.MapControllers();
}

if (app.Environment.IsStaging())
{
    app.UseRouting();

    app.MapControllers();
}

if (app.Environment.IsProduction())
{
    app.UseRouting();

    app.MapControllers();
}

await app.RunAsync();
