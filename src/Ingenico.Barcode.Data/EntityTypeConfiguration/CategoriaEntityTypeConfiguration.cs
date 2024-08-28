using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ingenico.Barcode.Data.EntityTypeConfiguration {
    public class CategoriaEntityTypeConfiguration : IEntityTypeConfiguration<CategoriaEntity> {
        public void Configure(EntityTypeBuilder<CategoriaEntity> builder) {
            builder.ToTable("Categoria");
            builder.HasKey(e => e.CategoriaId);

            builder.Property(e => e.Nome).IsRequired();
        }
    }
}
