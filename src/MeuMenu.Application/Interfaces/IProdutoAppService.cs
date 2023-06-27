using MeuMenu.Application.ViewModels;

namespace MeuMenu.Application.Interfaces;

public interface IProdutoAppService
{
    Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoAddViewModel produtoAddViewModel);
    Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoAddViewModel produtoAddViewModel);
    Task ExcluirProdutoAsync(Guid produtoId);
    Task<ProdutoViewModel?> ObterProdutoPorIdAsync(Guid produtoId);
    Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync();
    Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync();
    Task<ProdutoValorViewModel?> BuscarProdutoValorAsync(Guid produtoId);
    Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorCategoriaAsync(int categoriaId);
}