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
}