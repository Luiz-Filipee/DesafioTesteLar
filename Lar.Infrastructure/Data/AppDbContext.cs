using Microsoft.EntityFrameworkCore;
using Lar.Domain.Entities;

namespace Lar.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Telefone> Telefones => Set<Telefone>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Telefones)
                .WithOne(t => t.Pessoa)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
