using MeuMenu.Domain.Models;
using MeuMenu.Infra.CrossCutting.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MeuMenu.Infra.Data.Context;

public sealed class MeuMenuDbContext : DbContext
{
    public MeuMenuDbContext(IOptions<AppSettings> appSettingsOptions) 
        : base(ObterContextOptions(appSettingsOptions.Value))
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }
    private static DbContextOptions ObterContextOptions(AppSettings configuracoes)
    {
        var conexao = configuracoes.ConnectionString?.MeuMenuDb;
        return new DbContextOptionsBuilder().UseSqlServer(conexao).Options;
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuMenuDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}