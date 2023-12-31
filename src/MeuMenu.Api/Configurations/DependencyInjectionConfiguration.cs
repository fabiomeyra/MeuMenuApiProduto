﻿using MeuMenu.Application.AppServices;
using MeuMenu.Application.Interfaces;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Notificador;
using MeuMenu.Domain.Services;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repository;

namespace MeuMenu.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection service)
    {
        service.AddScoped<NegocioService>();
        service.AddScoped<MeuMenuDbContext>();
        service.AddScoped<IProdutoRepository, ProdutoRepository>();
        service.AddScoped<ICategoriaRepository, CategoriaRepository>();
        
        service.AddScoped<IProdutoAppService, ProdutoAppService>();
        service.AddScoped<ICategoriaAppService, CategoriaAppService>();
        
        service.AddScoped<IProdutoService, ProdutoService>();
        service.AddScoped<ICategoriaService, CategoriaService>();

        service.AddScoped<INotificador, Notificador>();
    }
}