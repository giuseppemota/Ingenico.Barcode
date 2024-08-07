using Microsoft.EntityFrameworkCore;
using Ingenico.Barcode.Domain.Entites;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Ingenico.Barcode.Data;

public class ApplicationDbContext : IdentityDbContext<User> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }
}