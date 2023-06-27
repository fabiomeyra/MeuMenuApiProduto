using MeuMenu.Api.Configurations;
using MeuMenu.Application.AutoMapper;
using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Services;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using MeuMenu.Application.AppServices;
using MeuMenu.Application.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using MeuMenu.Api.Middlewares;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Notificador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerConfiguration();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseCors();

// Adicionando Middleware para tratar exceções
app.UseExceptionHandlerMiddleware();

var options = new RewriteOptions();
options.AddRedirect("^$", "swagger");
app.UseRewriter(options);

app.UseMiddleware<RequestTelemetryMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
