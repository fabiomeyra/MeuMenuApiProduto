using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Interfaces.Services;

public interface IProdutoService
{
    Task<Produto> AdicionarProdutoAsync(Produto produto);
    Task<Produto> AtualizarProdutoAsync(Produto produto);
    Task ExcluirProdutoAsync(Guid produtoId);
    Task<Produto?> ObterProdutoPorIdAsync(Guid produtoId);
    Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
    Task<IEnumerable<Produto>> ObterProdutosAtivosAsync();
}