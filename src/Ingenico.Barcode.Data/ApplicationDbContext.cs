using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ingenico.Barcode.Domain.Entites;
using System.Threading;

namespace Ingenico.Barcode.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ProdutoEntity> Produto { get; set; } = default!;
    public DbSet<CategoriaEntity> Categoria { get; set; } = default!;
    public DbSet<TagEntity> Tag { get; set; } = default!;
    public DbSet<ProdutoCategoria> ProdutoCategoria { get; set; } = default!;
    public DbSet<ProdutoTag> ProdutoTag { get; set; } = default!;

    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Entity<ProdutoEntity>().HasQueryFilter(p => !p.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is ProdutoEntity && e.State == EntityState.Deleted);

        foreach (var entry in entries) {
            entry.State = EntityState.Modified; 
            ((ProdutoEntity)entry.Entity).IsDeleted = true; 
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}