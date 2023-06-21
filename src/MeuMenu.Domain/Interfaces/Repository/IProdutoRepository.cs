using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Interfaces.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    Task Remover(Guid produtoId);
}