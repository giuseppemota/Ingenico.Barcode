using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingenico.Barcode.Data.EntityTypeConfiguration {
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<ProdutoEntity> {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder) {
            builder.ToTable("Produto");
            builder.HasKey(p => p.ProdutoId);

            builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Peso).IsRequired();
            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Preco).IsRequired();
            builder.Property(p => p.UnidadeMedida).IsRequired();
            builder.Property(p => p.Ingredientes).IsRequired();
            builder.Property(p => p.PaisOrigem).IsRequired();
            builder.Property(p => p.Validade).IsRequired();

            builder.HasMany(p => p.Categorias).WithOne(e => e.Produto).HasForeignKey(e => e.IdProduto);
            builder.HasMany(p => p.Tags).WithOne(t => t.Produto).HasForeignKey(t => t.IdProduto);

        }
    }
}
