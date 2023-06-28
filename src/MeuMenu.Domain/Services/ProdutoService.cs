using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.Services.Base;

namespace MeuMenu.Domain.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository  _produtoRepository;
    private readonly NegocioService _negocioService;

    public ProdutoService(
        IProdutoRepository produtoRepository, 
        NegocioService negocioService)
    {
        _produtoRepository = produtoRepository;
        _negocioService = negocioService;
    }

    public async Task<Produto> AdicionarProdutoAsync(Produto produto)
    {
        // TODO: Criar validações e adicionar ao produto
        await _negocioService.ExecutarValidacao();
        if (!_negocioService.EhValido()) return produto;
        
        await _produtoRepository.Adicionar(produto);

        return produto;
    }

    public async Task<Produto> AtualizarProdutoAsync(Produto produto)
    {
        // TODO: Criar validações e adicionar ao produto
        await _negocioService.ExecutarValidacao();
        if (!_negocioService.EhValido()) return produto;
        
        await _produtoRepository.Atualizar(produto);

        return produto;
    }

    public async Task ExcluirProdutoAsync(Guid produtoId)
    {
        // TODO: Criar validações e adicionar ao produto
        await _negocioService.ExecutarValidacao();
        if (!_negocioService.EhValido()) return;
        await _produtoRepository.Remover(produtoId);
    }

    public async Task<Produto?> ObterProdutoPorIdAsync(Guid produtoId)
    {
        return await _produtoRepository.ObterProdutoPorIdAsync(produtoId);
    }

    public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
    {
        return await _produtoRepository.ObterTodos();
    }

    public async Task<IEnumerable<Produto>> ObterProdutosAtivosAsync()
    {
        return await _produtoRepository.Buscar(p => p.ProdutoAtivo);
    }

    public Task<Produto?> BuscarProdutoValorAsync(Guid produtoId) => _produtoRepository.BuscarProdutoValorAsync(produtoId);

    public Task<ICollection<Produto>> BuscarProdutoDescricaoEImagemAsync(ICollection<Guid> listaIds) =>
        _produtoRepository.BuscarProdutoDescricaoEImagemAsync(listaIds);
}