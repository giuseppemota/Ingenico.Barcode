using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ingenico.Barcode.Data.EntityTypeConfiguration {
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagEntity> {
        public void Configure(EntityTypeBuilder<TagEntity> builder) {
            builder.ToTable("Tag");
            builder.HasKey(e => e.TagId);

            builder.Property(e => e.NomeTag).IsRequired();
        }
    }
}
