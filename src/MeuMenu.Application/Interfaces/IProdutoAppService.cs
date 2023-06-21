using MeuMenu.Application.ViewModels;

namespace MeuMenu.Application.Interfaces;

public interface IProdutoAppService
{
    Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoViewModel produtoViewModel);
    Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel);
    Task ExcluirProdutoAsync(Guid produtoId);
    Task<ProdutoViewModel?> ObterProdutoPorIdAsync(Guid produtoId);
    Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync();
    Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync();
}