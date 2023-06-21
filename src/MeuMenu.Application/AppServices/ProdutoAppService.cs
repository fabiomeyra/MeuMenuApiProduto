using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Services;

namespace MeuMenu.Application.AppServices;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoService _produtoService;

    public ProdutoAppService(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoViewModel produtoViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task ExcluirProdutoAsync(Guid produtoViewModelId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProdutoViewModel> ObterProdutoPorIdAsync(Guid produtoViewModelId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync()
    {
        throw new NotImplementedException();
    }
}