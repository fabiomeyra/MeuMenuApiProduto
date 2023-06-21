using MeuMenu.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuMenu.Infra.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(c => c.ProdutoId);

        builder.Property(c => c.ProdutoDescricao)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.ProdutoAtivo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(c => c.ProdutoImagem)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.ProdutoValor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ProdutoIngredientes)
            .IsRequired()
            .HasColumnType("varchar(max)");

        builder.Property(c => c.ProdutoCalorias)
            .HasColumnType("int");

        builder.Property(c => c.ProdutoAlergias)
            .HasColumnType("varchar(200)");

        builder.Property(c => c.CategoriaId)
            .IsRequired()
            .HasColumnType("int");

        builder.ToTable("Produto", "Produto");
    }
}