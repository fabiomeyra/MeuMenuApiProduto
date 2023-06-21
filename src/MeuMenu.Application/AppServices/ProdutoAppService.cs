using AutoMapper;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;

namespace MeuMenu.Application.AppServices;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutoAppService(IProdutoService produtoService, IMapper mapper)
    {
        _produtoService = produtoService;
        _mapper = mapper;
    }

    public async Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoViewModel produtoViewModel)
    {
        var produto = _mapper.Map<Produto>(produtoViewModel);

        produto.GerarId();

        produto = await _produtoService.AdicionarProdutoAsync(produto);

        return _mapper.Map<ProdutoViewModel>(produto);
    }

    public async Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel)
    {
        var produto = _mapper.Map<Produto>(produtoViewModel);

        produto.Categoria = null;

        produto = await _produtoService.AtualizarProdutoAsync(produto);

        return _mapper.Map<ProdutoViewModel>(produto);
    }

    public async Task ExcluirProdutoAsync(Guid produtoId)
    {
        await _produtoService.ExcluirProdutoAsync(produtoId);
    }

    public async Task<ProdutoViewModel?> ObterProdutoPorIdAsync(Guid produtoId)
    {
        var produto = await _produtoService.ObterProdutoPorIdAsync(produtoId);
        return _mapper.Map<ProdutoViewModel?>(produto);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync()
    {
        var produtos = await _produtoService.ObterTodosProdutosAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync()
    {
        var produtos = await _produtoService.ObterProdutosAtivosAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
    }
}