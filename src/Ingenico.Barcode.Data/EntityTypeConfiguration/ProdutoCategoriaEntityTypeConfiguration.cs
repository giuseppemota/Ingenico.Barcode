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
    public class ProdutoCategoriaEntityTypeConfiguration : IEntityTypeConfiguration<ProdutoCategoria>
    {
        public void Configure(EntityTypeBuilder<ProdutoCategoria> builder)
        {
            builder.HasKey(pc => new { pc.ProdutoId, pc.CategoriaId });

            builder.HasOne(pc => pc.Produto)
                   .WithMany(p => p.ProdutoCategoria)
                   .HasForeignKey(pc => pc.ProdutoId);

            builder.HasOne(pc => pc.Categoria)
                   .WithMany(c => c.ProdutoCategoria)
                   .HasForeignKey(pc => pc.CategoriaId);
        }
    }
}
