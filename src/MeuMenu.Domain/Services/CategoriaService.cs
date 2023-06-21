using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository  _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    
    public async Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync()
    {
        return await _categoriaRepository.ObterTodos();
    }
}