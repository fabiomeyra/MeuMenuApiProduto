using System.Linq.Expressions;

namespace MeuMenu.Domain.Interfaces.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task Adicionar(TEntity entity);
    Task<List<TEntity>> ObterTodos();
    Task Atualizar(TEntity entity);
    Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChanges();
}