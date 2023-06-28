using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Models;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace MeuMenu.Infra.Data.Repository;

public class ProdutoRepository : Repository<Produto, MeuMenuDbContext>, IProdutoRepository
{
    public ProdutoRepository(MeuMenuDbContext db) : base(db) { }


    public async Task Remover(Guid produtoId)
    {
        DbSet.Remove(new Produto { ProdutoId = produtoId });
        await SaveChanges();
    }

    public override async Task<List<Produto>> ObterTodos()
    {
        return await Db.Produtos.AsNoTracking()
            .Include(c => c.Categoria)
            .ToListAsync();
    }

    public async Task<Produto?> ObterProdutoPorIdAsync(Guid produtoId)
    {
        return await Db.Produtos
            .Where(p => p.ProdutoId == produtoId)
            .Include(c => c.Categoria)
            .FirstOrDefaultAsync();
    }

    public async Task<Produto?> BuscarProdutoValorAsync(Guid produtoId)
    {
        var produto = await Db.Produtos
            .Where(x => x.ProdutoId == produtoId)
            .Select(x => new Produto
            {
                ProdutoId = x.ProdutoId,
                ProdutoValor = x.ProdutoValor
            })
            .FirstOrDefaultAsync();

        return produto;
    }

    public async Task<ICollection<Produto>> BuscarProdutoDescricaoEImagemAsync(ICollection<Guid> listaIds)
    {
        var produtos = await Db.Produtos
            .Where(x => listaIds.Any(y => y == x.ProdutoId))
            .Select(x => new Produto
            {
                ProdutoId = x.ProdutoId,
                ProdutoDescricao = x.ProdutoDescricao,
                ProdutoImagem = x.ProdutoImagem
            }).ToListAsync();

        return produtos;
    }
}