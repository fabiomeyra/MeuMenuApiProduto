using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository  _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> AdicionarProdutoAsync(Produto produto)
    {
        await _produtoRepository.Adicionar(produto);

        return produto;
    }

    public async Task<Produto> AtualizarProdutoAsync(Produto produto)
    {
        await _produtoRepository.Atualizar(produto);

        return produto;
    }

    public async Task ExcluirProdutoAsync(Guid produtoId)
    {
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
}