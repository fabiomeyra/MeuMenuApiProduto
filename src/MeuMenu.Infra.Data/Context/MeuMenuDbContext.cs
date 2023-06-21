using MeuMenu.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuMenu.Infra.Data.Context;

public class MeuMenuDbContext : DbContext
{
    public MeuMenuDbContext() { }

    public MeuMenuDbContext(DbContextOptions<MeuMenuDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuMenuDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}