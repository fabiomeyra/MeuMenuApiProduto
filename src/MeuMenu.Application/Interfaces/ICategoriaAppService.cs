using MeuMenu.Application.ViewModels;

namespace MeuMenu.Application.Interfaces;

public interface ICategoriaAppService
{
    Task<IEnumerable<CategoriaViewModel>> ObterTodasCategoriasAsync();
}