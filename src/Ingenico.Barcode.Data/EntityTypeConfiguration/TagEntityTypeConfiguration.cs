using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ingenico.Barcode.Data.EntityTypeConfiguration;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagEntity> {

    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("Tag");
        builder.HasKey(t => t.TagId);

        builder.Property(t => t.Nome).IsRequired().HasMaxLength(150);

        builder.HasMany(t => t.ProdutoTag)
               .WithOne(pt => pt.Tag)
               .HasForeignKey(pt => pt.TagId);
    }
}

