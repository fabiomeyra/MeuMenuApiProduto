using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MeuMenu.Domain.Interfaces.Repository;

namespace MeuMenu.Infra.Data.Repository.Base;

public class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    protected readonly TDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(TDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }
    
    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}