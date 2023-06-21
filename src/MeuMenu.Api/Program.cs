using MeuMenu.Application.AutoMapper;
using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Services;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using MeuMenu.Application.AppServices;
using MeuMenu.Application.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<MeuMenuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeuMenuDb"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Meu Menu - API Produtos", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<MeuMenuDbContext>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();

builder.Services.AddScoped<IProdutoService, ProdutoService>();

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
