using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ingenico.Barcode.Data.EntityTypeConfiguration;

public class CategoriaEntityTypeConfiguration : IEntityTypeConfiguration<CategoriaEntity> {
    public void Configure(EntityTypeBuilder<CategoriaEntity> builder)
    {
        builder.ToTable("Categoria");
        builder.HasKey(c => c.CategoriaId);

        builder.Property(c => c.Nome).IsRequired().HasMaxLength(150);

        builder.HasMany(c => c.ProdutoCategoria)
               .WithOne(pc => pc.Categoria)
               .HasForeignKey(pc => pc.CategoriaId);
    }
}

