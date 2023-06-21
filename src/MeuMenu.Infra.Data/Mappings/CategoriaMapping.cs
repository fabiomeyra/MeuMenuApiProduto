using MeuMenu.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuMenu.Infra.Data.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.CategoriaId);

        builder.Property(c => c.CategoriaDescricao)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.CategoriaImagem)
            .IsRequired()
            .HasColumnType("varchar(200)");
        
        builder.ToTable("Categoria", "Produto");
    }
}