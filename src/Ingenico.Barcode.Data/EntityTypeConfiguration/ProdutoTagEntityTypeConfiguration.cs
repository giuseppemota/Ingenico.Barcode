using Ingenico.Barcode.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Data.EntityTypeConfiguration
{
    public class ProdutoTagEntityTypeConfiguration : IEntityTypeConfiguration<ProdutoTag>
    {
        public void Configure(EntityTypeBuilder<ProdutoTag> builder)
        {
            builder.HasKey(pt => new { pt.ProdutoId, pt.TagId });

            builder.HasOne(pt => pt.Produto)
                   .WithMany(p => p.ProdutoTag)
                   .HasForeignKey(pt => pt.ProdutoId);

            builder.HasOne(pt => pt.Tag)
                   .WithMany(t => t.ProdutoTag)
                   .HasForeignKey(pt => pt.TagId);
        }
    }
}
