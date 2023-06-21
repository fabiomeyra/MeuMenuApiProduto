using AutoMapper;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Services;

namespace MeuMenu.Application.AppServices;

public class CategoriaAppService : ICategoriaAppService
{
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public CategoriaAppService(IMapper mapper, ICategoriaService categoriaService)
    {
        _mapper = mapper;
        _categoriaService = categoriaService;
    }
    
    public async Task<IEnumerable<CategoriaViewModel>> ObterTodasCategoriasAsync()
    {
        var categorias = await _categoriaService.ObterTodasCategoriasAsync();
        return _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias);
    }
}