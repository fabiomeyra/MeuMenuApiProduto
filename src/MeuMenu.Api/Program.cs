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

builder.Services.AddScoped<INotificador, Notificador>();

builder.Services.AddScoped<MeuMenuDbContext>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<ICategoriaAppService, CategoriaAppService>();

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

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
