using MeuMenu.Application.ViewModels;

namespace MeuMenu.Application.Interfaces;

public interface IProdutoAppService
{
    Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoViewModel produtoViewModel);
    Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel);
    Task ExcluirProdutoAsync(Guid produtoViewModelId);
    Task<ProdutoViewModel> ObterProdutoPorIdAsync(Guid produtoViewModelId);
    Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync();
    Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync();
}