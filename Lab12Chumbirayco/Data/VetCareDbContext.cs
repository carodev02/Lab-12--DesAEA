using Microsoft.EntityFrameworkCore;
using Lab12Chumbirayco.Models;

namespace Lab12Chumbirayco.Data;

public partial class VetCareDbContext(DbContextOptions<VetCareDbContext> options) : DbContext(options)
{
    public virtual DbSet<Owner> Owners => Set<Owner>();
    public virtual DbSet<Pet> Pets => Set<Pet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("owners");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(160).IsRequired();
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(25).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.ToTable("pets");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
            entity.Property(e => e.Species).HasColumnName("species").HasMaxLength(40).IsRequired();
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.HasOne(e => e.Owner).WithMany(e => e.Pets)
                .HasForeignKey(e => e.OwnerId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
