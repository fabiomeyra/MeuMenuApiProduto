using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Interfaces.Services;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync();
}