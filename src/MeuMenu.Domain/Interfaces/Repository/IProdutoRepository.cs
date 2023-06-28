using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Interfaces.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    Task Remover(Guid produtoId);
    Task<Produto?> ObterProdutoPorIdAsync(Guid produtoId);
    Task<Produto?> BuscarProdutoValorAsync(Guid produtoId);
    Task<ICollection<Produto>> BuscarProdutoDescricaoEImagemAsync(ICollection<Guid> listaIds);
}