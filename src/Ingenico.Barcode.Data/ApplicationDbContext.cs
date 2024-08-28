using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ProdutoEntity> Produto { get; set; } = default!;
 
    public DbSet<CategoriaEntity> Categoria { get; set; } = default!;

    public DbSet<TagEntity> Tag { get; set; } = default!;
    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}