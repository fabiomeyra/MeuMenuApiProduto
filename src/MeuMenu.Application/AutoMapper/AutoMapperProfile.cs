﻿using AutoMapper;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Models;

namespace MeuMenu.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
    }
}