using MeuMenu.Domain.Interfaces.Repository;
using MeuMenu.Domain.Models;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repository.Base;

namespace MeuMenu.Infra.Data.Repository;

public class CategoriaRepository : Repository<Categoria, MeuMenuDbContext>, ICategoriaRepository
{
    public CategoriaRepository(MeuMenuDbContext db) : base(db) { }
}